using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.IO;
using System.Windows.Forms;

namespace DiceGame
{
    public class DiceJson
    {
        [JsonProperty("diceName")]
        public string diceName { get; set; }
        [JsonProperty("diceFace")]
        public string diceFace { get; set; }
    }

    static class HTTP
    {
        static string host = "https://dicegame3401.azure-api.net/3401diceGame";

        static string key = "bf677d4f09554aeeaa6bf6c02da6ff57";

        public async static Task<List<DiceJson>> getDiceListAsync()
        {
            string path = "/getdicelist";

            HttpClient client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(10);
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", key);

            string uri = host + path;

            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                response.EnsureSuccessStatusCode();

                string contentString = await response.Content.ReadAsStringAsync();

                JsonSerializer serializer = new JsonSerializer();
                List<DiceJson> json = JsonConvert.DeserializeObject<List<DiceJson>>(contentString);

                return json;
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK);
                Application.Exit();
            }

            return null;
        }

        public async static Task UpdateDiceListAsync()
        {
            string path = "/UpdateDiceList";

            HttpClient client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(10);
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", key);

            string uri = host + path;

            try
            {
                MultipartFormDataContent content = new MultipartFormDataContent();
                content.Add(new StringContent("Light,Dark"), "Force");

                var response = await client.PostAsync(uri, content);
                response.EnsureSuccessStatusCode();

                Console.WriteLine(response.Content);
            }
            catch(HttpRequestException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK);
                Application.Exit();
            }
        }
    }
}
