using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiceGame.Source.Base;
using Microsoft.AspNet.SignalR.Client;


namespace DiceGame.Source.Logic
{
    public class DiceLogicRemote : DiceLogicBase
    {
        private HubConnection connection;
        private IHubProxy hubProxy;

        private Dictionary<string, string> users = new Dictionary<string, string>();
        public Dictionary<string, string> Users { get { return users; } }

        string name; 
        public string Name { get { return name; } set { name = value; } }

        Action<string> updateDice;
        public Action<string> UpdateDice { set { updateDice = value; } }

        Action updateUsers;
        public Action UpdateUsers { set { updateUsers = value; } }

        public override void CloseLogic()
        {
            
        }

        public async override Task<string> DiceRoll()
        {
            if (diceList != null && diceList.Count > 0)
            {
                return await hubProxy.Invoke<string>("DiceRoll", currentDice); 
            }

            return string.Empty;
        }

        public override async Task InitializeAsync()
        {
            await ConnectAsync("http://82.73.229.223:8080");
            diceList = await hubProxy.Invoke<List<Dice>>("GetDiceList");
        }

        public async Task ConnectAsync(string url)
        {
            var query = new Dictionary<string, string>();
            query.Add("name", name);
            
            connection = new HubConnection(url, query);

            connection.TraceLevel = TraceLevels.All;
            connection.TraceWriter = Console.Out;

            hubProxy = connection.CreateHubProxy("DiceRollerHub");

            hubProxy.On<string>("UpdateRoll", output =>
            { updateDice(output); } );

            hubProxy.On<string, string>("userConnected", (id, user) =>
            { users.Add(id, user); updateUsers(); } );

            hubProxy.On<Dictionary<string, string>>("UserList", userList =>
             { users = userList; updateUsers(); } );

            await connection.Start();

            Console.WriteLine(@"Client: " + connection.State);
        }

        public void Disconnect()
        {
            connection.Stop();
        }
    }
}
