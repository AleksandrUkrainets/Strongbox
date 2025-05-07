using Microsoft.AspNetCore.Mvc;
using Strongbox.Domain.Entities;
using System.Security.Claims;

namespace Strongbox.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiBaseController : ControllerBase
    {
        internal bool TryGetCurrentUser(out Guid userId, out PersonRole role)
        {
            userId = Guid.Empty;
            role = default;

            var subClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var roleClaim = User.FindFirstValue(ClaimTypes.Role);

            if (string.IsNullOrWhiteSpace(subClaim) ||
                string.IsNullOrWhiteSpace(roleClaim) ||
                !Guid.TryParse(subClaim, out userId) ||
                !Enum.TryParse<PersonRole>(roleClaim, out role))
            {
                return false;
            }

            return true;
        }

        internal bool TryGetCurrentUserId(out Guid userId)
        {
            userId = Guid.Empty;

            var subClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrWhiteSpace(subClaim) ||
                !Guid.TryParse(subClaim, out userId))
            {
                return false;
            }

            return true;
        }
    }
}
