using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Testing.End_To_End
{
    [TestClass]
    public class SpecificationBased
    {
        private string connectionString;
        private string basePathAPI;
        private ConfigurationHelper config = new ConfigurationHelper();

        public SpecificationBased()
        {
            config.ResolvePaths(out connectionString, out basePathAPI);
        }

        class requestObject
        {
            public DateTime start;
            public DateTime end;
            public string customerComment;
        }

        [TestMethod]
        public void BookRoomTest_StartDateInThePast_Invalid()
        {
            //arrange
            //test data
            var roomId = 5;

            //formatting and serialization
            var json = JsonConvert.SerializeObject(new requestObject()
            {
                start = DateTime.Today.AddDays(-1),
                end = DateTime.Today,
                customerComment = "No comment"
            });
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            //request construction
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(basePathAPI);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "api/booking/bookRoom/" + roomId)
            {
                Content = data
            };

            //act
            var res = client.SendAsync(request).Result;

            //assert
            Assert.AreEqual(res.StatusCode, System.Net.HttpStatusCode.BadRequest);
        }
    }
}
