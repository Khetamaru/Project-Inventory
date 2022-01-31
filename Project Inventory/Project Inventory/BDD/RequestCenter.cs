using System;
using System.Net;
using System.IO;
using System.Text;
using System.Configuration;

namespace Project_Inventory.Tools
{
    /// <summary>
    /// Define request's type
    /// </summary>
    public enum HttpVerb
    {
        GET,
        POST,
        PUT,
        DELETE,
        OPTIONS
    }

    /// <summary>
    /// Use to speak with the API
    /// </summary>
    public class RequestCenter
    {
        public string http { get; set; }
        public string port { get; set; }
        public string endPoint { get; set; }
        public HttpVerb httpMethod { get; set; }

        public RequestCenter()
        {
            http = ConfigurationManager.AppSettings["http"];
            port = ConfigurationManager.AppSettings["port"];
            endPoint = string.Empty;
            httpMethod = HttpVerb.GET;
        }

        /// <summary>
        /// Call the API with a json
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        private string MakeRequest(string json)
        {
            string strResponseValue = string.Empty;

            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(http + port + endPoint); 
            request.KeepAlive = false;
            request.ProtocolVersion = HttpVersion.Version10;
            request.Method = httpMethod.ToString();

            // turn our request string into a byte stream
            byte[] postBytes = Encoding.UTF8.GetBytes(json);

            request.ContentType = "application/json; charset=UTF-8";
            request.Accept = "application/json";
            request.ContentLength = postBytes.Length;
            Stream requestStream = request.GetRequestStream();

            // now send it
            try
            {
                requestStream.Write(postBytes, 0, postBytes.Length);
                requestStream.Close();

                // grab the response and print it out to the console along with the status code
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                string result;
                using (StreamReader rdr = new StreamReader(response.GetResponseStream()))
                {
                    result = rdr.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                CatchError(e, json);
            }

            return strResponseValue;
        }

        /// <summary>
        /// Call the API without json
        /// </summary>
        /// <returns></returns>
        private string MakeRequest()
        {
            string strResponseValue = string.Empty;
            HttpWebResponse response = null;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(http + port + endPoint);

            request.Method = httpMethod.ToString();

            try
            {
                using (response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        PopUpCenter.MessagePopup(response.StatusCode.ToString());
                    }

                    using (Stream responseStream = response.GetResponseStream())
                    {
                        if (responseStream != null)
                        {
                            using (StreamReader reader = new StreamReader(responseStream))
                            {
                                strResponseValue = reader.ReadToEnd();
                            }
                        }
                    }
                }
            }
            catch(Exception e)
            {
                CatchError(e);
            }

            return strResponseValue;
        }

        public string GetRequest(string requestString)
        {
            endPoint = requestString;
            httpMethod = HttpVerb.GET;

            return MakeRequest();
        }

        public string PostRequest(string requestString, string json)
        {
            endPoint = requestString;
            httpMethod = HttpVerb.POST;

            return MakeRequest(json);
        }

        public string PutRequest(string requestString, string json)
        {
            endPoint = requestString;
            httpMethod = HttpVerb.PUT;

            return MakeRequest(json);
        }

        public string DeleteRequest(string requestString)
        {
            endPoint = requestString;
            httpMethod = HttpVerb.DELETE;

            return MakeRequest();
        }

        public string OptionRequest(string requestString)
        {
            endPoint = requestString;
            httpMethod = HttpVerb.OPTIONS;

            return MakeRequest();
        }

        public string OptionRequest(string requestString, string json)
        {
            endPoint = requestString;
            httpMethod = HttpVerb.OPTIONS;

            return MakeRequest(json);
        }

        private void CatchError(Exception e, string json)
        {
            PopUpCenter.MessagePopup("Une erreur a eu lieu pendant la communication entre le programme et le serveur.\n\n" +
                                     http + port + endPoint + "\n\n" +
                                     e.ToString() + "\n\n" + 
                                     json);
        }

        private void CatchError(Exception e)
        {
            PopUpCenter.MessagePopup("Une erreur a eu lieu pendant la communication entre le programme et le serveur.\n\n" +
                                     http + port + endPoint + "\n\n" +
                                     e.ToString() + "\n\n");
        }
    }
}
