using AutoMapper;
using ERP_backend.Entity;
using ERP_backend.Model;
using ERP_backend.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Xml.Linq;

namespace ERP_backend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/korisnici")]
    [Produces("application/json", "application/xml")]
    public class KorisnikController : ControllerBase
    {
        private IKorisnikRepository korisnikRepository;
        private LinkGenerator linkGenerator;
        private IMapper mapper;

        public KorisnikController(IKorisnikRepository korisnikRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.korisnikRepository = korisnikRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        [HttpHead]
        [HttpGet]
        public ActionResult<List<KorisnikDTO>> GetAllKorisnici()
        {
            try
            {
                if (HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Role)?.Value == "ADMIN")
                {
                    List<KorisnikEntity> korisnici = korisnikRepository.GetAllKorisnici();

                    if (korisnici == null || korisnici.Count == 0)
                        return NoContent();

                    List<KorisnikDTO> korisniciDTO = mapper.Map<List<KorisnikDTO>>(korisnici);
                    return Ok(korisniciDTO);
                }
                else
                    return StatusCode(StatusCodes.Status403Forbidden, "Access forbiden");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{korisnikID}")]
        public ActionResult<List<KorisnikDTO>> GetKorisnikByID(int korisnikID)
        {
            try
            {
                if (HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Role)?.Value == "ADMIN" ||
                    HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.NameIdentifier)?.Value == korisnikID.ToString())
                {
                    KorisnikEntity? korisnik = korisnikRepository.GetKorisnikByID(korisnikID);

                    if (korisnik == null)
                        return NotFound();

                    KorisnikDTO korisnikDTO = mapper.Map<KorisnikDTO>(korisnik);
                    return Ok(korisnikDTO);
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
        [HttpPost]
        [Consumes("application/json")]
        public ActionResult<KorisnikDTO> CreateKorinsik([FromBody] KorisnikCreateDTO korisnikCreateDTO)
        {
            try
            {
                List<KorisnikEntity> korisnici = korisnikRepository.GetAllKorisnici();
                if (korisnici.Find(e => e.Username == korisnikCreateDTO.Username) == null &&
                    korisnici.Find(e => e.Email == korisnikCreateDTO.Email) == null)
                {

                    KorisnikDTO korisnikDTO = korisnikRepository.CreateKorisnik(korisnikCreateDTO);
                    korisnikRepository.SaveChanges();

                    return Ok("Uspesno unet novi korisnik!");
                }
                else
                {
                    return StatusCode(StatusCodes.Status422UnprocessableEntity, "Vec postoji korisnik sa istim username-om ili emailom!");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{korisnikID}")]
        public IActionResult DeleteKorisnik(int korisnikID)
        {
            try
            {
                if (HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Role)?.Value == "ADMIN" ||
                    HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.NameIdentifier)?.Value == korisnikID.ToString())
                {
                    KorisnikEntity? korisnik = korisnikRepository.GetKorisnikByID(korisnikID);
                    if (korisnik == null)
                        return NotFound();

                    korisnikRepository.DeleteKorisnik(korisnikID);
                    korisnikRepository.SaveChanges();

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
        public ActionResult<KorisnikDTO> UpdateKorinsik(KorisnikUpdateDTO korisnikUpdateDTO)
        {
            try
            {
                
                if (HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Role)?.Value == "ADMIN" ||
                    HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.NameIdentifier)?.Value == korisnikUpdateDTO.IDKorisnik.ToString())
                {
                    if (korisnikRepository.GetKorisnikByID(korisnikUpdateDTO.IDKorisnik) == null)
                        return NotFound();

                    List<KorisnikEntity> users = korisnikRepository.GetAllKorisnici();

                    KorisnikEntity? tempUser = users.Find(e => e.IDKorisnik == korisnikUpdateDTO.IDKorisnik);

                    if (tempUser != null)
                        users.Remove(tempUser);

                    if (users.Find(e => e.Username == korisnikUpdateDTO.Username) == null &&
                        users.Find(e => e.Email == korisnikUpdateDTO.Email) == null)
                    {
                        KorisnikDTO usersDTO = korisnikRepository.UpdateKorisnik(korisnikUpdateDTO);
                        korisnikRepository.SaveChanges();

                        return Ok(usersDTO);
                    }
                    else
                        return StatusCode(StatusCodes.Status422UnprocessableEntity, "Username or email already exists.");
                }
                else
                {
                    return StatusCode(StatusCodes.Status403Forbidden, "Access forbidden.");
                }
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, exception.Message);
            }
        }

    }
}
