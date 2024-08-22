using System.ComponentModel.DataAnnotations.Schema;
using Gis.Net.Auth.DTO;

namespace Gis.Net.Auth.Entities;

/// <summary>
/// The FullUser class represents a full user profile, including additional information such as company name, phone number, address, etc.
/// </summary>
[Table("users")]
public class FullUser : ReducedUser, IFullUser
{
    /// <summary>
    /// Gets or sets the company name of the user.
    /// </summary>
    /// <remarks>
    /// The company name can be null or empty if not provided.
    /// </remarks>
    /// <value>The company name of the user.</value>
    [Column("company_name")] public string? CompanyName { get; set; }

    /// <summary>
    /// Represents a phone number associated with a user's profile.
    /// </summary>
    [Column("phone")] public string? Phone { get; set; }

    /// <summary>
    /// Represents a mobile property of a user.
    /// </summary>
    [Column("mobile")] public string? Mobile { get; set; }

    /// <summary>
    /// Gets or sets the VAT number of the user.
    /// </summary>
    [Column("vat_number")] public string? VatNumber { get; set; }

    /// <summary>
    /// Represents the fiscal code of a user.
    /// </summary>
    [Column("fiscal_code")] public string? FiscalCode { get; set; }

    /// <summary>
    /// Represents the address information of a user.
    /// </summary>
    [Column("address")] public string? Address { get; set; }

    /// <summary>
    /// Represents a city in a user's profile.
    /// </summary>
    [Column("city")] public string? City { get; set; }

    /// <summary>
    /// Gets or sets the region of the user's address.
    /// </summary>
    [Column("region")] public string? Region { get; set; }

    /// <summary>
    /// Represents the ZipCode property of a user.
    /// </summary>
    [Column("zip_code")] public string? ZipCode { get; set; }

    /// <summary>
    /// The Country property represents the user's country.
    /// </summary>
    /// <remarks>
    /// This property is available in the FullUser class, which represents a full user profile.
    /// </remarks>
    /// <seealso cref="FullUser"/>
    [Column("country")] public string? Country { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the user account is enabled or disabled.
    /// </summary>
    [Column("enabled")] public bool? Enabled { get; set; } = true;

    /// <summary>
    /// The Gender enum represents the gender of a person.
    /// </summary>
    [Column("gender")] public Gender? Gender { get; set; } = DTO.Gender.None;

    /// BirthYear represents the year of birth of a user.
    [Column("birth_year")] public int? BirthYear { get; set; }

    /// <summary>
    /// Represents an avatar for a user.
    /// </summary>
    [Column("avatar")] public string? Avatar { get; set; }

    /// <summary>
    /// Represents a photo property of a user.
    /// </summary>
    [Column("photo")] public string? Photo { get; set; }
}