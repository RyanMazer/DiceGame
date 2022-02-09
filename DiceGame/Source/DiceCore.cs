using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DiceGame.Forms; 


namespace DiceGame.Source
{
    public enum ELoadingState
    {
        S_Empty,
        S_Loading,
        S_Loaded,
        S_Uploading,
        S_Failed
    }

    public enum EUploadType
    {
        T_Delete, 
        T_Update
    }

    public class Core
    {
        private List<Dice> diceList;
        public List<Dice> DiceList { get { return diceList; } }

        private Dice currentDice = null;
        public Dice CurrentDice { get { return currentDice; } }

        private ELoadingState loading = ELoadingState.S_Empty;
        public ELoadingState ELoadingState { get { return loading; } }

        private Dictionary<EUploadType, List<Dice>> uploadQueue; 

        private void LoadingState(ELoadingState state)
        {
            loading = state;
            Console.WriteLine(loading.ToString());
        }

        public Core()
        {
            diceList = new List<Dice>();
            uploadQueue = new Dictionary<EUploadType,List<Dice>>();
        }

        public void selectDice(string diceName)
        {
            if (diceName != null)
            {
                foreach (Dice d in diceList)
                    if (d.getName() == diceName)
                    {
                        currentDice = d;
                    }
            }
            else
                MessageBox.Show("Something went wrong. You shouldnt be able to select an empty dice! Contact Developer plz!", "Error", MessageBoxButtons.OK);
        }

        public async Task Intialize()
        {
            var ReqResult = await HTTP.getDiceListAsync(LoadingState);

            foreach (var dice in ReqResult)
            {
                string[] faces = dice.diceFace.Split(',');

                Dice newDice = new Dice(faces, dice.diceName);
                diceList.Add(newDice);
            }

            //await HTTP.UpdateDiceListAsync(DiceList);
        }

        public string RollDice()
        {
            string output = "";

            if (currentDice != null)
            {
                rollResult result = currentDice.roll();
                output = "You rolled " + result.side + " on a" + result.name;
            }
            else
                MessageBox.Show("Select a dice to roll", "Error", MessageBoxButtons.OK);

            return output;
        }

        public void saveDice(List<Dice> a_diceList)
        {
            List<Dice> update;
            List<Dice> deleted;

            if (a_diceList != null)
                if(a_diceList != diceList)
                {
                    deleted = diceList.Except(a_diceList).ToList();
                    update = a_diceList.Except(diceList, new DiceComparer()).ToList();
                    
                    //TODO Handle multiple saves before upload. 

                    if(deleted != null && deleted.Count > 0)
                        uploadQueue.Add(EUploadType.T_Delete, deleted);
                    if(update != null && update.Count > 0)
                        uploadQueue.Add(EUploadType.T_Update, update);

                    diceList = a_diceList;
                }
        }

        public async void StartUploadAsync()
        {
            foreach (var upload in uploadQueue)
                await UploadAsync(upload.Value, upload.Key); 
        }

        private async Task UploadAsync(List<Dice> a_Upload, EUploadType a_Type)
        {
            switch (a_Type)
            {
                case EUploadType.T_Delete:
                    {
                        await HTTP.DeleteDiceAsync(a_Upload); 
                    }
                    break;
                case EUploadType.T_Update:
                    {
                        await HTTP.UpdateDiceListAsync(a_Upload);
                    }
                    break;
            }
        }
    }
}