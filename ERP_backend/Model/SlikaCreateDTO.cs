using System.ComponentModel.DataAnnotations;

namespace ERP_backend.Model
{
    public class SlikaCreateDTO
    {
        public IFormFile File { get; set; } = null!;
        public int IDInfo { get; set; }
    }
}
