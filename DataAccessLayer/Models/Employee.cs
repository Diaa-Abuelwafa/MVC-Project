using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        [Required]
        [Length(2, 10,ErrorMessage = "Name Must Be Between 2 And 10 Characters")]
        public string Name { get; set; }

        [Required]
        [Range(18, 100, ErrorMessage = "Age Must Be Between 18 And 100 Years Old")]
        public int Age { get; set; }

        [Required]
        [RegularExpression("^\\d{2}-[A-Za-z0-9]+-[A-Za-z]+$", ErrorMessage = "Address Format Must Match This Pattern [BuildingNumber-StreetName-CityName]")]
        public string Address { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        public double Salary { get; set; }

        [Required]
        public long PhoneNumber { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime HireDate { get; set; }
        public int DeptId { get; set; }
        public Department? Department { get; set; }
        public string? ImagePath { get; set; }
    }
}
