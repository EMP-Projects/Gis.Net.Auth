using System.Text.Json.Serialization;

namespace Gis.Net.Auth.DTO;

/// <summary>
/// 
/// </summary>
public class FullUserDto : ReducedUserDto, IFullUser
{
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("companyName")] public string? CompanyName { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("phone")] public string? Phone { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("mobile")] public string? Mobile { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("vatNumber")] public string? VatNumber { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("fiscalCode")] public string? FiscalCode { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("address")] public string? Address { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("city")] public string? City { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("region")] public string? Region { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("zipCode")] public string? ZipCode { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("country")] public string? Country { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("enabled")] public bool? Enabled { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("gender")] public Gender? Gender { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("birthday")] public int? BirthYear { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("avatar")] public string? Avatar { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("photo")] public string? Photo { get; set; }
}