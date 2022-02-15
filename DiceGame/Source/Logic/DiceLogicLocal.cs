using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DiceGame.Source.Base;

namespace DiceGame.Source.Logic
{
    /// <summary>
    /// Local Dice Logic class for locally updating, fetching and using dice data 
    /// </summary>
    public class DiceLogicLocal : DiceLogicBase
    {
        private readonly Dictionary<EUploadType, List<Dice>> uploadQueue;

        public DiceLogicLocal()
        {
            uploadQueue = new Dictionary<EUploadType, List<Dice>>();
        }

        public override void CloseLogic()
        {
            //TODO Not sure what yet but keep it in mind
        }

        /// <summary>
        /// Called to roll currently selected dice
        /// </summary>
        /// <returns></returns>
        public override Task<string> DiceRoll()
        {
            var output = "";

            if (currentDice != null)
            {
                var result = currentDice.Roll();
                output = "You rolled " + result.Side + " on a" + result.Name;
            }
            else
            {
                MessageBox.Show(@"Select a dice to roll", @"Error", MessageBoxButtons.OK);
            }

            return Task.FromResult(output);
        }

        /// <summary>
        /// Initialize Dicelist using the Get Dice List Api
        /// </summary>
        /// <returns></returns>
        public override async Task InitializeAsync()
        {
            var diceJson = await Http.GetDiceListAsync(UpdateState);
            diceList = Statics.GetDiceList(diceJson);
        }

        /// <summary>
        /// Save new dicelist locally and initializes update and delete list for when the user decided to upload the new list. 
        /// </summary>
        /// <param name="aDice"></param>
        public void SaveDice(List<Dice> aDice)
        {
            if (diceList == null)
            {
                diceList = aDice;
                return;
            }

            if (aDice == diceList) return;

            var deleted = diceList.Except(aDice).ToList();
            var update = aDice.Except(diceList, new DiceComparer()).ToList();

            //TODO Handle multiple saves before upload. 

            if (deleted.Count > 0)
                uploadQueue.Add(EUploadType.T_Delete, deleted);
            if (update.Count > 0)
                uploadQueue.Add(EUploadType.T_Update, update);

            diceList = aDice;
        }
        
        /// <summary>
        /// Initializes the upload of locally saved dicelist changes
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public async Task StartUploadAsync()
        {
            foreach (var upload in uploadQueue)
                switch (upload.Key)
                {
                    case EUploadType.T_Delete:
                    {
                        await Http.DeleteDiceAsync(upload.Value);
                    }
                        break;
                    case EUploadType.T_Update:
                    {
                        await Http.UpdateDiceListAsync(upload.Value);
                    }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
        }
    }
}