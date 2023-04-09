using AutoMapper;
using ERP_backend.Entity;
using ERP_backend.Model;
using ERP_backend.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ERP_backend.Controllers
{
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
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

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

    }
}
