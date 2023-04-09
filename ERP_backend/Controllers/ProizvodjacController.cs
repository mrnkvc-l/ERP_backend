using AutoMapper;
using ERP_backend.Entity;
using ERP_backend.Model;
using ERP_backend.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ERP_backend.Controllers
{
    [ApiController]
    [Route("api/proizvodjaci")]
    [Produces("application/json", "application/xml")]
    public class ProizvodjacController : ControllerBase
    {
        private readonly IProizvodjacRepository proizvodjacRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public ProizvodjacController(IProizvodjacRepository proizvodjacRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.proizvodjacRepository = proizvodjacRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        [HttpHead]
        [HttpGet]
        public ActionResult<List<ProizvodjacDTO>> GetAllProizvodjaci()
        {
            try
            {
                List<ProizvodjacEntity> proizvodjaci = proizvodjacRepository.GetAllProizvodjaci();
                if (proizvodjaci == null || proizvodjaci.Count == 0)
                    return NoContent();

                List<ProizvodjacDTO> proizvodjaciDTO = mapper.Map<List<ProizvodjacDTO>>(proizvodjaci);
                return Ok(proizvodjaciDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{proizvodjacID}")]
        public ActionResult<ProizvodjacDTO> GetProizvodjacByID (int proizvodjacID)
        {
            try
            {
                ProizvodjacEntity? proizvodjac = proizvodjacRepository.GetProizvodjacByID(proizvodjacID);
                if (proizvodjac == null)
                    return NoContent();

                ProizvodjacDTO proizvodjacDTO = mapper.Map<ProizvodjacDTO>(proizvodjac);
                return Ok(proizvodjacDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Consumes("application/json")]
        public ActionResult<ProizvodjacDTO> CreateProizvodjac(ProizvodjacCreateDTO proizvodjacCreateDTO)
        {
            try
            {
                ProizvodjacDTO proizvodjacDTO = proizvodjacRepository.CreateProizvodjac(proizvodjacCreateDTO);
                proizvodjacRepository.SaveChanges();

                return Ok("Uspesno kreiran proizvodjac!");
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
