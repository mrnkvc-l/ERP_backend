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
    [Route("api/racuni")]
    [Produces("application/json", "application/xml")]
    public class RacunController : ControllerBase
    {
        private readonly IRacunRepository racunRepository;
        private readonly IKorisnikRepository korisnikRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public RacunController(IRacunRepository racunRepository,IKorisnikRepository korisnikRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.racunRepository = racunRepository;
            this.korisnikRepository = korisnikRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        [HttpHead]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<RacunDTO>> GetAllRacuni()
        {
            try
            {
                if (HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Role)?.Value == "ADMIN")
                {
                    List<RacunEntity> racuni = racunRepository.GetAllRacuni();

                    if (racuni == null || racuni.Count == 0)
                        return NoContent();

                    List<RacunDTO> racuniDTO = new();

                    foreach(RacunEntity racun in racuni)
                    {
                        RacunDTO racunDTO = mapper.Map<RacunDTO>(racun);

                        racunDTO.Kupac = mapper.Map<KorisnikDTO>(korisnikRepository.GetKorisnikByID(racun.IDKupac));

                        racuniDTO.Add(racunDTO);
                    }

                    return Ok(racuniDTO);
                }
                else
                    return StatusCode(StatusCodes.Status403Forbidden, "Access forbiden");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{racunID}")]
        public ActionResult<RacunDTO> GetRacunByID(int racunID)
        {
            try
            {
                RacunEntity? racun = racunRepository.GetRacunByID(racunID);

                if (HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Role)?.Value == "ADMIN" ||
                    HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.NameIdentifier)?.Value == racun?.IDKupac.ToString())
                {
                    if (racun == null)
                        return NoContent();

                    RacunDTO racunDTO = mapper.Map<RacunDTO>(racun);

                    racunDTO.Kupac = mapper.Map<KorisnikDTO>(korisnikRepository.GetKorisnikByID(racun.IDKupac));

                    return Ok(racunDTO);
                }
                else
                {
                    return StatusCode(StatusCodes.Status403Forbidden, "Access forbiden");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Consumes("application/json")]
        public ActionResult<RacunDTO> CreateRacun([FromBody] RacunCreateDTO racunCreateDTO)
        {
            try
            {
                if (HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Role)?.Value == "USER" ||
                    HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Role)?.Value == "ADMIN")
                {
                    KorisnikEntity? kupac = korisnikRepository.GetKorisnikByID(racunCreateDTO.IDKupac);
                    if (kupac == null)
                        return StatusCode(StatusCodes.Status422UnprocessableEntity, "Neki od starnih kljuceva nedostaje!");
                    else
                    {
                        RacunDTO racunDTO = racunRepository.CreateRacun(racunCreateDTO);
                        racunRepository.SaveChanges();

                        racunDTO.Kupac = mapper.Map<KorisnikDTO>(kupac);

                        return Ok("Uspesno kreirana racun!");
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

        [HttpDelete("{racunID}")]
        public IActionResult DeleteRacun(int racunId)
        {
            try
            {
                if (HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Role)?.Value == "ADMIN")
                {
                    RacunEntity? racun = racunRepository.GetRacunByID(racunId);
                    if (racun == null)
                        return NotFound();

                    racunRepository.DeleteRacun(racunId);
                    racunRepository.SaveChanges();

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
    }
}
