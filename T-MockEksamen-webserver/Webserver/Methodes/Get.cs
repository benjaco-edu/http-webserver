using System;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace T_MockEksamen_webserver.Webserver.Methodes
{
    internal class Get
    {
        public static void Execute(List<String> header, string url, StreamWriter streamWriter)
        {
            Console.WriteLine("get " + url);

            if (File.Exists(Config.Path + url))
            {
                Console.WriteLine("200");

                Response.SendHeader(200, streamWriter,
                    MimeMapping.GetMimeMapping(Config.Path + url),
                    new System.IO.FileInfo(Config.Path + url).Length);


                using (FileStream stream = new FileStream(Config.Path + url, FileMode.Open))
                {
                    stream.CopyTo(streamWriter.BaseStream);
                }

                streamWriter.Close();
                
            }
            else
            {
                Console.WriteLine("404");

                Response.SendHeader(404, streamWriter);
                streamWriter.WriteLine("404 file not found");

                streamWriter.Close();
            }
        }
    }
}