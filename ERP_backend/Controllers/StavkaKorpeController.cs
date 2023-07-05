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
    [Route("api/stavke")]
    [Produces("application/json", "application/xml")]
    public class StavkaKorpeController : ControllerBase
    {
        private readonly IStavkaKorpeRepository stavkaKorpeRepository;
        private readonly IKorisnikRepository korisnikRepository;
        private readonly IProizvodRepository proizvodRepository;
        private readonly IInfoRepository infoRepository;
        private readonly IVelicinaRepository velicinaRepository;
        private readonly IKategorijaRepository kategorijaRepository;
        private readonly IKolekcijaRepository kolekcijaRepository;
        private readonly IProizvodjacRepository proizvodjacRepository;
        private LinkGenerator linkGenerator;
        private IMapper mapper;

        public StavkaKorpeController(IInfoRepository infoRepository, IVelicinaRepository velicinaRepository, IKategorijaRepository kategorijaRepository, IKolekcijaRepository kolekcijaRepository, IProizvodjacRepository proizvodjacRepository, IProizvodRepository proizvodRepository, IStavkaKorpeRepository stavkaKorpeRepository, IKorisnikRepository korisnikRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.stavkaKorpeRepository = stavkaKorpeRepository;
            this.proizvodRepository = proizvodRepository;
            this.infoRepository = infoRepository;
            this.kategorijaRepository = kategorijaRepository;
            this.korisnikRepository = korisnikRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.kolekcijaRepository = kolekcijaRepository;
            this.velicinaRepository= velicinaRepository;
            this.proizvodjacRepository= proizvodjacRepository;
        }

        
        [HttpGet]
        public ActionResult<List<StavkaKorpeDTO>> GetAllStavkaKorpe()
        {
            try
            {
                KorisnikEntity? korisnik = korisnikRepository.GetKorisnikByID(Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.NameIdentifier)?.Value));

                if (korisnik != null)
                {
                    List<StavkaKorpeEntity> stavkeKorpe = stavkaKorpeRepository.GetAllStavkeKorpe(korisnik.IDKorisnik);

                    if (stavkeKorpe == null || stavkeKorpe.Count == 0)
                        return NotFound();

                    List<StavkaKorpeDTO> stavkeKorpeDTO = new();

                    foreach(StavkaKorpeEntity stavka in stavkeKorpe)
                    {
                        StavkaKorpeDTO stavkaDTO = mapper.Map<StavkaKorpeDTO>(stavka);

                        stavkaDTO.Kupac = mapper.Map<KorisnikDTO>(korisnik);
                        stavkaDTO.Proizvod = mapper.Map<ProizvodDTO>(proizvodRepository.GetProizvodByID(stavka.IDProizvod));
                        stavkaDTO.Proizvod.ProizvodInfo = mapper.Map<InfoDTO>(infoRepository.GetInfoByID(stavka.Proizvod.IDProizvodInfo));
                        stavkaDTO.Proizvod.Velicina = mapper.Map<VelicinaDTO>(velicinaRepository.GetVelicinaByID(stavka.Proizvod.IDVelicina));
                        stavkaDTO.Proizvod.ProizvodInfo.Proizvodjac = mapper.Map<ProizvodjacDTO>(proizvodjacRepository.GetProizvodjacByID(stavka.Proizvod.Info.IDProizvodjac));
                        stavkaDTO.Proizvod.ProizvodInfo.Kategorija = mapper.Map<KategorijaDTO>(kategorijaRepository.GetKategorijaByID(stavka.Proizvod.Info.IDKategorija));
                        stavkaDTO.Proizvod.ProizvodInfo.Kolekcija = mapper.Map<KolekcijaDTO>(kolekcijaRepository.GetKolekcijaByID(stavka.Proizvod.Info.IDKolekcija));

                        stavkeKorpeDTO.Add(stavkaDTO);
                    }

                    return Ok(stavkeKorpeDTO);
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
        public ActionResult<StavkaKorpeDTO> PostStavkaKorpe([FromBody]StavkaKorpeCreateDTO stavkaKorpeCreateDTO)
        {
            try
            {
                stavkaKorpeCreateDTO.IDKupac = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.NameIdentifier)?.Value);

                ProizvodEntity? proizvod = proizvodRepository.GetProizvodByID(stavkaKorpeCreateDTO.IDProizvod);

                KorisnikEntity? korisnik = korisnikRepository.GetKorisnikByID(stavkaKorpeCreateDTO.IDKupac);

                if (korisnik == null)
                    return StatusCode(StatusCodes.Status403Forbidden, "Morate se prijaviti.");

                if (proizvod == null)
                    return StatusCode(StatusCodes.Status422UnprocessableEntity, "Neki od starnih kljuceva nedostaje!");
                else
                {
                    StavkaKorpeDTO stavkaKorpeDTO = stavkaKorpeRepository.CreateStavkaKorpe(stavkaKorpeCreateDTO);
                    stavkaKorpeRepository.SaveChanges();

                    stavkaKorpeDTO.Kupac = mapper.Map<KorisnikDTO>(korisnik);
                    stavkaKorpeDTO.Proizvod = mapper.Map<ProizvodDTO>(proizvod);

                    return Ok("Uspesno kreirana stavka korpe!");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpDelete("{proizvodID}")]
        public IActionResult DeleteStavka(int proizvodID)
        {
            try
            {

                KorisnikEntity? korisnik = korisnikRepository.GetKorisnikByID(Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.NameIdentifier)?.Value));
                ProizvodEntity? proizvod = proizvodRepository.GetProizvodByID(proizvodID);

                if (korisnik == null)
                    return StatusCode(StatusCodes.Status409Conflict, "Doslo je do grekse.");

                if (proizvod == null)
                    return StatusCode(StatusCodes.Status404NotFound, "Nije pronadjen proizvod.");

                stavkaKorpeRepository.DeleteStavkaKorpe(proizvodID, korisnik.IDKorisnik);
                stavkaKorpeRepository.SaveChanges();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
    }
}
