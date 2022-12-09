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
            Debug.Log("did animation");
            _stateMachine.ChangeState(_stateMachine.TouchState);
        }

        public override void ExitState()
        {
        }
    }
}