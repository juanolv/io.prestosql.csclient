using System;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Text;

namespace io.prestosql.client
{
    public class Helper
    {
        public static T HttpRequest<T>(string Method, string URL, string Request = "", WebHeaderCollection Headers = null)
        {
            var http = WebRequest.Create(new Uri(URL));
            http.Method = Method;

            if (Headers != null)
                http.Headers.Add(Headers);
            http.Credentials = CredentialCache.DefaultCredentials;

            if (!string.IsNullOrEmpty(Request))
            {
                ASCIIEncoding encoding = new ASCIIEncoding();
                Byte[] bytes = encoding.GetBytes(Request);

                using (Stream newStream = http.GetRequestStream())
                {
                    newStream.Write(bytes, 0, bytes.Length);
                    newStream.Close();
                }
            }
            try
            {
                using (var response = http.GetResponse())
                {                    
                    using (var stream = response.GetResponseStream())
                    {
                        return JsonConvert.DeserializeObject<T>(new StreamReader(stream).ReadToEnd());
                    }
                }
            }
            catch (WebException ex)
            {
                if (ex.Response.ContentLength > 0)
                {
                    using (var stream = ex.Response.GetResponseStream())
                    {
                        var sr = new StreamReader(stream);
                        throw new Exception("ERROR: " + sr.ReadToEnd());
                    }
                }
                else
                    throw new Exception("ERROR: " + ex.Message);

            }
        }
    }
}
