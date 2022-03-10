using System;
using System.Collections.Generic;
using System.Text;
using ivr.Services;
using ivr.Models;
using System.Threading.Tasks;
using System.Linq;
using Firebase.Database;
using Firebase.Database.Query;

namespace ivr.Services
{
    public class FirebaseHelper
    {
        /*FirebaseClient client;
        public FirebaseHelper()
        {
            client = new FirebaseClient(Constants.FirebasePath);
        }

        private async Task<IEnumerable<User>> GetAllUsers()
        {
            return (await client
                .Child("Users")
                .OnceAsync<User>()).Select(item => item.Object).ToList();
        }

        public async Task AddUser(User user)
        {
            await client
                .Child("Users")
                .PostAsync(user);
        }

        public async Task DeleteUser(string id)
        {
            var toDelete = (await client
                .Child("Users")
                .OnceAsync<User>()).Where(a => a.Object.Id == id).FirstOrDefault();
            await client
                .Child("Users")
                .Child(toDelete.Key).DeleteAsync();
        }

        public async Task<User> GetUser(string id)
        {
            return (await client
                .Child("Users")
                .OnceAsync<User>()).Where(a => a.Object.Id == id).FirstOrDefault().Object;
        }

        public async Task UpdateUser(User user)
        {
            var toUpdate = (await client
                .Child("Users")
                .OnceAsync<User>()).
                Where(a => a.Object.Id == user.Id).FirstOrDefault();
            await client
                .Child("Users")
                .Child(toUpdate.Key)
                .PutAsync(user);
        }*/
    }
}
