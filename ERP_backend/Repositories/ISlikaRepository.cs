﻿using ERP_backend.Entity;
using ERP_backend.Model;

namespace ERP_backend.Repositories
{
    public interface ISlikaRepository
    {
        List<SlikaEntity> GetAllSlike();

        List<SlikaEntity> GetSlikeByProizvod(int proizvodId);

        SlikaEntity? GetSlikaByID(int slikaID);

        SlikaDTO CreateSlika(SlikaCreateDTO slikaCreateDTO);

        void DeleteSlika(int slikaID);

        bool SaveChanges();
    }
}
