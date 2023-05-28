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
    [Route("api/infoi")]
    [Produces("application/json", "application/xml")]
    public class InfoController : ControllerBase
    {
        private readonly IInfoRepository infoRepository;
        private readonly IKategorijaRepository kategorijaRepository;
        private readonly IKolekcijaRepository kolekcijaRepository;
        private readonly IProizvodjacRepository proizvodjacRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment webHostEnvironment;

        public InfoController(IWebHostEnvironment webHostEnvironment, IInfoRepository infoRepository, LinkGenerator linkGenerator, IMapper mapper, IKategorijaRepository kategorijaRepository, IKolekcijaRepository kolekcijaRepository, IProizvodjacRepository proizvodjacRepository)
        {
            this.infoRepository = infoRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.kategorijaRepository = kategorijaRepository;
            this.kolekcijaRepository = kolekcijaRepository;
            this.proizvodjacRepository = proizvodjacRepository;
            this.webHostEnvironment = webHostEnvironment;
        }

        [AllowAnonymous]
        [HttpHead]
        [HttpGet]
        public ActionResult<List<InfoDTO>> GetAllInfos()
        {
            try
            {
                List<InfoEntity> infos = infoRepository.GetAllInfo();

                if (infos == null || infos.Count == 0)
                    return NoContent();

                List<InfoDTO> infoiDTO = new();

                foreach (InfoEntity info in infos)
                {
                    InfoDTO infoDTO = mapper.Map<InfoDTO>(info);

                    infoDTO.Proizvodjac = mapper.Map<ProizvodjacDTO>(proizvodjacRepository.GetProizvodjacByID(info.IDProizvodjac));
                    infoDTO.Kategorija = mapper.Map<KategorijaDTO>(kategorijaRepository.GetKategorijaByID(info.IDKategorija));
                    infoDTO.Kolekcija = mapper.Map<KolekcijaDTO>(kolekcijaRepository.GetKolekcijaByID(info.IDKolekcija));

                    infoiDTO.Add(infoDTO);
                }

                return Ok(infoiDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet("{infoID}")]
        public ActionResult<InfoDTO> GetInfoByID(int infoID)
        {
            try
            {
                InfoEntity? info = infoRepository.GetInfoByID(infoID);

                if (info == null)
                    return NoContent();

                InfoDTO infoDTO = mapper.Map<InfoDTO>(info);

                infoDTO.Proizvodjac = mapper.Map<ProizvodjacDTO>(proizvodjacRepository.GetProizvodjacByID(info.IDProizvodjac));
                infoDTO.Kategorija = mapper.Map<KategorijaDTO>(kategorijaRepository.GetKategorijaByID(info.IDKategorija));
                infoDTO.Kolekcija = mapper.Map<KolekcijaDTO>(kolekcijaRepository.GetKolekcijaByID(info.IDKolekcija));

                return Ok(infoDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Consumes("application/json")]
        public ActionResult<InfoDTO> CreateInfo(InfoCreateDTO infoCreateDTO)
        {
            try
            {
                if (HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Role)?.Value == "ADMIN")
                {
                    List<InfoEntity> infos = infoRepository.GetAllInfo();

                    if (infos.Find(e => e.Naziv == infoCreateDTO.Naziv) == null)
                    {
                        KategorijaEntity? kategorija = kategorijaRepository.GetKategorijaByID(infoCreateDTO.IDKategorija);
                        KolekcijaEntity? kolekcija = kolekcijaRepository.GetKolekcijaByID(infoCreateDTO.IDKolekcija);
                        ProizvodjacEntity? proizvodjac = proizvodjacRepository.GetProizvodjacByID(infoCreateDTO.IDProizvodjac);

                        if(kategorija == null || proizvodjac == null || kolekcija == null)
                            return StatusCode(StatusCodes.Status422UnprocessableEntity, "Neki od starnih kljuceva nedostaje!");
                        else
                        {
                            InfoDTO infoDTO = infoRepository.CreateInfo(infoCreateDTO);
                            infoRepository.SaveChanges();

                            infoDTO.Kategorija = mapper.Map<KategorijaDTO>(kategorija);
                            infoDTO.Kolekcija = mapper.Map<KolekcijaDTO>(kolekcija);
                            infoDTO.Proizvodjac = mapper.Map<ProizvodjacDTO>(proizvodjac);

                            return Ok("Uspesno kreirana informacija!");
                        }
                    }
                    else
                        return StatusCode(StatusCodes.Status422UnprocessableEntity, "Vec postoji informacija sa istim nazivom.");

                }
                else
                    return StatusCode(StatusCodes.Status403Forbidden, "Access forbiden");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{infoID}")]
        public IActionResult DeleteInfo(int infoID)
        {
            try
            {
                if (HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Role)?.Value == "ADMIN")
                {
                    InfoEntity? info = infoRepository.GetInfoByID(infoID);
                    if (info == null)
                        return NotFound();

                    infoRepository.DeleteInfo(infoID);
                    infoRepository.SaveChanges();

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
        public ActionResult<InfoDTO> UpdateInfo([FromBody] InfoUpdateDTO infoUpdateDTO)
        {
            try
            {
                if (HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Role)?.Value == "ADMIN")
                {
                    InfoEntity? oldInfo = infoRepository.GetInfoByID(infoUpdateDTO.IDInfo);
                    if (oldInfo == null)
                        return NotFound();

                    KategorijaEntity? kategorija = kategorijaRepository.GetKategorijaByID(infoUpdateDTO.IDKategorija);
                    KolekcijaEntity? kolekcija = kolekcijaRepository.GetKolekcijaByID(infoUpdateDTO.IDKolekcija);
                    ProizvodjacEntity? proizvodjac = proizvodjacRepository.GetProizvodjacByID(infoUpdateDTO.IDProizvodjac);

                    if (kategorija == null || proizvodjac == null || kolekcija == null)
                        return StatusCode(StatusCodes.Status422UnprocessableEntity, "Neki od starnih kljuceva nedostaje!");
                    else
                    {
                        InfoEntity info = mapper.Map<InfoEntity>(infoUpdateDTO);
                        mapper.Map(info, oldInfo);
                        infoRepository.SaveChanges();

                        info.Kategorija = kategorija;
                        info.Kolekcija= kolekcija;
                        info.Proizvodjac= proizvodjac;

                        return Ok(mapper.Map<InfoDTO>(info));
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

        [HttpPost]
        public JsonResult SaveFile()
        {
            try
            {
                if (HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Role)?.Value == "ADMIN")
                {
                    var httpRequest = Request.Form;
                    var postedFile = httpRequest.Files[0];
                    string filename = postedFile.FileName;
                    var physicalPath = webHostEnvironment.ContentRootPath + "/Photos/" + filename;

                    using (var stream = new FileStream(physicalPath, FileMode.Create))
                    {
                        postedFile.CopyTo(stream);
                    }

                    return new JsonResult(filename);
                }
                else
                    return new JsonResult("Access forbiden");
            }
            catch(Exception ex)
            {
                return new JsonResult(ex.ToString());
            }
        }
    }
}
