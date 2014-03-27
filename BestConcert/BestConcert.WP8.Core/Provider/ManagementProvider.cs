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
        public static async Task<object[]> SignInAsync (string username, string password)
        {
            try
            {
                var result = await BestConcertManagement.Login(username, password);

                if ((bool)result[0])
                {
                    result[1] = JsonConvert.DeserializeObject<UserModel>((string)result[1]);
                }

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

        public static async Task<UserModel> GetUserAsync(string token)
        {
            try
            {
                var req = await BestConcertManagement.GetUserAsync(token);
                var result = JsonConvert.DeserializeObject<UserModel>(req);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static async Task<UserModel> SetUserAsync(string token, string firstName, string lastName, string password,
            string email, string address)
        {
            try
            {
                var req = await BestConcertManagement.SetUserAsync(token, firstName, lastName, password, email, address);
                var result = JsonConvert.DeserializeObject<UserModel>(req);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static async Task<UserModel> SetUserPasswordAsync(string token, string password)
        {
            try
            {
                var req = await BestConcertManagement.SetUserPasswordAsync(token, password);
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

        public static async Task<List<Order>> GetAllOrdersFromUserTokenAsync(string token)
        {
            try
            {
                var req = await BestConcertManagement.GetAllOrdersFromUserTokenAsync(token);
                var result = JsonConvert.DeserializeObject<List<Order>>(req);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static async Task<Order> GetCurrentOrderFromUserTokenAsync(object token)
        {
            try
            {
                var req = await BestConcertManagement.GetCurrentOrderFromUserTokenAsync(token.ToString());
                var result = JsonConvert.DeserializeObject<Order>(req);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static async Task<List<Order>> GetHistoryOrdersFromUserIdAsync(string token)
        {
            try
            {
                var req = await BestConcertManagement.GetHistoryOrdersFromUserIdAsync(token);
                var result = JsonConvert.DeserializeObject<List<Order>>(req);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static async Task<string> AddOrderItemAsync(string token, string concertId, string quantity)
        {
            try
            {
                return await BestConcertManagement.AddOrderItemAsync(token, concertId, quantity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static async Task PayeOrderAsync(string orderId)
        {
            try
            {
                var req = await BestConcertManagement.PayeOrderAsync(orderId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static async Task DeleteOrderItemAsync(string orderId)
        {
            try
            {
                var req = await BestConcertManagement.DeleteOrderItemAsync(orderId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        #endregion

        #region Payement

        public static async Task<CheckCreditCardModel> CheckCreditCardAsync(string userToken, string creditCardNumber, string expirationDate)
        {
            try
            {
                var req = await PayementBestConcert.CheckCreditCardAsync(userToken, creditCardNumber, expirationDate);
                var result = JsonConvert.DeserializeObject<CheckCreditCardModel>(req);

                return result;

            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public static async Task<ValidateSecureTokenModel> ValidateSecureTokenAsync(string userToken, string secureToken)
        {
            try
            {
                var req = await PayementBestConcert.ValidateSecureTokenAsync(userToken, secureToken);
                var result = JsonConvert.DeserializeObject<ValidateSecureTokenModel>(req);

                return result;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        #endregion
    }
}
