using System.ComponentModel.DataAnnotations;

namespace PresentationLayer.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "You Must Enter Your UserName")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "You Musr Enter Your Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
