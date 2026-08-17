using System.Security.Claims;

namespace TownBites.API.Helpers
{
    public static class UserClaimsHelper
    {
        public static int? GetRestaurantId(ClaimsPrincipal user)
        {
            var claim = user.FindFirst("RestaurantId");

            if (claim == null)
                return 2;

            if (!int.TryParse(claim.Value, out var restaurantId))
                return 2;

            return restaurantId;
        }
    }
}
