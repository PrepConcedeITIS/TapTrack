using Microsoft.AspNetCore.Authorization;

namespace TapTrackAPI.Core.Base
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class AuthorizedApiController: ApiBaseController
    {
        
    }
}