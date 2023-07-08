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

        [AllowAnonymous]
        [HttpGet("kupac/{kupacID}")]
        public ActionResult<List<RacunDTO>> GetRacuniByKupac()
        {
            try
            {
                KorisnikEntity? korisnik = korisnikRepository.GetKorisnikByID(Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.NameIdentifier)?.Value));
                if (korisnik == null)
                    return StatusCode(StatusCodes.Status403Forbidden, "Access forbiden");
                else
                {
                    List<RacunEntity> racuni = racunRepository.GetRacunEntitiesByUser(korisnik.IDKorisnik);

                    if (racuni == null || racuni.Count == 0)
                        return NoContent();

                    List<RacunDTO> racuniDTO = new();

                    foreach (RacunEntity racun in racuni)
                    {
                        RacunDTO racunDTO = mapper.Map<RacunDTO>(racun);

                        racunDTO.Kupac = mapper.Map<KorisnikDTO>(korisnikRepository.GetKorisnikByID(racun.IDKupac));

                        racuniDTO.Add(racunDTO);
                    }

                    return Ok(racuniDTO);
                }
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
                    KorisnikEntity? kupac = korisnikRepository.GetKorisnikByID(int.Parse(HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.NameIdentifier)?.Value));
                    if (kupac == null)
                        return StatusCode(StatusCodes.Status422UnprocessableEntity, "Neki od starnih kljuceva nedostaje!");
                    else
                    {
                        racunCreateDTO.IDKupac = kupac.IDKorisnik;
                        RacunDTO racunDTO = racunRepository.CreateRacun(racunCreateDTO);
                        racunRepository.SaveChanges();

                        racunDTO.Kupac = mapper.Map<KorisnikDTO>(kupac);

                        List<RacunEntity> racuni = racunRepository.GetAllRacuni();

                        RacunDTO prikaz = mapper.Map<RacunDTO>(racuni.Find(e => e.UkupnaCena.Equals(racunDTO.UkupnaCena) && e.IDKupac.Equals(racunDTO.Kupac.IDKorisnik) && e.Datum.Equals(racunDTO.Datum)));

                        return Ok(prikaz);
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

        [HttpPut]
        [Consumes("application/json")]
        public ActionResult<RacunDTO> UpdateRacun([FromBody] RacunUpdateDTO racunUpdateDTO)
        {
            try
            {
                if (HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Role)?.Value == "ADMIN")
                {
                    RacunEntity? oldRacun = racunRepository.GetRacunByID(racunUpdateDTO.IDRacun);
                    if(oldRacun == null) 
                        return NotFound();
                    KorisnikEntity? kupac = korisnikRepository.GetKorisnikByID(int.Parse(HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.NameIdentifier)?.Value));

                    if(kupac != null)
                    {
                        racunUpdateDTO.IDKupac = kupac.IDKorisnik;
                        RacunEntity racun = mapper.Map<RacunEntity>(racunUpdateDTO);
                        mapper.Map(racun, oldRacun);

                        racunRepository.SaveChanges();

                        racun.Kupac = kupac;

                        return Ok(mapper.Map<RacunDTO>(racun));
                    }
                    else
                        return StatusCode(StatusCodes.Status422UnprocessableEntity, "Neki od starnih kljuceva nedostaje!");

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
