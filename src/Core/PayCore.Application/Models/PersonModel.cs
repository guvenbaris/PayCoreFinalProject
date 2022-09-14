﻿
namespace PayCore.Application.Models;

public class PersonModel : BaseModel
{
    public string? FirstName { get; set; }
    public string LastName { get; set; }
    public string IdentityNumber { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string LicencePlateNumber { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    public DateTime? LastActivity { get; set; }
}
