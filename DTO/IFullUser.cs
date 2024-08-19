namespace Gis.Net.Auth.DTO;

public interface IFullUser
{   
    string? CompanyName { get; set; }

    string? Phone { get; set; }

    string? Mobile { get; set; }

    string? VatNumber { get; set; }

    string? FiscalCode { get; set; }

    string? Address { get; set; }

    string? City { get; set; }

    string? Region { get; set; }

    string? ZipCode { get; set; }

    string? Country { get; set; }

    bool? Enabled { get; set; }

    int? BirthYear { get; set; }

    Gender? Gender { get; set; }

    string? Avatar { get; set; }

    string? Photo { get; set; }
}