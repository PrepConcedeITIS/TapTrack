using Microsoft.AspNetCore.Authorization;

namespace TapTrackAPI.Core.Base
{
    [Authorize]
    public class AuthorizedApiController: ApiBaseController
    {
        
    }
}