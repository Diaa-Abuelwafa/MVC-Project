using DataAccessLayer.Data.Contexts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    // Custom Validator
    internal class UniqueAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return null;
            }

            string S = value.ToString();
            Department D;
            using (AppDbContext Context = new AppDbContext())
            {
                 D = Context.Departments.FirstOrDefault(x => x.Name == S);
            }

            if(D == null)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("This Value Already Exsists");
        }
    }
}
