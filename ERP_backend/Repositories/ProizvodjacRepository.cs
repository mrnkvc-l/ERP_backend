using AutoMapper;
using ERP_backend.Entity;
using ERP_backend.Model;

namespace ERP_backend.Repositories
{
    public class ProizvodjacRepository : IProizvodjacRepository
    {
        private readonly ErpContext context;
        private readonly IMapper mapper;

        public ProizvodjacRepository(ErpContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public ProizvodjacDTO CreateProizvodjac(ProizvodjacCreateDTO proizvodjacCreateDTO)
        {
            ProizvodjacEntity proizvodjac = mapper.Map<ProizvodjacEntity>(proizvodjacCreateDTO);
            context.Add(proizvodjac);
            return mapper.Map<ProizvodjacDTO>(proizvodjac);
        }

        public void DeleteProizvodjac(int proizvodjacID)
        {
            throw new NotImplementedException();
        }

        public List<ProizvodjacEntity> GetAllProizvodjaci()
        {
            return context.Proizvodjaci.ToList();
        }

        public ProizvodjacEntity? GetProizvodjacByID(int proizvodjacID)
        {
            return context.Proizvodjaci.FirstOrDefault(e => e.IDProizvodjac == proizvodjacID);
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
    }
}
