using AutoMapper;
using ERP_backend.Entity;
using ERP_backend.Model;
using ERP_backend.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ERP_backend.Controllers
{
    public class StavkaKorpeController : ControllerBase
    {
        private readonly IStavkaKorpeRepository stavkaKorpeRepository;
        private readonly IKorisnikRepository korisnikRepository;
        private LinkGenerator linkGenerator;
        private IMapper mapper;

        public StavkaKorpeController(IStavkaKorpeRepository stavkaKorpeRepository, IKorisnikRepository korisnikRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.stavkaKorpeRepository = stavkaKorpeRepository;
            this.korisnikRepository = korisnikRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult<List<StavkaKorpeDTO>> GetAllStavkaKorpe(int userID)
        {
            try
            {
                if (HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.NameIdentifier)?.Value == userID.ToString())
                {
                    List<StavkaKorpeEntity> stavkeKorpe = stavkaKorpeRepository.GetAllStavkeKorpe(userID);

                    if (stavkeKorpe == null || stavkeKorpe.Count == 0)
                        return NotFound();

                    List<StavkaKorpeDTO> stavkeKorpeDTO = mapper.Map<List<StavkaKorpeDTO>>(stavkeKorpe);

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

        [HttpPost]
        public ActionResult<StavkaKorpeDTO> PostStavkaKorpe(StavkaKorpeCreateDTO stavkaKorpeCreateDTO)
        {
            try
            {
                if (HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.NameIdentifier)?.Value == stavkaKorpeCreateDTO.Kupac.ToString())
                {
                    return Ok();
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
