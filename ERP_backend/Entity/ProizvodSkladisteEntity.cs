using ERP_backend.Model;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP_backend.Entity
{
    [Table("proizvodSkladiste")]
    [PrimaryKey(nameof(Proizvod), nameof(Skladiste))]
    public class ProizvodSkladisteEntity
    {
        public int Proizvod { get; set; }

        public int Skladiste { get; set; }

        public int Kolicina { get; set; }
    }
}
