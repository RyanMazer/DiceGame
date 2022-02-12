//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;



//namespace DiceGame.Source
//{
//    //public enum ELoadingState
//    //{
//    //    S_Empty,
//    //    S_Loading,
//    //    S_Loaded,
//    //    S_Uploading,
//    //    S_Failed
//    //}

//    public class Core
//    {
//        private List<Dice> diceList;
//        public List<Dice> DiceList { get { return diceList; } }

//        private Dice currentDice = null;
//        public Dice CurrentDice { get { return currentDice; } }

//        private ELoadingState loading = ELoadingState.S_Empty;
//        public ELoadingState ELoadingState { get { return loading; } }


//        private Action<string> updateRoll;
//        public Action<string> UpdateRoll { get { return updateRoll; } set { updateRoll = value; } }

//        private Action updateList;
//        public Action UpdateList { get { return updateList; } set { updateList = value; } }

//        private void LoadingState(ELoadingState state)
//        {
//            loading = state;
//            Console.WriteLine(loading.ToString());
//        }

//        public Core()
//        {
//            diceList = new List<Dice>();
//            //uploadQueue = new Dictionary<EUploadType, List<Dice>>();
//        }

//        public void selectDice(string diceName)
//        {

//        }

//        public async Task Intialize()
//        {

//        }

//        public void RollDice()
//        {
//            //TODO Add State Machine Update() 
//        }

//        public void saveDice(List<Dice> a_diceList)
//        {

//        }

//        public async void StartUploadAsync()
//        {

//        }

//        private async Task UploadAsync(List<Dice> a_Upload, EUploadType a_Type)
//        {

//        }
//    }
//}