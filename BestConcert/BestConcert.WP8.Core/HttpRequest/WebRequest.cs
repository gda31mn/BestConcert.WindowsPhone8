using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace BestConcert.WP8.Core.HttpRequest
{
    public static class WebRequest
    {
        private static string _cookie;

        public static void RemoveCookie()
        {
            _cookie = null;
        }

        public static async Task<string> Create(string url, string method)
        {
            var request = (HttpWebRequest) System.Net.WebRequest.Create(url);

            request.Method = method;

            return await GetResponse(request, false);
        }

        public static async Task<string> Create(string url, string method, bool cookie)
        {
            if (!cookie)
            {
                return await Create(url, method);
            }

            var request = (HttpWebRequest) System.Net.WebRequest.Create(url);
            request.Method = method;

            return await GetResponse(request, true);
        }

        public static async Task<string> CreateWithCookie(string url, string method)
        {
            var request = (HttpWebRequest) System.Net.WebRequest.Create(url);

            request.Method = method;
            request.Headers["Cookie"] = _cookie;

            return await GetResponse(request, false);
        }

        public static async Task<string> Create(string url, string login, string password, string method)
        {
            var request = (HttpWebRequest) System.Net.WebRequest.Create(url);

            request.UseDefaultCredentials = false;

            request.Credentials = new NetworkCredential(login, password);

            request.Method = method;

            return await GetResponse(request, false);
        }

        public static async Task<string> Create(string url, string parameters, string login, string password, string method)
        {
            var request = (HttpWebRequest) System.Net.WebRequest.Create(url);

            request.UseDefaultCredentials = false;

            request.Credentials = new NetworkCredential(login, password);

            request.Method = method;

            byte[] parametersArray = System.Text.Encoding.UTF8.GetBytes(parameters);

            //request.ContentLength = parameters.Length;

            request.ContentType = "application/x-www-form-urlencoded";

            using (var stream = await request.GetRequestStreamAsync().ConfigureAwait(false))
            {
                stream.Write(parametersArray, 0, parametersArray.Length);
            }

            return await GetResponse(request, false);
        }

        private static async Task<string> GetResponse(HttpWebRequest request, bool cookie)
        {
            string result = null;
            
            var response = await request.GetResponseAsync().ConfigureAwait(false);
            
            if (((HttpWebResponse) response).StatusCode == System.Net.HttpStatusCode.OK)
            {
                using (var stream = response.GetResponseStream())
                {
                    if (stream != null)
                    {
                        var buffer = new byte[2048];

                        using (var destinationStream = new MemoryStream())
                        {
                            int bytesRead;

                            do
                            {
                                bytesRead = await stream.ReadAsync(buffer, 0, 2048).ConfigureAwait(false);

                                destinationStream.Write(buffer, 0, bytesRead);
                            } while (bytesRead != 0);

                            result = System.Text.Encoding.UTF8.GetString(destinationStream.ToArray(), 0,
                                destinationStream.ToArray().Length);

                            if (cookie)
                                _cookie = response.Headers["Set-Cookie"];
                        }
                    }
                }
            }

            return result;
        }
    }
}