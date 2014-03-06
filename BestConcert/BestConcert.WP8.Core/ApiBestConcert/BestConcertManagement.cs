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
    static class BestConcertManagement
    {
        private const string urlBestConcert = "http://webserviceing3.cloudapp.net/API/";

        #region User
        internal static async Task<string> Login(string username, string password)
        {
            try
            {
                var requestUrl = string.Format(urlBestConcert + "/user/signin?email={0}&password={1}", username, password);
                var req = await WebRequest.Create(requestUrl, "GET", false);

                if (req == "-1")
                {
                    throw new Exception("Invalide login/password !");
                }

                return req;
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
                var requestUrl = string.Format(urlBestConcert + "/user/getAll");
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

                var requestUrl = string.Format(urlBestConcert + "/user/AddUser?firstName={0}&lastName={1}&password={2}&email={3}&address={4}", firstName, lastName, password, email, address);
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

                var requestUrl = string.Format(urlBestConcert + "/concert/getall");
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

                var requestUrl = string.Format(urlBestConcert + "/concert/getbyid?id={0}", id);
                var req = await WebRequest.Create(requestUrl, "GET", false);

                return req;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        internal static async Task<string> GetConcertByGenre(string genre)
        {
            try
            {
                if (!String.IsNullOrEmpty(genre))
                    throw new Exception("Invalid Parameter !");

                var requestUrl = string.Format(urlBestConcert + "/concert/getbygenre?genre={0}", genre);
                var req = await WebRequest.Create(requestUrl, "GET", false);

                return req;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        internal static async Task<string> GetConcertByArtist(string artist)
        {
            try
            {
                if (!String.IsNullOrEmpty(artist))
                    throw new Exception("Invalid Parameter !");

                var requestUrl = string.Format(urlBestConcert + "/concert/getbyartist?genre={0}", artist);
                var req = await WebRequest.Create(requestUrl, "GET", false);

                return req;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        #endregion
    }
}
