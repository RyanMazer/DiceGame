using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Hosting;
using Owin;

namespace DiceGame.Source
{
    public static class SignalRServer
    {
        /// <summary>
        /// Save server object to close later
        /// </summary>
        private static IDisposable server;

        // Crude way of storing data from the server
        public static Dictionary<string, string> users = new Dictionary<string, string>();
        public static string AdminId = "";
        public static List<Dice> dice;

        /// <summary>
        /// Initialize webapp to host server, using owin on port 8080
        /// </summary>
        /// <returns></returns>
        public static async Task InitializeServerAsync()
        {
            var url = @"http://+:8080";
            server = WebApp.Start<Startup>(url);

            Console.WriteLine(@"Server running on: {0}", url);
        }

        /// <summary>
        /// Shutdown Webapp 
        /// </summary>
        /// <returns></returns>
        public static async Task ShutdownServer()
        {
            server.Dispose();
        }
    }

    /// <summary>
    /// Used to set userID in the Hubs
    /// </summary>
    public class IdBasedUserIdProvider : IUserIdProvider
    {
        /// <summary>
        /// Overwrite for assigning UserID
        /// </summary>
        /// <param name="request"></param>
        /// <returns>New UserID</returns>
        public string GetUserId(IRequest request)
        {
            return request.Headers["ID"];
        }
    }

    /// <summary>
    /// Class used by owin to configure webapp after initialization
    /// </summary>
    internal class Startup
    {
        /// <summary>
        /// Called automatically to configure webapp after launch
        /// </summary>
        /// <param name="app"></param>
        public void Configuration(IAppBuilder app)
        {
            //Allow cross domain calls
            app.UseCors(CorsOptions.AllowAll);

            var hubConfiguration = new HubConfiguration();
            hubConfiguration.EnableJSONP = true;
            hubConfiguration.EnableDetailedErrors = true;

            //Initializes a SignalR server using given hubconfig
            app.MapSignalR(hubConfiguration);

            //Adds out UserID overwrite class to the WebApp to ensure any connection has their ID replaced
            GlobalHost.DependencyResolver.Register(typeof(IUserIdProvider), () => new IdBasedUserIdProvider());
        }
    }

    /// <summary>
    /// Hub initialized by the signalR server made for Dice Rolling Logic
    /// Used to update, fetch and manipulate/use dice data and send data to clients. 
    /// </summary>
    public class DiceRollerHub : Hub
    {
        private readonly Task initialization;

        public DiceRollerHub()
        {
            if (SignalRServer.dice == null)
                initialization = InitializeAsync();

            Console.WriteLine(@"Hub Initialized");
        }

        private void UpdateState(ELoadingState a_state)
        {
            Console.WriteLine(@"Server: " + a_state);
        }

        /// <summary>
        /// Initializes the hub by getting the Current Dicelist
        /// Not used if dicelist is already initialized
        /// </summary>
        /// <returns></returns>
        private async Task InitializeAsync()
        {
            var result = await Http.GetDiceListAsync(UpdateState);

            Console.WriteLine(@"Server retrieved dice list");

            SignalRServer.dice = Statics.GetDiceList(result);
        }

        /// <summary>
        /// Returns current dicelist to the client
        /// </summary>
        /// <returns>List of currently available dice</returns>
        public async Task<List<Dice>> GetDiceList()
        {
            //If first initialization wait till dice list is done updating
            if (initialization != null)
                await initialization;

            return SignalRServer.dice;
        }

        /// <summary>
        /// Rolls input dice and return + send output to clients.
        /// </summary>
        /// <param name="a_dice">Dice to roll</param>
        /// <returns></returns>
        public string DiceRoll(Dice a_dice)
        {
            if (a_dice == null)
                return string.Empty;

            var roll = a_dice.Roll();
            var output = string.Format("{0}: Rolled a {1} on a {2}",
                SignalRServer.users[Context.Headers["ID"]], roll.Side, roll.Name);

            Clients.Others.UpdateRoll(output);

            return output;
        }

        /// <summary>
        /// Verifies if caller is admin, if so kick specified user
        /// </summary>
        /// <param name="id">User to kick</param>
        /// <returns>True if kick succeeded</returns>
        public bool Kick(string id)
        {
            if (SignalRServer.AdminId == Context.Headers["ID"])
            {
                SignalRServer.users.Remove(id);

                Clients.AllExcept(id).clientDisconnect(id);

                Clients.User(id).Disconnect();

                return true;
            }

            return false;
        }

        /// <summary>
        /// Called when a user disconnects
        /// </summary>
        /// <param name="stopCalled">Not sure tbh</param>
        /// <returns></returns>
        public override Task OnDisconnected(bool stopCalled)
        {
            if (SignalRServer.users.ContainsKey(Context.Headers["ID"]))
            {
                Clients.Others.clientDisconnect(Context.Headers["ID"]);
                SignalRServer.users.Remove(Context.Headers["ID"]);
            }

            return base.OnDisconnected(stopCalled);
        }

        /// <summary>
        /// Called when a user connects
        /// Sends newly connected user to other users to update playerlists
        /// Also calls function on client to give client current playerlist
        /// </summary>
        /// <returns></returns>
        public override Task OnConnected()
        {
            Console.WriteLine(@"Server: ClientID {0} Connected", Context.Headers["ID"]);

            var name = Context.QueryString["name"];
            var id = Context.Headers["ID"];

            SignalRServer.users.Add(id, name);

            Clients.Others.UserConnected(id, name);
            Clients.Caller.UserList(SignalRServer.users);

            if (SignalRServer.AdminId == string.Empty)
                SignalRServer.AdminId = id;

            return base.OnConnected();
        }
    }
}