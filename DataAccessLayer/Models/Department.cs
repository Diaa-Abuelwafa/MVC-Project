using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }

        [Required]
        [RegularExpression("^\\d{2}$", ErrorMessage = "Code Must Be Contains 2 Digits Only")]
        public string Code { get; set; }

        [Required]
        [MaxLength(10, ErrorMessage = "The Name Must Be Less Than 10 Characters")]
        [Unique]
        public string Name { get; set; }

        [Required]
        public DateTime DateOfCreation { get; set; }
    }
}
