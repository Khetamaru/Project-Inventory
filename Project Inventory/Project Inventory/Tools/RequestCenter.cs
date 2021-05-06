using System;
using System.Collections.Generic;
using System.Text;
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
        public string httpUrl { get; set; }
        public string endPoint { get; set; }
        public HttpVerb httpMethod { get; set; }

        public RequestCenter()
        {
            httpUrl = string.Empty;
            endPoint = string.Empty;
            httpMethod = HttpVerb.GET;
        }

        public string MakeRequest()
        {
            string strResponseValue = string.Empty;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(httpUrl + endPoint);

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

        public string GetRequest(string requestString)
        {
            endPoint = requestString;
            httpMethod = HttpVerb.GET;

            return MakeRequest();
        }

        public string PostRequest(string requestString)
        {
            endPoint = requestString;
            httpMethod = HttpVerb.POST;

            return MakeRequest();
        }

        public string PutRequest(string requestString)
        {
            endPoint = requestString;
            httpMethod = HttpVerb.PUT;

            return MakeRequest();
        }

        public string DeleteRequest(string requestString)
        {
            endPoint = requestString;
            httpMethod = HttpVerb.DELETE;

            return MakeRequest();
        }
    }
}
