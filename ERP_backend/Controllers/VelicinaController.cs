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

        [AllowAnonymous]
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

        [AllowAnonymous]
        [HttpGet("{velicinaID}")]
        public ActionResult<VelicinaDTO> GetVelicinaByID(int velicinaID)
        {
            try
            {
                VelicinaEntity? velicina = velicinaRepository.GetVelicinaByID(velicinaID);
                if (velicina == null)
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
                if (HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Role)?.Value == "ADMIN")
                {
                    List<VelicinaEntity> velicine = velicinaRepository.GetAllVelicine();
                    if (velicine.Find(e => e.Oznaka == velicinaCreateDTO.Oznaka) == null)
                    {
                        VelicinaDTO velicinaDTO = velicinaRepository.CreateVelicina(velicinaCreateDTO);
                        velicinaRepository.SaveChanges();

                        return Ok("Uspesno kreirana velicina!");
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status422UnprocessableEntity, "Već postoji zadata oznaka velicine.");
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

        [HttpDelete("{velicinaID}")]
        public IActionResult DeleteVelicina(int velicinaID)
        {
            try
            {
                if (HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Role)?.Value == "ADMIN")
                {
                    VelicinaEntity? velicina = velicinaRepository.GetVelicinaByID(velicinaID);
                    if (velicina == null)
                        return NotFound();

                    velicinaRepository.DeleteVelicina(velicinaID);
                    velicinaRepository.SaveChanges();

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
        public ActionResult<VelicinaDTO> UpdateVelicina([FromBody] VelicinaUpdateDTO velicinaUpdateDTO)
        {
            try
            {
                if (HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Role)?.Value == "ADMIN")
                {
                    VelicinaEntity? oldVelicina = velicinaRepository.GetVelicinaByID(velicinaUpdateDTO.IDVelicina);

                    if (oldVelicina == null)
                        return NotFound();

                    VelicinaEntity velicina = mapper.Map<VelicinaEntity>(velicinaUpdateDTO);
                    mapper.Map(velicina, oldVelicina);
                    velicinaRepository.SaveChanges();
                    return Ok(mapper.Map<VelicinaDTO>(velicina));
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
