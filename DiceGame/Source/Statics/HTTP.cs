using System;
using System.Collections.Generic;
using System.Management;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace DiceGame.Source
{
    internal static class Http
    {
        /// <summary>
        /// Link for this applications API 
        /// </summary>
        private static readonly string host = "https://dicegame3401.azure-api.net/3401diceGame";

        /// <summary>
        /// Authentication key for api access
        /// </summary>
        private static readonly string key = "bf677d4f09554aeeaa6bf6c02da6ff57";
        private static HttpClient client;

        /// <summary>
        /// Initializes static class for HttpClient instance and sets Auth key in header
        /// </summary>
        public static void Initialize()
        {
            client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(60);
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", key);
        }

        /// <summary>
        /// Static to get current external ip
        /// Uses "https://ipinfo.io/ip"
        /// </summary>
        /// <returns>Current external Ip</returns>
        public static async Task<string> GetIp()
        {
            try
            {
                var response = await client.GetAsync("https://ipinfo.io/ip");
                return await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(e);
                return "";
            }
        }

        #region DiceList

        /// <summary>
        /// Connects to API to get current dicelist from database
        /// </summary>
        /// <param name="aState">Function to update states</param>
        /// <returns>List of Json Dice objects</returns>
        public static async Task<List<DiceJson>> GetDiceListAsync(Action<ELoadingState> aState)
        {
            const string path = "/getdicelist";

            aState.Invoke(ELoadingState.S_Loading);

            var uri = host + path;

            try
            {
                var response = await client.GetAsync(uri);
                response.EnsureSuccessStatusCode(); //If there was any error, this throws an exception

                var contentString = await response.Content.ReadAsStringAsync();

                aState.Invoke(ELoadingState.S_Loaded);

                //Convert Json from the http request into a list of objects
                var json = JsonConvert.DeserializeObject<List<DiceJson>>(contentString);

                return json;
            }
            catch (HttpRequestException ex)
            {
                aState.Invoke(ELoadingState.S_Failed);
                MessageBox.Show(ex.Message, @"Error", MessageBoxButtons.OK);
                //Application.Exit();
            }

            return null;
        }

        /// <summary>
        /// Called up upload new dice data into the database
        /// Either new dice or updating old dice
        /// </summary>
        /// <param name="aDice">List of dice to update/add</param>
        /// <returns></returns>
        public static async Task UpdateDiceListAsync(List<Dice> aDice)
        {
            var path = "/UpdateDiceList";

            var uri = host + path;

            Console.WriteLine(@"Update Hit");

            try
            {
                var content = new MultipartFormDataContent();

                //Convert list of dice into form data to send with the POST request
                foreach (var dice in aDice)
                    content.Add(new StringContent(string.Join(",", dice.Faces)), dice.Name);

                var response = await client.PostAsync(uri, content);
                response.EnsureSuccessStatusCode(); //Throw exception if Http request returns error

                Console.WriteLine(response.Content);
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK);
                //Application.Exit();
            }
        }

        /// <summary>
        /// Called to delete given dice
        /// </summary>
        /// <param name="aDice">Dice to delete</param>
        /// <returns></returns>
        public static async Task DeleteDiceAsync(List<Dice> aDice)
        {
            const string path = "/DeleteDice";

            var uri = host + path;

            Console.WriteLine(@"Delete Hit");

            try
            {
                var req = new HttpRequestMessage(HttpMethod.Post, uri);
                var content = new MultipartFormDataContent();

                req.Headers.Add(@"passw", @"TestingDice");

                //Convert list of dice into form data to send with the POST request
                foreach (var dice in aDice) 
                    content.Add(new StringContent(""), dice.Name);

                req.Content = content;

                var response = await client.SendAsync(req);
                Console.WriteLine(await response.Content.ReadAsStringAsync());
                response.EnsureSuccessStatusCode(); //Throw exception if Http request returns error
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show(ex.Message, @"Error", MessageBoxButtons.OK);
                //Application.Exit();
            }
        }

        #endregion

        #region Sessions
        
        /// <summary>
        /// Called to register a session in the database using API calls
        /// </summary>
        /// <param name="aName">Session name</param>
        /// <param name="aIp">Server Ip</param>
        /// <param name="aPassword">Password (Not implemented)</param>
        /// <returns></returns>
        public static async Task RegisterSession(string aName, string aIp, string aPassword = "")
        {
            var path = "/RegisterSession";

            var uri = host + path;

            Console.WriteLine(@"Registering Session");

            try
            {
                var content = new MultipartFormDataContent();

                //Add form data to send with the POST request
                content.Add(new StringContent(aName), "name");
                content.Add(new StringContent(aPassword), "pass");
                content.Add(new StringContent(aIp), "ip");

                var response = await client.PostAsync(uri, content);
                response.EnsureSuccessStatusCode(); //Throws exception if Http request returns error

                Console.WriteLine(response.Content);
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK);
                //Application.Exit();
            }
        }

        /// <summary>
        /// Removes session register from database using Api calls
        /// </summary>
        /// <returns></returns>
        public static async Task CloseSessionAsync()
        {
            const string path = "/CloseSession";

            var uri = host + path;

            Console.WriteLine(@"Closing Session");

            try
            {
                var req = new HttpRequestMessage(HttpMethod.Post, uri);
                var content = new MultipartFormDataContent();

                var ip = await GetIp();

                //Add form data to send with the POST request
                content.Add(new StringContent(ip), "ip");

                req.Content = content;

                var response = await client.SendAsync(req).ConfigureAwait(false);
                Console.WriteLine(await response.Content.ReadAsStringAsync() + @" Session Closed");
                response.EnsureSuccessStatusCode(); //Throws exception if Http request returns error
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show(ex.Message, @"Error", MessageBoxButtons.OK);
            }
        }

        /// <summary>
        /// Gets currently registered sessions from the database using Api calls
        /// </summary>
        /// <returns>List of currently active servers</returns>
        public static async Task<List<ServerJson>> GetSessionListAsync()
        {
            const string path = "/getSessionlist";

            var uri = host + path;

            try
            {
                var response = await client.GetAsync(uri);
                response.EnsureSuccessStatusCode(); //Throws exception if Http request returns error

                var contentString = await response.Content.ReadAsStringAsync();

                //Convert http request json body into a list of usable objects
                var json = JsonConvert.DeserializeObject<List<ServerJson>>(contentString);

                return json;
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show(ex.Message, @"Error", MessageBoxButtons.OK);
                //Application.Exit();
            }

            return null;
        }

        /// <summary>
        /// Get Local IP. Note needed after refactor
        /// </summary>
        /// <returns></returns>
        public static string GetId()
        {
            var mbs = new ManagementObjectSearcher("Select ProcessorId From Win32_processor");
            var mbsList = mbs.Get();
            var id = "";

            foreach (var o in mbsList)
            {
                var mo = (ManagementObject) o;
                id = mo["ProcessorId"].ToString();
                break;
            }

            return id;
        }

        #endregion
    }
}