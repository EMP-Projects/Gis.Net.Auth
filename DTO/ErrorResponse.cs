using System.Text.Json.Serialization;

namespace Gis.Net.Auth.DTO;

/// <summary>
/// Represents an error response message.
/// </summary>
public class ErrorResponse
{
    /// <summary>
    /// Represents an error response returned by the API.
    /// </summary>
    public string Error { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IEnumerable<string>? Validations { get; set; }

    /// <summary>
    /// Represents an error response object.
    /// </summary>
    public ErrorResponse(string errorMessage)
    {
        Error = errorMessage;
    }

    /// <summary>
    /// Represents an error response.
    /// </summary>
    public ErrorResponse(IEnumerable<string> validationErrors)
    {
        Error = $"Validation errors";
        Validations = validationErrors;
    }
}