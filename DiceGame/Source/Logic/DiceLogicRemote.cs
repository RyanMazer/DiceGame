using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using DiceGame.Source.Base;
using Microsoft.AspNet.SignalR.Client;

namespace DiceGame.Source.Logic
{
    /// <summary>
    /// Remote Dice Logic class used as a client object to call methods on the server and update local info based on info sent from server
    /// </summary>
    public class DiceLogicRemote : DiceLogicBase
    {
        private HubConnection connection;
        private IHubProxy hubProxy;

        public Dictionary<string, string> Users { get; private set; } = new Dictionary<string, string>();

        public string UserName { get; set; }

        //Get and set all actions to be called later
        private Action<string> updateDice;
        private Action updateUsers;
        private Action disconnect;

        public Action<string> UpdateDice
        {
            set => updateDice = value;
        }

        public Action UpdateUsers
        {
            set => updateUsers = value;
        }

        public Action Disconnect
        {
            set => disconnect = value;
        }

        /// <summary>
        /// Called to close client connection
        /// </summary>
        public override void CloseLogic()
        {
            if (connection.State == ConnectionState.Connected)
                connection.Stop();
        }

        /// <summary>
        /// Request the server to roll a dice and return the result
        /// </summary>
        /// <returns></returns>
        public override async Task<string> DiceRoll()
        {
            if (diceList != null && diceList.Count > 0) return await hubProxy.Invoke<string>("DiceRoll", currentDice);

            return string.Empty;
        }

        public override Task InitializeAsync()
        {
            return null;
        }

        /// <summary>
        /// Connect to server on given Ip 
        /// </summary>
        /// <param name="ip">Ip to connect to</param>
        /// <returns></returns>
        public async Task Connect(string ip)
        {
            await ConnectAsync($"http://{ip}:8080");
            diceList = await hubProxy.Invoke<List<Dice>>("GetDiceList");
        }

        /// <summary>
        /// Create hub connection to SignalR server at the given url using the DiceRollerHub
        /// </summary>
        /// <param name="url">Url to connect to, typically an Ip on port 8080</param>
        /// <returns></returns>
        private async Task ConnectAsync(string url)
        {
            var query = new Dictionary<string, string> {{"name", UserName}};

            try
            {
                connection = new HubConnection(url, query);
                //connection.Credentials = CredentialCache.DefaultNetworkCredentials;
                //Defines an ID in the headers using the processor ID + current time
                //Using current time to keep the ID unique even if user connects on multiples instances using one machine
                connection.Headers.Add("ID", Http.GetId() + ":" + DateTime.Now.ToString("HH:mm:ss"));

                connection.TraceLevel = TraceLevels.All;
                connection.TraceWriter = Console.Out;

                //creates the hub object the connection will use to connect
                //Also creates functions for the server to call on the client and binds them into the hub object
                hubProxy = connection.CreateHubProxy("DiceRollerHub");

                hubProxy.On<string>("UpdateRoll", output => { updateDice(output); });

                hubProxy.On<string, string>("userConnected", (id, user) =>
                {
                    Users.Add(id, user);
                    updateUsers();
                });

                hubProxy.On<Dictionary<string, string>>("UserList", userList =>
                {
                    Users = userList;
                    updateUsers();
                });

                hubProxy.On<string>("clientDisconnect", id =>
                {
                    Users.Remove(id);
                    updateUsers();
                });

                hubProxy.On("Disconnect", () => { disconnect.Invoke(); });
                
                //when the server drops connection just disconnect instead of attempting to reconnect
                connection.Reconnecting += disconnect;

                await connection.Start();

                Console.WriteLine(@"Client: " + connection.State);
            }
            catch (HttpClientException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Called to kick the specified user from the session.
        /// Can be called by every user but will only succeed when called by admin user
        /// Admin user automatically assigned by first connection (aka server host)
        /// </summary>
        /// <param name="connectionId"></param>
        /// <returns></returns>
        public async Task<bool> Kick(string connectionId)
        {
            return await hubProxy.Invoke<bool>("Kick", connectionId);
        }
    }
}