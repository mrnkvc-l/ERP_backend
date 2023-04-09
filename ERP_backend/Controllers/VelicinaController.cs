using AutoMapper;
using ERP_backend.Entity;
using ERP_backend.Model;
using ERP_backend.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ERP_backend.Controllers
{
    [ApiController]
    [Route("api/velicine")]
    [Produces("application/json", "application/xml")]
    public class VelicinaController : ControllerBase
    {
        private IVelicinaRepository velicinaRepository;
        private LinkGenerator linkGenerator;
        private IMapper mapper;

        public VelicinaController(IVelicinaRepository velicinaRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.velicinaRepository = velicinaRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        [HttpHead]
        [HttpGet]
        public ActionResult<List<VelicinaDTO>> GetAllVelicine()
        {
            try
            {
                List<VelicinaEntity> velicine = velicinaRepository.GetAllVelicine();

                if (velicine == null || velicine.Count == 0)
                    return NoContent();

                List<VelicinaDTO> velicineDTO = mapper.Map<List<VelicinaDTO>>(velicine);

                return Ok(velicineDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{velicinaID}")]
        public ActionResult<VelicinaDTO> GetVelicinaByID(int velicinaID)
        {
            try
            {
                VelicinaEntity? velicina = velicinaRepository.GetVelicinaByID(velicinaID);
                if(velicina == null)
                    return NoContent();

                VelicinaDTO velicinaDTO = mapper.Map<VelicinaDTO>(velicina);
                return Ok(velicinaDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Consumes("application/json")]
        public ActionResult<VelicinaDTO> CreateVelicina([FromBody] VelicinaCreateDTO velicinaCreateDTO)
        {
            try
            {
                VelicinaDTO velicinaDTO = velicinaRepository.CreateVelicina(velicinaCreateDTO);
                velicinaRepository.SaveChanges();

                return Ok("Uspesno kreirana velicina!");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{velicinaID}")]
        public IActionResult DeleteVelicina (int velicinaID)
        {
            try
            {
                VelicinaEntity? velicina = velicinaRepository.GetVelicinaByID(velicinaID);
                if (velicina == null)
                    return NoContent();

                velicinaRepository.DeleteVelicina(velicinaID);
                velicinaRepository.SaveChanges();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
