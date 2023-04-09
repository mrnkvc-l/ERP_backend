using AutoMapper;
using ERP_backend.Entity;
using ERP_backend.Model;

namespace ERP_backend.Repositories
{
    public class VelicinaRepository : IVelicinaRepository
    {
        private readonly ErpContext context;
        private readonly IMapper mapper;

        public VelicinaRepository(ErpContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public VelicinaDTO CreateVelicina(VelicinaCreateDTO velicinaCreateDTO)
        {
            VelicinaEntity velicina = mapper.Map<VelicinaEntity>(velicinaCreateDTO);
            context.Add(velicina);
            return mapper.Map<VelicinaDTO>(velicina);
        }

        public void DeleteVelicina(int velicinaID)
        {
            VelicinaEntity? velicina = GetVelicinaByID(velicinaID);
            if (velicina != null)
                context.Remove(velicina);
        }

        public List<VelicinaEntity> GetAllVelicine()
        {
            return context.Velicine.ToList();
        }

        public VelicinaEntity? GetVelicinaByID(int velicinaID)
        {
            return context.Velicine.FirstOrDefault(e => e.IDVelicina == velicinaID);
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
    }
}
