using AutoMapper;
using ERP_backend.Entity;
using ERP_backend.Model;
using ERP_backend.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ERP_backend.Controllers
{
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

        public ProizvodController(IProizvodRepository proizvodRepository, IProizvodjacRepository proizvodjacRepository,IInfoRepository infoRepository, IKategorijaRepository kategorijaRepository, IKolekcijaRepository kolekcijaRepository, IVelicinaRepository velicinaRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.proizvodRepository = proizvodRepository;
            this.proizvodjacRepository = proizvodjacRepository;
            this.infoRepository = infoRepository;
            this.kategorijaRepository= kategorijaRepository;
            this.kolekcijaRepository= kolekcijaRepository;
            this.velicinaRepository= velicinaRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

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

                foreach(ProizvodEntity proizvod in proizvodi)
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
    }
}
