using ClientManager.Domain;
using ClientManager.Domain.Enum;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ClientManager.Web.Models
{
    public class ClientViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Поле ИНН является обязательным.")]
        [StringLength(12, MinimumLength = 12, ErrorMessage = "ИНН должен содержать 12 символов.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "ИНН должен содержать только цифры.")]
        public string INN { get; set; }

        [Required(ErrorMessage = "Поле Наименование является обязательным.")]
        [MaxLength(100)]
        public string Name { get; set; }
        public ClientType Type { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public List<Shareholder>? Shareholders { get; set; }
    }
}
