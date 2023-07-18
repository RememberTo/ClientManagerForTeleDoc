using ClientManager.Domain;
using System.ComponentModel.DataAnnotations;

namespace ClientManager.Web.Models
{
    public record ShareholderViewModel
    {
        public int Id { get; set; }
        public int ClientId { get; set; }

        [Required(ErrorMessage = "Поле ИНН является обязательным.")]
        [StringLength(12, MinimumLength = 12, ErrorMessage = "ИНН должен содержать 12 символов.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "ИНН должен содержать только цифры.")]
        public string INN { get; set; }

        [Required(ErrorMessage = "Поле ФИО является обязательным.")]
        public string FullName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Client? Client { get; set; }
    }
}
