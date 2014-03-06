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

        public static async Task<UserModel> AddUserAsync(string firstName, string lastName, string password, string email,
            string address)
        {
            try
            {
                var req = await BestConcertManagement.AddUserAsync(firstName, lastName, password, email, address);
                var result = JsonConvert.DeserializeObject<UserModel>(req);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static async Task<UserModel> GetUserAsync(string userId)
        {
            try
            {
                var req = await BestConcertManagement.GetUserAsync(userId);
                var result = JsonConvert.DeserializeObject<UserModel>(req);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static async Task<UserModel> SetUserAsync(string userId, string firstName, string lastName, string password,
            string email, string address)
        {
            try
            {
                var req = await BestConcertManagement.SetUserAsync( userId, firstName, lastName, password, email, address);
                var result = JsonConvert.DeserializeObject<UserModel>(req);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static async Task<UserModel> SetUserPasswordAsync(string userId, string password)
        {
            try
            {
                var req = await BestConcertManagement.SetUserPasswordAsync(userId, password);
                var result = JsonConvert.DeserializeObject<UserModel>(req);

                return result;
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

        public static async Task<ConcertModel> GetConcertByIdAsync(string id)
        {
            try
            {
                var req = await BestConcertManagement.GetConcertByIdAsync(id);
                var result = JsonConvert.DeserializeObject<ConcertModel>(req);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static async Task<List<ConcertModel>> GetConcertByGenreAsync(string genre)
        {
            try
            {
                var req = await BestConcertManagement.GetConcertByGenreAsync(genre);
                var result = JsonConvert.DeserializeObject<List<ConcertModel>>(req);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static async Task<List<ConcertModel>> GetConcertByArtistAsync(string artist)
        {
            try
            {
                var req = await BestConcertManagement.GetConcertByArtistAsync(artist);
                var result = JsonConvert.DeserializeObject<List<ConcertModel>>(req);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region Order

        public static async Task GetAllOrdersFromUserIdAsync()
        {
            try
            {
                var req = await BestConcertManagement.GetAllOrdersFromUserIdAsync();
                //var result = JsonConvert.DeserializeObject<List<ConcertModel>>(req);

                //return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}
