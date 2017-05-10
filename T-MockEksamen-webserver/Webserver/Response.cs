using System;
using System.Collections.Generic;
using System.IO;

namespace T_MockEksamen_webserver.Webserver
{
    internal class Response
    {
        private static Dictionary<int, String> statuscodes = new Dictionary<int, string>()
        {
            {200, "OK"},
            {201, "Created"},
            {202, "Accepted"},
            {204, "No Content"},
            {301, "Moved Permanently"},
            {302, "Moved Temporarily"},
            {304, "Not Modified"},
            {400, "Bad Request"},
            {401, "Unauthorized"},
            {403, "Forbidden"},
            {404, "Not Found"},
            {500, "Internal Server Error"},
            {501, "Not Implemented"},
            {502, "Bad Gateway"},
            {503, "Service Unavailable"},
        };

        public static void SendHeader( int statuscode, StreamWriter streamWriter, string type = "html/text", long ?length = null)
        {
            

            streamWriter.WriteLine("HTTP-Version: HTTP/1.0 " + statuscode + " " + statuscodes[statuscode]);
            streamWriter.WriteLine("content-type: "+type);

            if (length !=  null)
            {
                streamWriter.WriteLine("content-length: "+length);
            }

            streamWriter.WriteLine("Connection: close");

            streamWriter.WriteLine();
        }
    }
}