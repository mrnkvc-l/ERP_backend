using AutoMapper;
using ERP_backend.Entity;
using ERP_backend.Model;
using ERP_backend.Repositories;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ERP_backend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/kolekcije")]
    [Produces("application/json", "application/xml")]
    public class KolekcijaController : ControllerBase
    {
        private IKolekcijaRepository kolekcijaRepository;
        private LinkGenerator linkGenerator;
        private IMapper mapper;

        public KolekcijaController(IKolekcijaRepository kolekcijaRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.kolekcijaRepository = kolekcijaRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        [AllowAnonymous]
        [HttpHead]
        [HttpGet]
        public ActionResult<List<KolekcijaDTO>> GetAllKolekcije()
        {
            try
            {
                List<KolekcijaEntity> kolekcije = kolekcijaRepository.GetAllKolekcije();

                if (kolekcije == null || kolekcije.Count == 0)
                    return NoContent();

                List<KolekcijaDTO> kolekcijeDTO = mapper.Map<List<KolekcijaDTO>>(kolekcije);
                return Ok(kolekcijeDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet("{kolekcijaID}")]
        public ActionResult<KolekcijaDTO> GetKolekcijaByID(int kolekcijaID)
        {
            try
            {
                KolekcijaEntity? kolekcija = kolekcijaRepository.GetKolekcijaByID(kolekcijaID);

                if (kolekcija == null)
                    return NoContent();

                KolekcijaDTO kolekcijaDTO = mapper.Map<KolekcijaDTO>(kolekcija);

                return Ok(kolekcijaDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Consumes("application/json")]
        public ActionResult<KolekcijaDTO> CreateKolekcija([FromBody] KolekcijaCreateDTO kolekcijaCreateDTO)
        {
            try
            {
                if (HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Role)?.Value == "ADMIN")
                {
                    List<KolekcijaEntity> kolekcije = kolekcijaRepository.GetAllKolekcije();
                    if (kolekcije.Find(e => e.Naziv == kolekcijaCreateDTO.Naziv) == null)
                    {
                        KolekcijaDTO kolekcijaDTO = kolekcijaRepository.CreateKolekcija(kolekcijaCreateDTO);
                        kolekcijaRepository.SaveChanges();

                        return Ok("Uspesno uneta kolekcija!");
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status422UnprocessableEntity, "Vec postoji kolekcija sa istim nazivom!");
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

        [HttpDelete("{kolekcijaID}")]
        public IActionResult DeleteKolekcija(int kolekcijaID)
        {
            try
            {
                if (HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Role)?.Value == "ADMIN")
                {
                    KolekcijaEntity? kolekcija = kolekcijaRepository.GetKolekcijaByID(kolekcijaID);
                    if (kolekcija == null)
                        return NotFound();

                    kolekcijaRepository.DeleteKolekcija(kolekcijaID);
                    kolekcijaRepository.SaveChanges();

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
        public ActionResult<KolekcijaDTO> UpdateKolekcija(KolekcijaUpdateDTO kolekcijaUpdateDTO)
        {
            try
            {
                if (HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Role)?.Value == "ADMIN")
                {
                    KolekcijaEntity? oldKolekcija = kolekcijaRepository.GetKolekcijaByID(kolekcijaUpdateDTO.IDKolekcija);
                    if (oldKolekcija == null)
                        return NotFound();

                    KolekcijaEntity kolekcija = mapper.Map<KolekcijaEntity>(kolekcijaUpdateDTO);
                    mapper.Map(kolekcija, oldKolekcija);
                    kolekcijaRepository.SaveChanges();
                    return Ok(mapper.Map<KolekcijaDTO>(kolekcija));
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
