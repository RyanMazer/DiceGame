using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.Owin.Hosting;
using Microsoft.Owin.Cors;
using Owin;
using System.Threading.Tasks;


namespace DiceGame.Source
{
    public static class SingalRServer
    {
        private static IDisposable server;

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
            hubconfig.EnableDetailedErrors = true;
            app.MapSignalR(hubconfig);
        }
    }

    public class DiceRollerHub : Hub
    {
        private string AdminId = ""; 
        private List<Dice> dice = new List<Dice>();
        ELoadingState eLoadingState = ELoadingState.S_Empty;

        private Task initialization; 

        public DiceRollerHub()
        {
            initialization = InitializeAsync(); 
            Console.WriteLine("Hub Initialized");
        }

        private void UpdateState(ELoadingState a_state)
        {
            eLoadingState = a_state;
        }

        private async Task InitializeAsync()
        {
            var result = await HTTP.getDiceListAsync(UpdateState);

            Console.WriteLine("Server retreived dicelist");

            dice = Statics.GetDiceList(result);
        }

        public void Send(string name, string message)
        {
            Clients.All.addMessage(name, message);
        }

        public override Task OnConnected()
        {
            Console.WriteLine("Connected");
            
            if(AdminId == string.Empty)
                AdminId = Context.ConnectionId;

            return base.OnConnected();
        }
    }

    public static class SignalRConnector
    {
        public static async Task Connect(string url)
        {
            var connection = new HubConnection(url);

            connection.TraceLevel = TraceLevels.All;
            connection.TraceWriter = Console.Out;

            var hub = connection.CreateHubProxy("DiceRollerHub");

            await connection.Start();

            Console.WriteLine(connection.State);
        }
    }
}
