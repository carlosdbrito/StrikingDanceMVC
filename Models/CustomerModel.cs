using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StrikingDanceMVC.Models
{
    public class CustomerModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório!")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "Email é obrigatório!")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Nascimento é obrigatório!")]
        public DateTime? Nascimento { get; set; }
        public DateTime? DataInclusao { get; set; }

    }
}
