using System.ComponentModel.DataAnnotations;

namespace PresentationLayer.Models.ViewModels
{
    public class RegisterViewModel
    {
        public string UserName { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmedPassword { get; set; }
        public string Address { get; set; }
        public bool IsAgree { get; set; }
    }
}
