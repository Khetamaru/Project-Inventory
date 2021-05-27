using System;
using System.Net;
using System.IO;
using System.Text;

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
            port = "5000/api/";
            endPoint = string.Empty;
            httpMethod = HttpVerb.GET;
        }

        public string MakeRequest(string json)
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
            requestStream.Write(postBytes, 0, postBytes.Length);
            requestStream.Close();

            // grab te response and print it out to the console along with the status code
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string result;
            using (StreamReader rdr = new StreamReader(response.GetResponseStream()))
            {
                result = rdr.ReadToEnd();
            }

            return strResponseValue;
        }

        public string MakeRequest()
        {
            string strResponseValue = string.Empty;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(http + port + endPoint);

            request.Method = httpMethod.ToString();

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

        public string PostRequest(string requestString)
        {
            endPoint = requestString;
            httpMethod = HttpVerb.POST;

            return MakeRequest();
        }

        public string PutRequest(string requestString, string json)
        {
            endPoint = requestString;
            httpMethod = HttpVerb.PUT;

            return MakeRequest(json);
        }

        public string PutRequest(string requestString)
        {
            endPoint = requestString;
            httpMethod = HttpVerb.PUT;

            return MakeRequest();
        }

        public string DeleteRequest(string requestString, string json)
        {
            endPoint = requestString;
            httpMethod = HttpVerb.DELETE;

            return MakeRequest(json);
        }

        public string DeleteRequest(string requestString)
        {
            endPoint = requestString;
            httpMethod = HttpVerb.DELETE;

            return MakeRequest();
        }
    }
}
