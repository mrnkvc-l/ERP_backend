using Microsoft.AspNetCore.DataProtection.KeyManagement;

namespace ERP_backend.Model
{
    public class SkladisteDTO
    {
        private int IDSkladiste { get; set; }

        public string Naziv { get; set; } = null!;

        public string Adresa { get; set; } = null!;
    }
}
