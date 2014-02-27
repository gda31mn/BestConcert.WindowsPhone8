using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebRequest = BestConcert.WP8.Core.HttpRequest.WebRequest;

namespace BestConcert.WP8.Core.ApiBestConcert
{
    static class BestConcertManagement
    {
        private const string urlBestConcert = "";

        public static async Task<bool> Login(string username, string password)
        {
            var result = false;

            try
            {
                var requestUrl = string.Format(urlBestConcert + "?username={0}&password={1}", username, password);
                var req = await WebRequest.Create(requestUrl, "GET", true);

                if (req == "OK")
                {
                    result = true;
                }

                return result;
            }
            catch (WebException ex)
            {
                throw new Exception("No Internet Connexion, sync cancelled.");
            }
        }
    }
}
