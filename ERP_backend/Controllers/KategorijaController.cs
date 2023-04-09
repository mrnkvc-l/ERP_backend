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
    [Route("api/kategorije")]
    [Produces("application/json", "application/xml")]
    public class KategorijaController : ControllerBase
    {
        private readonly IKategorijaRepository kategorijaRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public KategorijaController(IKategorijaRepository kategorijaRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.kategorijaRepository = kategorijaRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        [AllowAnonymous]
        [HttpHead]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<KategorijaDTO>> GetAllKategorije()
        {
            try
            {
                List<KategorijaEntity> kategorije = kategorijaRepository.GetAllKategorije();

                if (kategorije == null || kategorije.Count == 0)
                    return NoContent();

                List<KategorijaDTO> kategorijeDTO = mapper.Map<List<KategorijaDTO>>(kategorije);

                return Ok(kategorijeDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet("{kategorijaID}")]
        public ActionResult<KategorijaDTO> GetKategorijaByID(int kategorijaID)
        {
            try
            {
                KategorijaEntity? kategorija = kategorijaRepository.GetKategorijaByID(kategorijaID);

                if (kategorija == null)
                    return NoContent();

                KategorijaDTO kategorijaDTO = mapper.Map<KategorijaDTO>(kategorija);

                return Ok(kategorijaDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Consumes("application/json")]
        public ActionResult<KategorijaDTO> CreateKategorija([FromBody] KategorijaCreateDTO kategorijaCreateDTO)
        {
            try
            {
                if (HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Role)?.Value == "ADMIN")
                {
                    List<KategorijaEntity> kategorije = kategorijaRepository.GetAllKategorije();
                    if (kategorije.Find(e => e.Naziv == kategorijaCreateDTO.Naziv) == null)
                    {
                        KategorijaDTO kategorijaDTO = kategorijaRepository.CreateKategorija(kategorijaCreateDTO);
                        kategorijaRepository.SaveChanges();

                        return Ok("Uspesno kreirana kategorija!");
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status422UnprocessableEntity, "Vec postoji kategorija sa istim nazivom!");
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

        [HttpDelete("{kategorijaID}")]
        public IActionResult DeleteKategorija(int kategorijaId)
        {
            try
            {
                if (HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Role)?.Value == "ADMIN")
                {
                    KategorijaEntity? kategorija = kategorijaRepository.GetKategorijaByID(kategorijaId);
                    if (kategorija == null)
                        return NotFound();

                    kategorijaRepository.DeleteKategorija(kategorijaId);
                    kategorijaRepository.SaveChanges();

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
        public ActionResult<KategorijaDTO> UpdateKategorija([FromBody] KategorijaUpdateDTO kategorijaUpdateDTO)
        {
            try
            {
                if (HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Role)?.Value == "ADMIN")
                {
                    KategorijaEntity? oldKategorija = kategorijaRepository.GetKategorijaByID(kategorijaUpdateDTO.IDKategorija);

                    if (oldKategorija == null)
                        return NotFound();

                    KategorijaEntity kategorija = mapper.Map<KategorijaEntity>(kategorijaUpdateDTO);
                    mapper.Map(kategorija, oldKategorija);
                    kategorijaRepository.SaveChanges();

                    return Ok(mapper.Map<KategorijaDTO>(kategorija));
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
