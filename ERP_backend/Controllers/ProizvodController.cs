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
    [Route("api/proizvodi")]
    public class ProizvodController : ControllerBase
    {
        private readonly IProizvodRepository proizvodRepository;
        private readonly IProizvodjacRepository proizvodjacRepository;
        private readonly IInfoRepository infoRepository;
        private readonly IKategorijaRepository kategorijaRepository;
        private readonly IKolekcijaRepository kolekcijaRepository;
        private readonly IVelicinaRepository velicinaRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public ProizvodController(IProizvodRepository proizvodRepository, IProizvodjacRepository proizvodjacRepository, IInfoRepository infoRepository, IKategorijaRepository kategorijaRepository, IKolekcijaRepository kolekcijaRepository, IVelicinaRepository velicinaRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.proizvodRepository = proizvodRepository;
            this.proizvodjacRepository = proizvodjacRepository;
            this.infoRepository = infoRepository;
            this.kategorijaRepository = kategorijaRepository;
            this.kolekcijaRepository = kolekcijaRepository;
            this.velicinaRepository = velicinaRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        [AllowAnonymous]
        [HttpHead]
        [HttpGet]
        public ActionResult<List<ProizvodDTO>> GetAllProizvodi()
        {
            try
            {
                List<ProizvodEntity> proizvodi = proizvodRepository.GetAllProizvodi();
                if (proizvodi == null || proizvodi.Count == 0)
                    return NoContent();

                List<ProizvodDTO> proizvodiDTO = new();

                foreach (ProizvodEntity proizvod in proizvodi)
                {
                    ProizvodDTO proizvodDTO = mapper.Map<ProizvodDTO>(proizvod);

                    proizvodDTO.Proizvodjac = mapper.Map<ProizvodjacDTO>(proizvodjacRepository.GetProizvodjacByID(proizvod.IDProizvodjac));
                    proizvodDTO.ProizvodInfo = mapper.Map<InfoDTO>(infoRepository.GetInfoByID(proizvod.IDProizvodInfo));
                    proizvodDTO.Kategorija = mapper.Map<KategorijaDTO>(kategorijaRepository.GetKategorijaByID(proizvod.IDKategorija));
                    proizvodDTO.Kolekcija = mapper.Map<KolekcijaDTO>(kolekcijaRepository.GetKolekcijaByID(proizvod.IDKolekcija));
                    proizvodDTO.Velicina = mapper.Map<VelicinaDTO>(velicinaRepository.GetVelicinaByID(proizvod.IDVelicina));

                    proizvodiDTO.Add(proizvodDTO);
                }
                return Ok(proizvodiDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet("{proizvodID}")]
        public ActionResult<ProizvodDTO> GetProizvodByID(int proizvodID)
        {
            try
            {
                ProizvodEntity? proizvod = proizvodRepository.GetProizvodByID(proizvodID);
                if (proizvod == null)
                    return NoContent();

                ProizvodDTO proizvodDTO = mapper.Map<ProizvodDTO>(proizvod);

                proizvodDTO.Proizvodjac = mapper.Map<ProizvodjacDTO>(proizvodjacRepository.GetProizvodjacByID(proizvod.IDProizvodjac));
                proizvodDTO.ProizvodInfo = mapper.Map<InfoDTO>(infoRepository.GetInfoByID(proizvod.IDProizvodInfo));
                proizvodDTO.Kategorija = mapper.Map<KategorijaDTO>(kategorijaRepository.GetKategorijaByID(proizvod.IDKategorija));
                proizvodDTO.Kolekcija = mapper.Map<KolekcijaDTO>(kolekcijaRepository.GetKolekcijaByID(proizvod.IDKolekcija));
                proizvodDTO.Velicina = mapper.Map<VelicinaDTO>(velicinaRepository.GetVelicinaByID(proizvod.IDVelicina));

                return Ok(proizvodDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Consumes("application/json")]
        public ActionResult<ProizvodDTO> CreateProizvod([FromBody] ProizvodCreateDTO proizvodCreateDTO)
        {
            try
            {
                if (HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Role)?.Value == "ADMIN")
                {
                    ProizvodjacEntity? proizvodjac = proizvodjacRepository.GetProizvodjacByID(proizvodCreateDTO.IDProizvodjac);
                    KategorijaEntity? kategorija = kategorijaRepository.GetKategorijaByID(proizvodCreateDTO.IDKategorija);
                    KolekcijaEntity? kolekcija = kolekcijaRepository.GetKolekcijaByID(proizvodCreateDTO.IDKolekcija);
                    InfoEntity? info = infoRepository.GetInfoByID(proizvodCreateDTO.IDProizvodInfo);
                    VelicinaEntity? velicina = velicinaRepository.GetVelicinaByID(proizvodCreateDTO.IDVelicina);

                    if (proizvodjac == null || kategorija == null || kolekcija == null || info == null || velicina == null)
                        return StatusCode(StatusCodes.Status422UnprocessableEntity, "Neki od starnih kljuceva nedostaje!");
                    else
                    {
                        ProizvodDTO proizvodDTO = proizvodRepository.CreateProizvod(proizvodCreateDTO);
                        proizvodRepository.SaveChanges();

                        proizvodDTO.Proizvodjac = mapper.Map<ProizvodjacDTO>(proizvodjac);
                        proizvodDTO.Kategorija = mapper.Map<KategorijaDTO>(kategorija);
                        proizvodDTO.Kolekcija = mapper.Map<KolekcijaDTO>(kolekcija);
                        proizvodDTO.ProizvodInfo = mapper.Map<InfoDTO>(info);
                        proizvodDTO.Velicina = mapper.Map<VelicinaDTO>(velicina);

                        return Ok("Proizvod uspesno kreiran!");
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

        [HttpDelete("{proizvodID}")]
        public IActionResult DeleteProizvod(int proizvodID)
        {
            try
            {
                if (HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Role)?.Value == "ADMIN")
                {
                    ProizvodEntity? proizvod = proizvodRepository.GetProizvodByID(proizvodID);
                    if (proizvod == null)
                        return NotFound();

                    proizvodRepository.DeleteProizvod(proizvodID);
                    proizvodRepository.SaveChanges();

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
        public ActionResult<ProizvodDTO> UpdateProizvod([FromBody] ProizvodUpdateDTO proizvodUpdateDTO)
        {
            try
            {
                if (HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Role)?.Value == "ADMIN")
                {
                    ProizvodEntity? oldProizvod = proizvodRepository.GetProizvodByID(proizvodUpdateDTO.IDProizvod);
                    if (oldProizvod == null)
                        return NotFound();

                    ProizvodjacEntity? proizvodjac = proizvodjacRepository.GetProizvodjacByID(proizvodUpdateDTO.IDProizvodjac);
                    KategorijaEntity? kategorija = kategorijaRepository.GetKategorijaByID(proizvodUpdateDTO.IDKategorija);
                    KolekcijaEntity? kolekcija = kolekcijaRepository.GetKolekcijaByID(proizvodUpdateDTO.IDKolekcija);
                    InfoEntity? info = infoRepository.GetInfoByID(proizvodUpdateDTO.IDProizvodInfo);
                    VelicinaEntity? velicina = velicinaRepository.GetVelicinaByID(proizvodUpdateDTO.IDVelicina);

                    if (proizvodjac == null || kategorija == null || kolekcija == null || info == null || velicina == null)
                        return StatusCode(StatusCodes.Status422UnprocessableEntity, "Neki od starnih kljuceva nedostaje!");
                    else
                    {
                        ProizvodEntity proizvod = mapper.Map<ProizvodEntity>(proizvodUpdateDTO);
                        mapper.Map(proizvod, oldProizvod);
                        proizvodRepository.SaveChanges();

                        proizvod.Proizvodjac = proizvodjac;
                        proizvod.Kategorija = kategorija;
                        proizvod.Kolekcija = kolekcija;
                        proizvod.Info = info;
                        proizvod.Velicina = velicina;

                        return Ok("Proizvod uspesno kreiran!");
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
    }
}
