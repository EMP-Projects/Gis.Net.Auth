using System.Text.Json.Serialization;

namespace Gis.Net.Auth.DTO;

public class ErrorResponse
{
    public string Error { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IEnumerable<string>? Validations { get; set; }

    public ErrorResponse(string errorMessage)
    {
        Error = errorMessage;
    }

    public ErrorResponse(IEnumerable<string> validationErrors)
    {
        Error = $"Validation errors";
        Validations = validationErrors;
    }
}