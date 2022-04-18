using DAL = GestBieresApi_DAL.Entities;
using API = GestBieresApi.Models;

namespace GestBieresApi.Tools
{
    public static class Mappers
    {
        public static API.AppUser ToApi(this DAL.AppUser user)
        {
            if (user is null) { throw new ArgumentNullException("Oops! Non-existing User"); }
            return new API.AppUser
            {
                Id = user.Id,
                Email = user.Email,
                IsAdmin = user.IsAdmin,
                Nickname = user.Nickname
            };
        }

    }
}
