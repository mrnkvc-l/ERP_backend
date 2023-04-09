using AutoMapper;
using ERP_backend.Entity;
using ERP_backend.Model;
using ERP_backend.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ERP_backend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/skladista")]
    [Produces("application/json", "application/xml")]
    public class SkladisteController : ControllerBase
    {
        private readonly ISkladisteRepository skladisteRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public SkladisteController(ISkladisteRepository skladisteRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.skladisteRepository = skladisteRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult<List<SkladisteDTO>> GetAllSkladista()
        {
            try
            {
                List<SkladisteEntity> skladista = skladisteRepository.GetAllSkladista();
                if(skladista == null)
                    return NoContent();
                List<SkladisteDTO> skladistaDTO = mapper.Map<List<SkladisteDTO>>(skladista);

                return Ok(skladistaDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [AllowAnonymous]
        [HttpGet("{skladisteID}")]
        public ActionResult<SkladisteDTO> GetSkladisteByID(int skladisteID)
        {
            try
            {
                SkladisteEntity? skladiste = skladisteRepository.GetSkladisteByID(skladisteID);

                if (skladiste == null)
                    return NoContent();

                SkladisteDTO skladisteDTO = mapper.Map<SkladisteDTO>(skladiste);

                return Ok(skladisteDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Consumes("application/json")]
        public ActionResult<SkladisteDTO> CreateSkladiste([FromBody] SkladisteCreateDTO skladisteCreateDTO)
        {
            try
            {
                if (HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Role)?.Value == "ADMIN")
                {
                    List<SkladisteEntity> skladista = skladisteRepository.GetAllSkladista();
                    if (skladista.Find(e => e.Naziv == skladisteCreateDTO.Naziv) == null)
                    {
                        SkladisteDTO skladisteDTO = skladisteRepository.CreateSkladiste(skladisteCreateDTO);
                        skladisteRepository.SaveChanges();

                        return Ok("Uspesno kreirana skladiste!");
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status422UnprocessableEntity, "Vec postoji skladiste sa istim nazivom!");
                    }
                }
                else
                    return StatusCode(StatusCodes.Status403Forbidden, "Access forbiden");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpDelete("{skladisteID}")]
        public IActionResult DeleteSkladiste(int skladisteId)
        {
            try
            {
                if (HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Role)?.Value == "ADMIN")
                {
                    SkladisteEntity? skladiste = skladisteRepository.GetSkladisteByID(skladisteId);
                    if (skladiste == null)
                        return NotFound();

                    skladisteRepository.DeleteSkladiste(skladisteId);
                    skladisteRepository.SaveChanges();

                    return NoContent();
                }
                else
                    return StatusCode(StatusCodes.Status403Forbidden, "Access forbiden");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        [Consumes("application/json")]
        public ActionResult<SkladisteDTO> UpdateSkladiste([FromBody] SkladisteUpdateDTO skladisteUpdateDTO)
        {
            try
            {
                if (HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Role)?.Value == "ADMIN")
                {
                    SkladisteEntity? oldSkladiste = skladisteRepository.GetSkladisteByID(skladisteUpdateDTO.IDSkladiste);

                    if (oldSkladiste == null)
                        return NotFound();

                    SkladisteEntity skladiste = mapper.Map<SkladisteEntity>(skladisteUpdateDTO);
                    mapper.Map(skladiste, oldSkladiste);
                    skladisteRepository.SaveChanges();

                    return Ok(mapper.Map<SkladisteDTO>(skladiste));
                }
                else
                    return StatusCode(StatusCodes.Status403Forbidden, "Access forbiden");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
