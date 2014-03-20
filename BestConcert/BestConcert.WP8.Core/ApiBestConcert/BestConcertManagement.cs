using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using WebRequest = BestConcert.WP8.Core.HttpRequest.WebRequest;

namespace BestConcert.WP8.Core.ApiBestConcert
{
    internal static class BestConcertManagement
    {
        private const string UrlBestConcert = "http://webserviceing3.cloudapp.net/API/";

        #region User
        internal static async Task<object[]> Login(string username, string password)
        {
            try
            {
                var result = new object[2];

                var requestUrl = string.Format(UrlBestConcert + "/user/SignInAndGetUserInfo?email={0}&password={1}", username, password);
                var req = await WebRequest.Create(requestUrl, "GET", false);

                if (req == null || req == "null" )
                {
                    result[0] = false;
                    result[1] = null;

                    return result;
                }

                result[0] = true;
                result[1] = req;

                return result;
            }
            catch (WebException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        internal static async Task<string> GetAllUserAsync()
        {
            try
            {
                var requestUrl = string.Format(UrlBestConcert + "/user/getAll");

                var req = await WebRequest.Create(requestUrl, "GET", false);

                return req;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        internal static async Task<string> AddUserAsync(string firstName, string lastName, string password, string email, string address)
        {
            try
            {
                if (!String.IsNullOrEmpty(firstName) && !String.IsNullOrEmpty(lastName) && !String.IsNullOrEmpty(password) && !String.IsNullOrEmpty(email) && !String.IsNullOrEmpty(address))
                    throw new Exception("Invalide Info !");

                var requestUrl = string.Format(UrlBestConcert + "/user/adduser?firstName={0}&lastName={1}&password={2}&email={3}&address={4}", firstName, lastName, password, email, address);
                var req = await WebRequest.Create(requestUrl, "GET", false);

                return req;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        internal static async Task<string> GetUserAsync(string token)
        {
            try
            {
                if (!String.IsNullOrEmpty(token))
                    throw new Exception("Invalide Info !");

                var requestUrl = string.Format(UrlBestConcert + "/user/getuser?token={0}", token);
                var req = await WebRequest.Create(requestUrl, "GET", false);

                return req;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        internal static async Task<string> SetUserAsync(string token, string firstName, string lastName, string password,
            string email, string address)
        {
            try
            {
                if (!String.IsNullOrEmpty(token) && !String.IsNullOrEmpty(firstName) && !String.IsNullOrEmpty(lastName) && !String.IsNullOrEmpty(password) && !String.IsNullOrEmpty(email) && !String.IsNullOrEmpty(address))
                    throw new Exception("Invalide Info !");

                var requestUrl = string.Format(UrlBestConcert + "/user/setuser?userid={0}&firstName={1}&lastName={2}&password={3}&email={4}&address={5}", token, firstName, lastName, password, email, address);
                var req = await WebRequest.Create(requestUrl, "GET", false);

                return req;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        internal static async Task<string> SetUserPasswordAsync(string token, string password)
        {
            try
            {
                if (!String.IsNullOrEmpty(token) && !String.IsNullOrEmpty(password))
                    throw new Exception("Invalide Info !");

                var requestUrl = string.Format(UrlBestConcert + "/user/setuserpassword?token={0}&password={1}", token, password);
                var req = await WebRequest.Create(requestUrl, "GET", false);

                return req;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region Concert

        internal static async Task<string> GetAllConcertAsync()
        {
            try
            {

                var requestUrl = string.Format(UrlBestConcert + "/concert/getall");
                var req = await WebRequest.Create(requestUrl, "GET", false);

                return req;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        internal static async Task<string> GetConcertByIdAsync(string id)
        {
            try
            {
                if (!String.IsNullOrEmpty(id))
                    throw new Exception("Invalid Parameter !");

                var requestUrl = string.Format(UrlBestConcert + "/concert/getbyid?id={0}", id);
                var req = await WebRequest.Create(requestUrl, "GET", false);

                return req;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        internal static async Task<string> GetConcertByGenreAsync(string genre)
        {
            try
            {
                if (!String.IsNullOrEmpty(genre))
                    throw new Exception("Invalid Parameter !");

                var requestUrl = string.Format(UrlBestConcert + "/concert/getbygenre?genre={0}", genre);
                var req = await WebRequest.Create(requestUrl, "GET", false);

                return req;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        internal static async Task<string> GetConcertByArtistAsync(string artist)
        {
            try
            {
                if (!String.IsNullOrEmpty(artist))
                    throw new Exception("Invalid Parameter !");

                var requestUrl = string.Format(UrlBestConcert + "/concert/getbyartist?genre={0}", artist);
                var req = await WebRequest.Create(requestUrl, "GET", false);

                return req;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region Order

        internal static async Task<string> GetAllOrdersFromUserIdAsync(string token)
        {
            try
            {
                if (!String.IsNullOrEmpty(token))
                    throw new Exception("Invalid Parameter !");

                var requestUrl = string.Format(UrlBestConcert + "/order/getallordersfromuserid?token={0}", token);
                var req = await WebRequest.Create(requestUrl, "GET", false);

                return req;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        internal static async Task<string> GetCurrentOrderFromUserIdAsync(string token)
        {
            try
            {
                if (!String.IsNullOrEmpty(token))
                    throw new Exception("Invalid Parameter !");

                var requestUrl = string.Format(UrlBestConcert + "/order/getcurrentorderfromuserid?token={0}", token);
                var req = await WebRequest.Create(requestUrl, "GET", false);

                return req;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        internal static async Task<string> GetHistoryOrdersFromUserIdAsync(string token)
        {
            try
            {
                if (!String.IsNullOrEmpty(token))
                    throw new Exception("Invalid Parameter !");

                var requestUrl = string.Format(UrlBestConcert + "/order/gethistoryordersfromuserid?token={0}", token);
                var req = await WebRequest.Create(requestUrl, "GET", false);

                return req;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        internal static async Task<string> AddOrderItemAsync(string token, string concertId, string quantity)
        {
            try
            {
                if (!String.IsNullOrEmpty(token) && !String.IsNullOrEmpty(concertId) && !String.IsNullOrEmpty(quantity))
                    throw new Exception("Invalid Parameter !");

                var requestUrl = string.Format(UrlBestConcert + "/order/addorderitem?token={0}&concerId={1}&quantity={2}", token, concertId, quantity);
                var req = await WebRequest.Create(requestUrl, "GET", false);

                return req;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        internal static async Task<string> PayeOrderAsync(string orderId)
        {
            try
            {
                if (!String.IsNullOrEmpty(orderId))
                    throw new Exception("Invalid Parameter !");

                var requestUrl = string.Format(UrlBestConcert + "/order/payeorder?orderId={0}", orderId);
                var req = await WebRequest.Create(requestUrl, "GET", false);

                return req;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region OrderItem

        internal static async Task<string> DeleteOrderItemAsync()
        {
            return null;
        }
        #endregion
    }
}
