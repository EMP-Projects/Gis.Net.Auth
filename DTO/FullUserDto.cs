using System.Text.Json.Serialization;

namespace Gis.Net.Auth.DTO;

/// <summary>
/// Represents a full user DTO (Data Transfer Object) that contains all the information about a user.
/// </summary>
public class FullUserDto : ReducedUserDto, IFullUser
{
    /// <summary>
    /// Represents the name of a company associated with a user.
    /// </summary>
    [JsonPropertyName("companyName")] public string? CompanyName { get; set; }

    /// <summary>
    /// Represents the phone number of a user.
    /// </summary>
    [JsonPropertyName("phone")] public string? Phone { get; set; }

    /// <summary>
    /// Represents the mobile number associated with a user.
    /// </summary>
    [JsonPropertyName("mobile")] public string? Mobile { get; set; }

    /// <summary>
    /// Represents the VAT number associated with a user or company.
    /// </summary>
    [JsonPropertyName("vatNumber")] public string? VatNumber { get; set; }

    /// <summary>
    /// Represents the fiscal code of a user.
    /// </summary>
    [JsonPropertyName("fiscalCode")] public string? FiscalCode { get; set; }

    /// <summary>
    /// Represents the address of a user.
    /// </summary>
    [JsonPropertyName("address")] public string? Address { get; set; }

    /// <summary>
    /// Represents the city associated with a user's address.
    /// </summary>
    [JsonPropertyName("city")] public string? City { get; set; }

    /// <summary>
    /// Represents the region associated with a user's address.
    /// </summary>
    [JsonPropertyName("region")] public string? Region { get; set; }

    /// <summary>
    /// Represents the zip code associated with a user's address.
    /// </summary>
    [JsonPropertyName("zipCode")] public string? ZipCode { get; set; }

    /// <summary>
    /// Represents the country associated with a user.
    /// </summary>
    [JsonPropertyName("country")] public string? Country { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the user is enabled.
    /// </summary>
    [JsonPropertyName("enabled")] public bool? Enabled { get; set; }

    /// <summary>
    /// Represents the gender of a user.
    /// </summary>
    [JsonPropertyName("gender")] public Gender? Gender { get; set; }

    /// <summary>
    /// Represents the birth year of a user.
    /// </summary>
    [JsonPropertyName("birthday")] public int? BirthYear { get; set; }

    /// <summary>
    /// Represents the avatar of a user.
    /// </summary>
    /// <remarks>
    /// The avatar is an image or representation that is associated with a user's profile and is used to visually identify the user.
    /// </remarks>
    [JsonPropertyName("avatar")] public string? Avatar { get; set; }

    /// <summary>
    /// Represents a property that stores the photo of a user.
    /// </summary>
    [JsonPropertyName("photo")] public string? Photo { get; set; }
}