using AutoMapper;
using ERP_backend.Entity;
using ERP_backend.Model;
using ERP_backend.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ERP_backend.Controllers
{
    public class StavkaKorpeController : ControllerBase
    {
        private readonly IStavkaKorpeRepository stavkaKorpeRepository;
        private readonly IKorisnikRepository korisnikRepository;
        private readonly IProizvodRepository proizvodRepository;
        private LinkGenerator linkGenerator;
        private IMapper mapper;

        public StavkaKorpeController(IProizvodRepository proizvodRepository, IStavkaKorpeRepository stavkaKorpeRepository, IKorisnikRepository korisnikRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.stavkaKorpeRepository = stavkaKorpeRepository;
            this.proizvodRepository = proizvodRepository;
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


        [AllowAnonymous]
        [HttpPost]
        public ActionResult<StavkaKorpeDTO> PostStavkaKorpe(StavkaKorpeCreateDTO stavkaKorpeCreateDTO)
        {
            try
            {
                stavkaKorpeCreateDTO.Kupac = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.NameIdentifier)?.Value);

                ProizvodEntity? proizvod = proizvodRepository.GetProizvodByID(stavkaKorpeCreateDTO.Proizvod);

                KorisnikEntity? korisnik = korisnikRepository.GetKorisnikByID(stavkaKorpeCreateDTO.Kupac);

                if (korisnik == null)
                    return StatusCode(StatusCodes.Status403Forbidden, "Morate se prijaviti.");

                if (proizvod == null)
                    return StatusCode(StatusCodes.Status422UnprocessableEntity, "Neki od starnih kljuceva nedostaje!");
                else
                {
                    StavkaKorpeDTO stavkaKorpeDTO = stavkaKorpeRepository.CreateStavkaKorpe(stavkaKorpeCreateDTO);
                    stavkaKorpeRepository.SaveChanges();

                    stavkaKorpeDTO.Kupac = mapper.Map<KorisnikDTO>(korisnik);
                    stavkaKorpeDTO.Proizvod = mapper.Map<ProizvodDTO>(proizvod);

                    return Ok("Uspesno kreirana stavka korpe!");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
