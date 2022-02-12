using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiceGame.Source.Base;
using System.Windows.Forms; 

namespace DiceGame.Source.Logic
{
    public class DiceLogicLocal : DiceLogicBase
    {
        private Dictionary<EUploadType, List<Dice>> uploadQueue;

        public DiceLogicLocal() : base()
        {
            uploadQueue = new Dictionary<EUploadType,List<Dice>>();
        }

        public override void CloseLogic()
        {
            //TODO Not sure what yet but keep it in mind
        }

        public override Task<string> DiceRoll()
        {
            string output = "";

            if (currentDice != null)
            {
                rollResult result = currentDice.roll();
                output = "You rolled " + result.side + " on a" + result.name;
            }
            else
                MessageBox.Show("Select a dice to roll", "Error", MessageBoxButtons.OK);

            return Task<string>.FromResult(output);
        }

        public async override Task InitializeAsync()
        {
            var diceJson = await HTTP.getDiceListAsync(UpdateState);
            diceList = Statics.GetDiceList(diceJson);
        }

        public void SaveDice(List<Dice> a_dice)
        {
            if (diceList == null)
            {
                diceList = a_dice;
                return;
            }

            List<Dice> update;
            List<Dice> deleted;

            if (a_dice != diceList)
            {
                deleted = diceList.Except(a_dice).ToList();
                update = a_dice.Except(diceList, new DiceComparer()).ToList();

                //TODO Handle multiple saves before upload. 

                if (deleted != null && deleted.Count > 0)
                    uploadQueue.Add(EUploadType.T_Delete, deleted);
                if (update != null && update.Count > 0)
                    uploadQueue.Add(EUploadType.T_Update, update);

                diceList = a_dice;
            }
        }

        public async Task StartUploadAsync()
        {
            foreach (var upload in uploadQueue)
            {
                switch (upload.Key)
                {
                    case EUploadType.T_Delete:
                        {
                            await HTTP.DeleteDiceAsync(upload.Value);
                        }
                        break;
                    case EUploadType.T_Update:
                        {
                            await HTTP.UpdateDiceListAsync(upload.Value);
                        }
                        break;
                }
            }
        }
    }
}
