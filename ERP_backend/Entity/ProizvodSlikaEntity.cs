using ERP_backend.Model;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP_backend.Entity
{
    [Table("proizvodSlika")]
    [PrimaryKey(nameof(Proizvod), nameof(Slika))]
    public class ProizvodSlikaEntity
    {
        public int Proizvod { get; set; }

        public int Slika { get; set; }
    }
}
