using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP_backend.Entity
{
    [Table("skladiste")]
    [PrimaryKey(nameof(IDSkladiste))]
    public class SkladisteEntity
    {
        public int IDSkladiste { get; set; }

        public string Naziv { get; set; } = null!;

        public string Adresa { get; set; } = null!;
    }
}
