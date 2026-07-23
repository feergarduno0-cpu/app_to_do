using System;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;

namespace DogService.WCF
{
    public class DogService : IDogService
    {
        public string ObtenerPerritoDelDia()
        {
            try
            {
                string url = "https://dog.ceo/api/breeds/image/random";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    string json = reader.ReadToEnd();
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    DogApiResponse result = serializer.Deserialize<DogApiResponse>(json);

                    if (result != null && result.status == "success")
                    {
                        return result.message;
                    }
                }
            }
            catch
            {
                // Manejo silencioso de excepción
            }

            return string.Empty;
        }
    }
}