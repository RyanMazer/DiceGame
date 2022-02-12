using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame.Source.States
{
    //public class GetDice : BaseState
    //{
    //    Core core; 
    //    List<DiceJson> diceJson;

    //    public GetDice(Core a_Core, StateMachine state) : base("GetDice", state)
    //    { 
    //        core = a_Core;
    //    }

    //    public async override void Enter()
    //    {
    //        diceJson = await HTTP.getDiceListAsync();
    //        stateMachine.ChangeState(new DiceRolling(core, stateMachine)); 
    //    }

    //    public override void Update()
    //    {
            
    //    }

    //    public override void Exit()
    //    {
    //        core.saveDice(Statics.GetDiceList(diceJson)); 
    //    }
    //}
}
