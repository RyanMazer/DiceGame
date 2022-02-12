using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.IO;
using System.Windows.Forms;
using System.Management;

namespace DiceGame.Source
{

    static class HTTP
    {
        static string host = "https://dicegame3401.azure-api.net/3401diceGame";

        static string key = "bf677d4f09554aeeaa6bf6c02da6ff57";
        static HttpClient client;

        public static void Initialize()
        {
            client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(60);
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", key);
        }

        #region DiceList

        public async static Task<List<DiceJson>> getDiceListAsync(Action<ELoadingState> _state)
        {
            string path = "/getdicelist";

            _state.Invoke(ELoadingState.S_Loading);

            string uri = host + path;

            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                response.EnsureSuccessStatusCode();

                string contentString = await response.Content.ReadAsStringAsync();

                _state.Invoke(ELoadingState.S_Loaded);

                JsonSerializer serializer = new JsonSerializer();
                List<DiceJson> json = JsonConvert.DeserializeObject<List<DiceJson>>(contentString);

                return json;
            }
            catch (HttpRequestException ex)
            {
                _state.Invoke(ELoadingState.S_Failed);
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK);
                //Application.Exit();
            }

            return null;
        }

        public async static Task UpdateDiceListAsync(List<Dice> a_dice)
        {
            string path = "/UpdateDiceList";

            string uri = host + path;

            Console.WriteLine("Update Hit");

            try
            {
                MultipartFormDataContent content = new MultipartFormDataContent();

                foreach (var dice in a_dice)
                {
                    content.Add(new StringContent(String.Join(",", dice.getFaces())), dice.getName());
                }

                var response = await client.PostAsync(uri, content);
                response.EnsureSuccessStatusCode();

                Console.WriteLine(response.Content);
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK);
                //Application.Exit();
            }
        }

        public async static Task DeleteDiceAsync(List<Dice> a_dice)
        {
            string path = "/DeleteDice";

            string uri = host + path;

            Console.WriteLine("Delete Hit");

            try
            {
                HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Post, uri);
                MultipartFormDataContent content = new MultipartFormDataContent();

                req.Headers.Add("passw", "TestingDice");

                foreach (var dice in a_dice)
                {
                    content.Add(new StringContent(""), dice.getName());
                }

                req.Content = content;

                HttpResponseMessage response = await client.SendAsync(req);
                Console.WriteLine(await response.Content.ReadAsStringAsync());
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK);
                //Application.Exit();
            }
        }
        #endregion

        #region Sessions

        public static async Task RegisterSession(string a_name, string a_password = "")
        {
            string path = "/RegisterSession";

            string uri = host + path;

            Console.WriteLine("Registering Session");

            try
            {
                MultipartFormDataContent content = new MultipartFormDataContent();

                content.Add(new StringContent(a_name), "name");
                content.Add(new StringContent(a_password), "pass");
                content.Add(new StringContent(GetId()), "hostid");

                var response = await client.PostAsync(uri, content);
                response.EnsureSuccessStatusCode();

                Console.WriteLine(response.Content);
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK);
                //Application.Exit();
            }
        }

        public async static Task CloseSessionAsync(string a_name)
        {
            string path = "/CloseSession";

            string uri = host + path;

            Console.WriteLine("Closing Session");

            try
            {
                HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Post, uri);
                MultipartFormDataContent content = new MultipartFormDataContent();

                content.Add(new StringContent(a_name), "name");
                content.Add(new StringContent(GetId()), "hostid");

                req.Content = content;

                HttpResponseMessage response = await client.SendAsync(req);
                Console.WriteLine(await response.Content.ReadAsStringAsync());
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK);
            }
        }

        private static string GetId()
        {
            var mbs = new ManagementObjectSearcher("Select ProcessorId From Win32_processor");
            ManagementObjectCollection mbsList = mbs.Get();
            string id = "";
            foreach (ManagementObject mo in mbsList)
            {
                id = mo["ProcessorId"].ToString();
                break;
            }

            return id;
        }

        #endregion
    }
}
