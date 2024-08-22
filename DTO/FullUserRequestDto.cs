using System.Text.Json.Serialization;

namespace Gis.Net.Auth.DTO;

/// <summary>
/// The FullUserRequestDto class represents a data transfer object for a full user request.
/// It inherits from the ReducedUserRequestDto class and implements the IFullUser interface.
/// </summary>
public class FullUserRequestDto : ReducedUserRequestDto, IFullUser
{
    /// <summary>
    /// Gets or sets the company name of the user.
    /// </summary>
    /// <remarks>
    /// This property is used to store the company name of the user.
    /// </remarks>
    [JsonPropertyName("companyName")] public string? CompanyName { get; set; }
    /// <summary>
    /// Represents a phone number.
    /// </summary>
    [JsonPropertyName("phone")] public string? Phone { get; set; }
    /// <summary>
    /// Represents a mobile phone number.
    /// </summary>
    [JsonPropertyName("mobile")] public string? Mobile { get; set; }
    /// <summary>
    /// Represents the VAT number associated with a user.
    /// </summary>
    [JsonPropertyName("vatNumber")] public string? VatNumber { get; set; }
    /// <summary>
    /// Represents the fiscal code of a user.
    /// </summary>
    [JsonPropertyName("fiscalCode")] public string? FiscalCode { get; set; }
    /// <summary>
    /// Represents the address information of a user.
    /// </summary>
    [JsonPropertyName("address")] public string? Address { get; set; }
    /// <summary>
    /// Represents a city.
    /// </summary>
    [JsonPropertyName("city")] public string? City { get; set; }
    /// <summary>
    /// Represents the region of a user's address.
    /// </summary>
    [JsonPropertyName("region")] public string? Region { get; set; }
    /// <summary>
    /// Represents a postal code.
    /// </summary>
    [JsonPropertyName("zipCode")] public string? ZipCode { get; set; }
    /// <summary>
    /// Represents the country property of a FullUserRequestDto object.
    /// </summary>
    [JsonPropertyName("country")] public string? Country { get; set; }
    /// <summary>
    /// Gets or sets the enabled status of the user.
    /// </summary>
    /// <remarks>
    /// This property is used to determine whether the user is enabled or disabled.
    /// If the value is true, it means the user is enabled and can access the system.
    /// If the value is false, it means the user is disabled and cannot access the system.
    /// </remarks>
    [JsonPropertyName("enabled")] public bool? Enabled { get; set; }
    /// <summary>
    /// Represents the gender of a person.
    /// </summary>
    [JsonPropertyName("gender")] public Gender? Gender { get; set; }
    /// <summary>
    /// Gets or sets the birth year of the user.
    /// </summary>
    /// <remarks>
    /// This property is used to specify the year in which the user was born.
    /// </remarks>
    /// <value>The birth year of the user.</value>
    [JsonPropertyName("birthday")] public int? BirthYear { get; set; }
    /// <summary>
    /// Represents the avatar of a user.
    /// </summary>
    [JsonPropertyName("avatar")] public string? Avatar { get; set; }
    /// <summary>
    /// Represents the photo property of a FullUserRequestDto object.
    /// </summary>
    /// <value>
    /// The photo of the user.
    /// </value>
    [JsonPropertyName("photo")] public string? Photo { get; set; }
}