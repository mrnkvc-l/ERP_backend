using AutoMapper;
using ERP_backend.Entity;
using ERP_backend.Model;
using ERP_backend.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ERP_backend.Controllers
{
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
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

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
    }
}
