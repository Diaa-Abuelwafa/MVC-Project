using System.ComponentModel.DataAnnotations;

namespace PresentationLayer.Models.ViewModels
{
    public class UserUpdateViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }

        [Required(ErrorMessage = "You Must Enter The UserName")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Address { get; set; }
    }
}
