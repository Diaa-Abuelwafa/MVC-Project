using System.ComponentModel.DataAnnotations;

namespace PresentationLayer.Models.ViewModels
{
    public class ForgetPasswordViewModel
    {
        [Required(ErrorMessage = "You Must Enter Valid Email Address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
