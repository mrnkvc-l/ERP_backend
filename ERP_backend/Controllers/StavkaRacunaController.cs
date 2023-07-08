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
    [Route("api/sracuna")]
    [Produces("application/json", "application/xml")]
    public class StavkaRacunaController : ControllerBase
    {
        private readonly IStavkaRacunaRepository stavkaRacunaRepository;
        private readonly IRacunRepository racunRepository;
        private readonly IProizvodRepository proizvodRepository;
        private readonly IVelicinaRepository velicinaRepository;
        private readonly IInfoRepository infoRepository;
        private readonly IProizvodjacRepository proizvodjacRepository;
        private readonly IKategorijaRepository kategorijaRepository;
        private readonly IKolekcijaRepository kolekcijaRepository;
        private readonly IKorisnikRepository korisnikRepository;
        private readonly IMapper mapper;

        public StavkaRacunaController(IStavkaRacunaRepository stavkaRacunaRepository, IRacunRepository racunRepository,IProizvodRepository proizvodRepository, IVelicinaRepository velicinaRepository, IMapper mapper, IInfoRepository infoRepository, IProizvodjacRepository proizvodjacRepository, IKategorijaRepository kategorijaRepository, IKolekcijaRepository kolekcijaRepository, IKorisnikRepository korisnikRepository)
        {
            this.stavkaRacunaRepository = stavkaRacunaRepository;
            this.racunRepository = racunRepository;
            this.proizvodRepository = proizvodRepository;
            this.velicinaRepository = velicinaRepository;
            this.mapper = mapper;
            this.infoRepository = infoRepository;
            this.proizvodjacRepository = proizvodjacRepository;
            this.kategorijaRepository = kategorijaRepository;
            this.kolekcijaRepository = kolekcijaRepository;
            this.korisnikRepository = korisnikRepository;
        }

        [AllowAnonymous]
        [HttpGet("{racunID}")]
        public ActionResult<List<StavkaRacunaDTO>> GetAllStavkeRacuna(int racunID)
        {
            try
            {
                if (HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.NameIdentifier)?.Value != null)
                {
                    List<StavkaRacunaEntity> stavkeRacuna = stavkaRacunaRepository.GetAllStavkeRacuna(racunID);

                    if (stavkeRacuna == null || stavkeRacuna.Count == 0)
                        return NoContent();

                    List<StavkaRacunaDTO>stavkeRacunaDTO = new();

                    foreach(StavkaRacunaEntity stavka in stavkeRacuna)
                    {
                        StavkaRacunaDTO stavkaDTO = mapper.Map<StavkaRacunaDTO>(stavka);

                        stavkaDTO.Racun = mapper.Map<RacunDTO>(racunRepository.GetRacunByID(stavka.IDRacun));
                        stavkaDTO.Proizvod = mapper.Map<ProizvodDTO>(proizvodRepository.GetProizvodByID(stavka.IDProizvod));

                        stavkaDTO.Racun.Kupac = mapper.Map<KorisnikDTO>(korisnikRepository.GetKorisnikByID(stavka.Racun.IDKupac));
                        stavkaDTO.Proizvod.Velicina = mapper.Map<VelicinaDTO>(velicinaRepository.GetVelicinaByID(stavka.Proizvod.IDVelicina));
                        stavkaDTO.Proizvod.ProizvodInfo = mapper.Map<InfoDTO>(infoRepository.GetInfoByID(stavka.Proizvod.IDProizvodInfo));

                        stavkaDTO.Proizvod.ProizvodInfo.Proizvodjac = mapper.Map<ProizvodjacDTO>(proizvodjacRepository.GetProizvodjacByID(stavka.Proizvod.Info.IDProizvodjac));
                        stavkaDTO.Proizvod.ProizvodInfo.Kategorija = mapper.Map<KategorijaDTO>(kategorijaRepository.GetKategorijaByID(stavka.Proizvod.Info.IDKategorija));
                        stavkaDTO.Proizvod.ProizvodInfo.Kolekcija = mapper.Map<KolekcijaDTO>(kolekcijaRepository.GetKolekcijaByID(stavka.Proizvod.Info.IDKolekcija));

                        stavkeRacunaDTO.Add(stavkaDTO);
                    }

                    return Ok(stavkeRacunaDTO);
                }
                else
                    return StatusCode(StatusCodes.Status403Forbidden, "Access forbiden");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{stavkaID}/{racunID}")]
        public ActionResult<StavkaRacunaDTO> GetStavkaByID(int stavkaID, int racunID)
        {
            try
            {
                StavkaRacunaEntity? stavka = stavkaRacunaRepository.GetStavkaRacunaByID(stavkaID, racunID);

                if(stavka == null)
                    return NoContent();

                StavkaRacunaDTO stavkaDTO = mapper.Map<StavkaRacunaDTO>(stavka);

                stavkaDTO.Racun = mapper.Map<RacunDTO>(racunRepository.GetRacunByID(stavka.IDRacun));
                stavkaDTO.Proizvod = mapper.Map<ProizvodDTO>(proizvodRepository.GetProizvodByID(stavka.IDProizvod));

                stavkaDTO.Racun.Kupac = mapper.Map<KorisnikDTO>(korisnikRepository.GetKorisnikByID(stavka.Racun.IDKupac));
                stavkaDTO.Proizvod.Velicina = mapper.Map<VelicinaDTO>(velicinaRepository.GetVelicinaByID(stavka.Proizvod.IDVelicina));
                stavkaDTO.Proizvod.ProizvodInfo = mapper.Map<InfoDTO>(infoRepository.GetInfoByID(stavka.Proizvod.IDProizvodInfo));

                stavkaDTO.Proizvod.ProizvodInfo.Proizvodjac = mapper.Map<ProizvodjacDTO>(proizvodjacRepository.GetProizvodjacByID(stavka.Proizvod.Info.IDProizvodjac));
                stavkaDTO.Proizvod.ProizvodInfo.Kategorija = mapper.Map<KategorijaDTO>(kategorijaRepository.GetKategorijaByID(stavka.Proizvod.Info.IDKategorija));
                stavkaDTO.Proizvod.ProizvodInfo.Kolekcija = mapper.Map<KolekcijaDTO>(kolekcijaRepository.GetKolekcijaByID(stavka.Proizvod.Info.IDKolekcija));

                return (stavkaDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Consumes("application/json")]
        public ActionResult<StavkaRacunaDTO> CreateStavkaRacuna([FromBody]StavkaRacunaCreateDTO stavkaRacunaCreateDTO)
        {
            try
            {
                ProizvodEntity? proizvod = proizvodRepository.GetProizvodByID(stavkaRacunaCreateDTO.IDProizvod);
                RacunEntity? racun = racunRepository.GetRacunByID(stavkaRacunaCreateDTO.IDRacun);

                if (racun == null)
                    return StatusCode(StatusCodes.Status422UnprocessableEntity, "Neki od starnih kljuceva nedostaje!--proizvod");
                else if (proizvod == null)
                    return StatusCode(StatusCodes.Status422UnprocessableEntity, "Neki od starnih kljuceva nedostaje!--racun");
                else
                {
                    StavkaRacunaDTO stavkaDTO = stavkaRacunaRepository.CreateStavkaRacuna(stavkaRacunaCreateDTO);
                    stavkaRacunaRepository.SaveChanges();

                    stavkaDTO.Racun = mapper.Map<RacunDTO>(racunRepository.GetRacunByID(stavkaRacunaCreateDTO.IDRacun));
                    stavkaDTO.Proizvod = mapper.Map<ProizvodDTO>(proizvodRepository.GetProizvodByID(stavkaRacunaCreateDTO.IDProizvod));

                    return Ok("Uspesno kreirana stavka racuna");
                }

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{stavkaID}/{racunID}")]
        public IActionResult DeleteStavka(int stavkaID, int racunID)
        {
            try
            {
                StavkaRacunaEntity? stavkaRacuna = stavkaRacunaRepository.GetStavkaRacunaByID(stavkaID, racunID);

                if(stavkaRacuna == null)
                    return NotFound();

                stavkaRacunaRepository.DeleteStavkaRacuna(stavkaID, racunID);
                stavkaRacunaRepository.SaveChanges();

                return NoContent();
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        [Consumes("application/json")]
        public ActionResult<StavkaRacunaDTO> UpdateStavkaRacuna([FromBody] StavkaRacunaUpdateDTO stavkaRacunaUpdateDTO)
        {
            try
            {
                if (HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Role)?.Value == "ADMIN")
                {
                    StavkaRacunaEntity? oldStavkaRacuna = stavkaRacunaRepository.GetStavkaRacunaByID(stavkaRacunaUpdateDTO.IDStavkaRacuna, stavkaRacunaUpdateDTO.IDRacun);
                    if (oldStavkaRacuna == null)
                        return NotFound();
                    ProizvodEntity? proizvod = proizvodRepository.GetProizvodByID(stavkaRacunaUpdateDTO.IDProizvod);
                    RacunEntity? racun = racunRepository.GetRacunByID(stavkaRacunaUpdateDTO.IDRacun);

                    if (proizvod != null && racun != null)
                    {
                        StavkaRacunaEntity stavkaRacuna = mapper.Map<StavkaRacunaEntity>(stavkaRacunaUpdateDTO);
                        mapper.Map(stavkaRacuna, oldStavkaRacuna);

                        racunRepository.SaveChanges();

                        stavkaRacuna.Racun = racun;
                        stavkaRacuna.Proizvod = proizvod;

                        return Ok(mapper.Map<StavkaRacunaDTO>(stavkaRacuna));
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
