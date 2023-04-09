using AutoMapper;
using ERP_backend.Entity;
using ERP_backend.Model;
using ERP_backend.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ERP_backend.Controllers
{
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
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
