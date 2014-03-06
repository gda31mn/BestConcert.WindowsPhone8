using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BestConcert.WP8.Core.ApiBestConcert;
using BestConcert.WP8.Model;
using Newtonsoft.Json;

namespace BestConcert.WP8.Core.Provider
{
    public static class ManagementProvider
    {

        #region User
        /// <summary>
        /// Vérification de la connection au site
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>Boolean</returns>
        public static async Task<string> SignInAsync (string username, string password)
        {
            try
            {
                var result = await BestConcertManagement.Login(username, password);

                return result;
            }
            catch (Exception exception)
            {

                throw new Exception(exception.Message);
            }
        }

        public static async Task<List<UserModel>> GetAllUserAsync()
        {
            try
            {
                var req = await BestConcertManagement.GetAllUserAsync();
                var result = JsonConvert.DeserializeObject<List<UserModel>>(req);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static async Task<bool> AddUserAsync(string firstName, string lastName, string password, string email,
            string address)
        {
            try
            {
                var result = await BestConcertManagement.AddUserAsync(firstName, lastName, password, email, address);

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region Concert

        public static async Task<List<ConcertModel>> GetAllConcertAsync()
        {
            try
            {
                var req = await BestConcertManagement.GetAllConcertAsync();
                var result = JsonConvert.DeserializeObject<List<ConcertModel>>(req);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
        




    }
}
