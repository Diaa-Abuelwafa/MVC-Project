using System.ComponentModel.DataAnnotations;

namespace PresentationLayer.Models.ViewModels
{
    public class RoleCreateViewModel
    {
        [Required(ErrorMessage = "You Must Enter The Name Of Role")]
        public string Name { get; set; }
    }
}
