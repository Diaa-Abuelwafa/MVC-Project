using System.ComponentModel.DataAnnotations;

namespace PresentationLayer.Models.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "You Must Enter New Password")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword")]
        public string ConfirmedPassword { get; set; }
    }
}
