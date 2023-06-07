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
        private readonly IInfoRepository infoRepository;
        private readonly IProizvodjacRepository proizvodjacRepository;
        private readonly IKategorijaRepository kategorijaRepository;
        private readonly IKolekcijaRepository kolekcijaRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public SlikaController(ISlikaRepository slikaRepository, LinkGenerator linkGenerator, IMapper mapper, IInfoRepository infoRepository, IProizvodjacRepository proizvodjacRepository, IKategorijaRepository kategorijaRepository, IKolekcijaRepository kolekcijaRepository)
        {
            this.slikaRepository = slikaRepository;
            this.infoRepository = infoRepository;
            this.proizvodjacRepository = proizvodjacRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.kategorijaRepository = kategorijaRepository;
            this.kolekcijaRepository = kolekcijaRepository;
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

                List<SlikaDTO> slikeDTO =new();

                foreach(SlikaEntity slika in slike)
                {
                    SlikaDTO slikaDTO = mapper.Map<SlikaDTO>(slika);

                    slikaDTO.Proizvod = mapper.Map<InfoDTO>(infoRepository.GetInfoByID(slika.IDInfo));

                    slikaDTO.Proizvod.Proizvodjac = mapper.Map<ProizvodjacDTO>(proizvodjacRepository.GetProizvodjacByID(slika.Info.IDProizvodjac));
                    slikaDTO.Proizvod.Kategorija = mapper.Map<KategorijaDTO>(kategorijaRepository.GetKategorijaByID(slika.Info.IDKategorija));
                    slikaDTO.Proizvod.Kolekcija = mapper.Map<KolekcijaDTO>(kolekcijaRepository.GetKolekcijaByID(slika.Info.IDKolekcija));

                    slikeDTO.Add(slikaDTO);
                }

                return Ok(slikeDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet("{proizvodID}")]
        public ActionResult<List<SlikaDTO>> GetSlikaByID(int proizvodID)
        {
            try
            {
                List<SlikaEntity> slike = slikaRepository.GetSlikeByProizvod(proizvodID);

                if (slike == null || slike.Count == 0)
                    return NoContent();

                List<SlikaDTO> slikeDTO = new();

                foreach (SlikaEntity slika in slike)
                {
                    SlikaDTO slikaDTO = mapper.Map<SlikaDTO>(slika);

                    slikaDTO.Proizvod = mapper.Map<InfoDTO>(infoRepository.GetInfoByID(slika.IDInfo));

                    slikaDTO.Proizvod.Proizvodjac = mapper.Map<ProizvodjacDTO>(proizvodjacRepository.GetProizvodjacByID(slika.Info.IDProizvodjac));
                    slikaDTO.Proizvod.Kategorija = mapper.Map<KategorijaDTO>(kategorijaRepository.GetKategorijaByID(slika.Info.IDKategorija));
                    slikaDTO.Proizvod.Kolekcija = mapper.Map<KolekcijaDTO>(kolekcijaRepository.GetKolekcijaByID(slika.Info.IDKolekcija));

                    slikeDTO.Add(slikaDTO);
                }

                return Ok(slikeDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        /*
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
        }*/


        [HttpPost]
        public JsonResult SaveFile([FromForm] SlikaCreateDTO model, [FromServices] IWebHostEnvironment webHostEnvironment, [FromServices] ErpContext dbContext)
        {
            try
            {
                if (HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Role)?.Value == "ADMIN")
                {
                    if (model.File != null && model.File.Length > 0)
                    {
                        string filename = Path.GetFileName(model.File.FileName);
                        string physicalPath = Path.Combine(webHostEnvironment.ContentRootPath, "Photos", filename);

                        using (var stream = new FileStream(physicalPath, FileMode.Create))
                        {
                            model.File.CopyTo(stream);
                        }

                        physicalPath = Path.Combine("http://localhost:5164/", "Photos", filename);

                        // Store the file location, name, and idInfo in the database table
                        var slika = new SlikaEntity { Adresa = physicalPath, Naziv = filename, IDInfo = model.IDInfo };
                        dbContext.Slike.Add(slika);
                        dbContext.SaveChanges();

                        return new JsonResult(filename);
                    }
                    else
                    {
                        return new JsonResult("No file received");
                    }
                }
                else
                {
                    return new JsonResult("Access forbidden");
                }
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.ToString());
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
    }
}
