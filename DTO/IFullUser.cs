namespace Gis.Net.Auth.DTO;

/// <summary>
/// The IFullUser interface represents a full user profile.
/// </summary>
public interface IFullUser
{
    /// <summary>
    /// Gets or sets the name of the company associated with the user.
    /// </summary>
    /// <value>The company name.</value>
    string? CompanyName { get; set; }

    /// <summary>
    /// Gets or sets the phone number.
    /// </summary>
    /// <value>The phone number.</value>
    string? Phone { get; set; }

    /// <summary>
    /// Represents the mobile phone number of a user.
    /// </summary>
    string? Mobile { get; set; }

    /// <summary>
    /// Represents the VAT number of a user.
    /// </summary>
    string? VatNumber { get; set; }

    /// <summary>
    /// Represents a fiscal code associated with a user.
    /// </summary>
    string? FiscalCode { get; set; }

    /// <summary>
    /// Represents an address associated with a user.
    /// </summary>
    string? Address { get; set; }

    /// <summary>
    /// Represents a city.
    /// </summary>
    string? City { get; set; }

    string? Region { get; set; }

    /// <summary>
    /// Represents the postal code of a user's address.
    /// </summary>
    string? ZipCode { get; set; }

    /// <summary>
    /// Represents the country information of a user.
    /// </summary>
    string? Country { get; set; }

    /// <summary>
    /// Gets or sets the value indicating whether the user is enabled.
    /// </summary>
    bool? Enabled { get; set; }

    /// <summary>
    /// Represents the birth year of a user.
    /// </summary>
    int? BirthYear { get; set; }

    /// <summary>
    /// The Gender enum represents the gender of a person.
    /// </summary>
    Gender? Gender { get; set; }

    /// <summary>
    /// Represents an avatar property of a user.
    /// </summary>
    string? Avatar { get; set; }

    /// <summary>
    /// Represents a Photo property for a user.
    /// </summary>
    string? Photo { get; set; }
}