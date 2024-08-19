using System.Text.Json.Serialization;

namespace Gis.Net.Auth.DTO;

public class FullUserRequestDto : ReducedUserRequestDto, IFullUser
{
    [JsonPropertyName("companyName")] public string? CompanyName { get; set; }
    [JsonPropertyName("phone")] public string? Phone { get; set; }
    [JsonPropertyName("mobile")] public string? Mobile { get; set; }
    [JsonPropertyName("vatNumber")] public string? VatNumber { get; set; }
    [JsonPropertyName("fiscalCode")] public string? FiscalCode { get; set; }
    [JsonPropertyName("address")] public string? Address { get; set; }
    [JsonPropertyName("city")] public string? City { get; set; }
    [JsonPropertyName("region")] public string? Region { get; set; }
    [JsonPropertyName("zipCode")] public string? ZipCode { get; set; }
    [JsonPropertyName("country")] public string? Country { get; set; }
    [JsonPropertyName("enabled")] public bool? Enabled { get; set; }
    [JsonPropertyName("gender")] public Gender? Gender { get; set; }
    [JsonPropertyName("birthday")] public int? BirthYear { get; set; }
    [JsonPropertyName("avatar")] public string? Avatar { get; set; }
    [JsonPropertyName("photo")] public string? Photo { get; set; }
}