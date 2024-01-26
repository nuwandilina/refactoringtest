using System.ComponentModel.DataAnnotations;

namespace LegacyApp
{
    public class Client
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ClientStatus ClientStatus { get; set; }
    }
}
