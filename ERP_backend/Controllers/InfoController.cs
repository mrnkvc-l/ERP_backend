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
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public InfoController(IInfoRepository infoRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.infoRepository = infoRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
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

                List<InfoDTO> infoDTO = mapper.Map<List<InfoDTO>>(infos);

                return infoDTO;
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
                        InfoDTO infoDTO = infoRepository.CreateInfo(infoCreateDTO);
                        infoRepository.SaveChanges();

                        return Ok("Uspesno kreirana informacija!");
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

                    InfoEntity info = mapper.Map<InfoEntity>(infoUpdateDTO);
                    mapper.Map(info, oldInfo);
                    infoRepository.SaveChanges();

                    return Ok(mapper.Map<InfoDTO>(info));
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
