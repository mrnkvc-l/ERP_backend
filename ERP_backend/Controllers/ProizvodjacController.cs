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
    [Route("api/proizvodjaci")]
    [Produces("application/json", "application/xml")]
    public class ProizvodjacController : ControllerBase
    {
        private readonly IProizvodjacRepository proizvodjacRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public ProizvodjacController(IProizvodjacRepository proizvodjacRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.proizvodjacRepository = proizvodjacRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        [AllowAnonymous]
        [HttpHead]
        [HttpGet]
        public ActionResult<List<ProizvodjacDTO>> GetAllProizvodjaci()
        {
            try
            {
                List<ProizvodjacEntity> proizvodjaci = proizvodjacRepository.GetAllProizvodjaci();
                if (proizvodjaci == null || proizvodjaci.Count == 0)
                    return NoContent();

                List<ProizvodjacDTO> proizvodjaciDTO = mapper.Map<List<ProizvodjacDTO>>(proizvodjaci);
                return Ok(proizvodjaciDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet("{proizvodjacID}")]
        public ActionResult<ProizvodjacDTO> GetProizvodjacByID(int proizvodjacID)
        {
            try
            {
                ProizvodjacEntity? proizvodjac = proizvodjacRepository.GetProizvodjacByID(proizvodjacID);
                if (proizvodjac == null)
                    return NoContent();

                ProizvodjacDTO proizvodjacDTO = mapper.Map<ProizvodjacDTO>(proizvodjac);
                return Ok(proizvodjacDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Consumes("application/json")]
        public ActionResult<ProizvodjacDTO> CreateProizvodjac([FromBody] ProizvodjacCreateDTO proizvodjacCreateDTO)
        {
            try
            {
                if (HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Role)?.Value == "ADMIN")
                {
                    List<ProizvodjacEntity> proizvodjaci = proizvodjacRepository.GetAllProizvodjaci();
                    if (proizvodjaci.Find(e => e.Naziv == proizvodjacCreateDTO.Naziv) == null)
                    {
                        ProizvodjacDTO proizvodjacDTO = proizvodjacRepository.CreateProizvodjac(proizvodjacCreateDTO);
                        proizvodjacRepository.SaveChanges();

                        return Ok("Uspesno kreiran proizvodjac!");
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status422UnprocessableEntity, "Vec postoji proizvodjac sa istim nazivom!");
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

        [HttpDelete("{proizvodjacID}")]
        public IActionResult DeleteProizvodjac(int proizvodjacID)
        {
            try
            {
                if (HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Role)?.Value == "ADMIN")
                {
                    ProizvodjacEntity? proizvodjac = proizvodjacRepository.GetProizvodjacByID(proizvodjacID);
                    if (proizvodjac == null)
                        return NotFound();

                    proizvodjacRepository.DeleteProizvodjac(proizvodjacID);
                    proizvodjacRepository.SaveChanges();

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
        public ActionResult<ProizvodjacDTO> UpdateProizvodjac([FromBody] ProizvodjacUpdateDTO proizvodjacUpdateDTO)
        {
            try
            {
                if (HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Role)?.Value == "ADMIN")
                {
                    ProizvodjacEntity? oldProizvodjac = proizvodjacRepository.GetProizvodjacByID(proizvodjacUpdateDTO.IDProizvodjac);

                    if (oldProizvodjac == null)
                        return NotFound();

                    ProizvodjacEntity proizvodjac = mapper.Map<ProizvodjacEntity>(proizvodjacUpdateDTO);
                    mapper.Map(proizvodjac, oldProizvodjac);
                    proizvodjacRepository.SaveChanges();

                    return Ok(mapper.Map<ProizvodjacDTO>(proizvodjac));
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
