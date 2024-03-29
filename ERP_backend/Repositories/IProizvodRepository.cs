﻿using ERP_backend.Entity;
using ERP_backend.Model;

namespace ERP_backend.Repositories
{
    public interface IProizvodRepository
    {
        List<ProizvodEntity> GetAllProizvodi();

        List<ProizvodEntity> GetProizvodByInfo(int infoID);

        ProizvodEntity? GetProizvodByID(int proizvodID);

        ProizvodDTO CreateProizvod(ProizvodCreateDTO proizvodCreateDTO);

        void DeleteProizvod(int proizvodID);

        bool SaveChanges();
    }
}
