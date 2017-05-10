using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task TestGet()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.UseDefaultCredentials = true;


            using (HttpClient client = new HttpClient(clientHandler))
            {
                client.BaseAddress = new Uri("http://127.0.0.1");
         
                try
                {
                    var result = client.GetAsync("unittest/simplefile.txt").Result;
                    if (result.IsSuccessStatusCode)
                    {
                        string resultat = await result.Content.ReadAsStringAsync();

                        Assert.AreEqual("virker", resultat);
                        
                        return;
                    }

                    
                }
                catch (Exception e)
                {
                }
            }

            Assert.Fail();
          
        }
    }
}
