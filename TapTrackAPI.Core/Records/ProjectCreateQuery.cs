using System.Net.Http;
using Microsoft.AspNetCore.Http;

namespace TapTrackAPI.Core.Records
{
    public record ProjectCreateQuery(string Name, string IdVisible, string Description, MultipartFormDataContent FormDataContent);
}