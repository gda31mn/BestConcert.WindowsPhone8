using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BestConcert.WP8.Core.ApiBestConcert;

namespace BestConcert.WP8.Core.Provider
{
    public static class ManagementProvider
    {
        /// <summary>
        /// Vérification de la connection au site
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>Boolean</returns>
        public static async Task<bool> CredentialAsync (string username, string password)
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
            return false;
        }
    }
}
