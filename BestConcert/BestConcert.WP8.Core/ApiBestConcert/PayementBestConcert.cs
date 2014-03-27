using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BestConcert.WP8.Core.HttpRequest;

namespace BestConcert.WP8.Core.ApiBestConcert
{
    internal static class PayementBestConcert
    {
        private const string UrlPayement = "http://bestconcertpaymentapi.azurewebsites.net/API";

        internal static async Task<string> CheckCreditCardAsync(string userToken, string creditCardNumber, string expirationDate)
        {
            try
            {
                var requestUrl = string.Format(UrlPayement + "/Payment/CheckCreditCard?userToken={0}&creditCardNumber={1}&expirationDate={2}",userToken, creditCardNumber, expirationDate);

                var req = await WebRequest.Create(requestUrl, "GET", false);

                return req;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        internal static async Task<string> ValidateSecureTokenAsync(string userToken, string secureToken)
        {
            try
            {
                var requestUrl = string.Format(UrlPayement + "/Payment/ValidateSecureToken?userToken={0}&secureToken={1}", userToken, secureToken);

                var req = await WebRequest.Create(requestUrl, "GET", false);

                return req;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

    }
}
