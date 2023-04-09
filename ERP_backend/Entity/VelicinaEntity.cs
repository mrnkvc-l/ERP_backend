using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP_backend.Entity
{
    [Table("velicina")]
    public class VelicinaEntity
    {
        [Key]
        public int IDVelicina { get; set; }

        public string Oznaka { get; set; } = null!;
    }
}
