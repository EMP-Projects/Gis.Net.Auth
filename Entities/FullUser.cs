using System.ComponentModel.DataAnnotations.Schema;
using Gis.Net.Auth.DTO;

namespace Gis.Net.Auth.Entities;

[Table("users")]
public class FullUser : ReducedUser, IFullUser
{
    [Column("company_name")] public string? CompanyName { get; set; }
    
    [Column("phone")] public string? Phone { get; set; }
    
    [Column("mobile")] public string? Mobile { get; set; }
    
    [Column("vat_number")] public string? VatNumber { get; set; }
    
    [Column("fiscal_code")] public string? FiscalCode { get; set; }
    
    [Column("address")] public string? Address { get; set; }
    
    [Column("city")] public string? City { get; set; }
    
    [Column("region")] public string? Region { get; set; }
    
    [Column("zip_code")] public string? ZipCode { get; set; }
    
    [Column("country")] public string? Country { get; set; }
    
    [Column("enabled")] public bool? Enabled { get; set; } = true;
    
    [Column("gender")] public Gender? Gender { get; set; } = DTO.Gender.None;
    
    [Column("birth_year")] public int? BirthYear { get; set; }
    
    [Column("avatar")] public string? Avatar { get; set; }
    
    [Column("photo")] public string? Photo { get; set; }
}