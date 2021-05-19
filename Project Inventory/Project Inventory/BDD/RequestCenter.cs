using System;
using System.Net;
using System.IO;

namespace Project_Inventory.Tools
{
    public enum HttpVerb
    {
        GET,
        POST,
        PUT,
        DELETE
    }

    public class RequestCenter
    {
        public string http { get; set; }
        public string port { get; set; }
        public string endPoint { get; set; }
        public HttpVerb httpMethod { get; set; }

        public RequestCenter()
        {
            http = "http://localhost:";
            port = "5000/";
            endPoint = string.Empty;
            httpMethod = HttpVerb.GET;
        }

        public string MakeRequest(string json)
        {
            string strResponseValue = string.Empty;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(http + port + endPoint);

            request.Method = httpMethod.ToString();

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(json);
            }

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new ApplicationException("error code: " + response.StatusCode.ToString());
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

            return strResponseValue;
        }

        public string GetRequest(string requestString, string json)
        {
            endPoint = requestString;
            httpMethod = HttpVerb.GET;

            return MakeRequest(json);
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

        public string DeleteRequest(string requestString, string json)
        {
            endPoint = requestString;
            httpMethod = HttpVerb.DELETE;

            return MakeRequest(json);
        }
    }
}
