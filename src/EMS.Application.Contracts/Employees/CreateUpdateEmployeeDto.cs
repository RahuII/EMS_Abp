using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EMS.Employees;

public class CreateUpdateEmployeeDto
{
    [RegularExpression(@"^[a-zA-Z\s]{1,30}$", ErrorMessage = "Full name should contain only alphabetical characters and spaces, with a maximum length of 30 characters.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Please provide a valid Date of Birth.")]
    public DateTime DateOfBirth { get; set; }

    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Please provide a valid email address.")]
    public string Email { get; set; }

    [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Please provide a valid phone number.")]
    public string Phone { get; set; }
}