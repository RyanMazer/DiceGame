//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace DiceGame.Source
//{
//    public class StateMachine
//    {
//        private BaseState CurrentState;

//        void Initialize()
//        {
//            CurrentState = GetInitialState();
//            if (CurrentState != null)
//                CurrentState.Enter(); 
//        }

//        void Update()
//        {
//            if (CurrentState != null)
//                CurrentState.Update();
//        }

//        public void ChangeState(BaseState a_state)
//        {
//            CurrentState.Exit();

//            CurrentState = a_state;
//            CurrentState.Enter(); 
//        }

//        protected virtual BaseState GetInitialState()
//        {
//            return null; 
//        }
//    }

//    public abstract class BaseState
//    {
//        public string Name { get; }
//        protected StateMachine stateMachine { get; }

//        public BaseState(string a_name, StateMachine a_stateMachine)
//        {
//            Name = a_name;
//            stateMachine = a_stateMachine;
//        }

//        public abstract void Enter();
//        public abstract void Update();
//        public abstract void Exit();
//    }
//}
