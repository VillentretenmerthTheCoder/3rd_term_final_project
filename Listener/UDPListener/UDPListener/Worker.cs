using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace UDPListener
{
    class Worker
    {
        private string URL = "https://conversationrestservice20191209113100.azurewebsites.net/api/conversations";

        public async Task<IList<Conversation>> GetAllDataAsync()
        {
            using (HttpClient client = new HttpClient())
            {

                string content = await client.GetStringAsync(URL);
                IList<Conversation> cList = JsonConvert.DeserializeObject<IList<Conversation>>(content);
                return cList;
            }
        }

        public async Task PostDataAsync(string data)
        {
            using (HttpClient client = new HttpClient())
            {
                var content = new StringContent(data, Encoding.UTF8, "application/json");
                await client.PostAsync(URL, content);
            }
        }


    }
}
