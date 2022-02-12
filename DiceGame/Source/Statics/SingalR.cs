using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Hosting;
using Microsoft.Owin.Cors;
using Owin;
using System.Threading.Tasks;


namespace DiceGame.Source
{

    public interface IServerData
    {

    }

    public static class SignalRServer
    {
        private static IDisposable server;

        public static Dictionary<string, string> users = new Dictionary<string, string>();
        public static string AdminId = "";
        public static List<Dice> dice;

        public static async Task InitializeServer()
        {
            string url = @"http://+:8080";
            server = WebApp.Start<Startup>(url);

            Console.WriteLine("Server running on: {0}", url);
        }

        public static async Task ShutdownServer()
        {
            server.Dispose();
        }
    }

    class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);
            var hubconfig = new HubConfiguration();
            hubconfig.EnableJSONP = true;
            hubconfig.EnableDetailedErrors = true;
            app.MapSignalR(hubconfig);
        }
    }

    public class DiceRollerHub : Hub
    {
        private Task initialization;

        public DiceRollerHub()
        {
            if(SignalRServer.dice == null)
                initialization = InitializeAsync(); 
            
            Console.WriteLine(@"Hub Initialized");
        }

        private void UpdateState(ELoadingState a_state)
        {
            Console.WriteLine(@"Server: " + a_state);
        }

        private async Task InitializeAsync()
        {
            var result = await HTTP.getDiceListAsync(UpdateState);

            Console.WriteLine(@"Server retrieved dice list");

            SignalRServer.dice = Statics.GetDiceList(result);
        }

        public async Task<List<Dice>> GetDiceList()
        {
            if(initialization != null)
                await initialization;
            
            return SignalRServer.dice;
        }

        public string DiceRoll(Dice a_dice)
        {
            if (a_dice == null)
                return string.Empty;

            rollResult roll = a_dice.roll();
            string output = string.Format("{0}: Rolled a {1} on a {2}", 
                SignalRServer.users[Context.ConnectionId],roll.side, roll.name);

            Clients.Others.UpdateRoll(output); 

            return output;
        }

        public override Task OnConnected()
        {
            Console.WriteLine(@"Server: ClientID {0} Connected", Context.ConnectionId);

            string name = Context.QueryString["name"];
            string id = Context.ConnectionId;

            SignalRServer.users.Add(id, name); 

            Clients.Others.UserConnected(id, name);
            Clients.Caller.UserList(SignalRServer.users); 

            if(SignalRServer.AdminId == string.Empty)
                SignalRServer.AdminId = Context.ConnectionId;

            return base.OnConnected();
        }
    }
}
