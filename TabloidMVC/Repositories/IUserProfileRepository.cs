using System.Collections.Generic;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public interface IUserProfileRepository
    {
        UserProfile GetByEmail(string email);
        List< UserProfile> GetAllUsers();
        UserProfile GetUsersById(int id);
        void DeactivateUser(UserProfile user);
        void ReactivateUser(UserProfile user);
        List<UserProfile> GetDeactivated();
         public UserProfile Register(UserProfile user);

        void UpdateUser(UserProfile user);

        public int Admin();
    }
}