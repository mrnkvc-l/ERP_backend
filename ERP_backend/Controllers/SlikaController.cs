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
    [Route("api/slike")]
    [Produces("application/json", "application/xml")]
    public class SlikaController : ControllerBase
    {
        private readonly ISlikaRepository slikaRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public SlikaController(ISlikaRepository slikaRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.slikaRepository = slikaRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        [AllowAnonymous]
        [HttpHead]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<SlikaDTO>> GetAllSlike()
        {
            try
            {
                List<SlikaEntity> slike = slikaRepository.GetAllSlike();

                if (slike == null || slike.Count == 0)
                    return NoContent();

                List<SlikaDTO> slikeDTO = mapper.Map<List<SlikaDTO>>(slike);

                return Ok(slikeDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet("{slikaID}")]
        public ActionResult<SlikaDTO> GetSlikaByID(int slikaID)
        {
            try
            {
                SlikaEntity? slika = slikaRepository.GetSlikaByID(slikaID);

                if (slika == null)
                    return NoContent();

                SlikaDTO slikaDTO = mapper.Map<SlikaDTO>(slika);

                return Ok(slikaDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Consumes("application/json")]
        public ActionResult<SlikaDTO> CreateSlika([FromBody] SlikaCreateDTO slikaCreateDTO)
        {
            try
            {
                if (HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Role)?.Value == "ADMIN")
                {
                    List<SlikaEntity> slike = slikaRepository.GetAllSlike();
                    SlikaDTO slikaDTO = slikaRepository.CreateSlika(slikaCreateDTO);
                    slikaRepository.SaveChanges();

                    return Ok("Uspesno kreirana slika!");
                }
                else
                    return StatusCode(StatusCodes.Status403Forbidden, "Access forbiden");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{slikaID}")]
        public IActionResult DeleteSlika(int slikaId)
        {
            try
            {
                if (HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Role)?.Value == "ADMIN")
                {
                    SlikaEntity? slika = slikaRepository.GetSlikaByID(slikaId);
                    if (slika == null)
                        return NotFound();

                    slikaRepository.DeleteSlika(slikaId);
                    slikaRepository.SaveChanges();

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
        public ActionResult<SlikaDTO> UpdateSlika([FromBody] SlikaUpdateDTO slikaUpdateDTO)
        {
            try
            {
                if (HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Role)?.Value == "ADMIN")
                {
                    SlikaEntity? oldSlika = slikaRepository.GetSlikaByID(slikaUpdateDTO.IDSlika);

                    if (oldSlika == null)
                        return NotFound();

                    SlikaEntity slika = mapper.Map<SlikaEntity>(slikaUpdateDTO);
                    mapper.Map(slika, oldSlika);
                    slikaRepository.SaveChanges();

                    return Ok(mapper.Map<SlikaDTO>(slika));
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
