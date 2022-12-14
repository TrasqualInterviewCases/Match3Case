using Main.Gameplay.Command;
using UnityEngine;

namespace Main.Gameplay.StateMachineSystem
{
    public class AnimationState : StateBase
    {
        public AnimationState(StateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void EnterState()
        {
            CommandManager.Instance.ExecuteCommands();
        }

        public override void ExitState()
        {
        }
    }
}