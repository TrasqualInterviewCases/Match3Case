using Main.Gameplay.TouchControls;

namespace Main.Gameplay.StateMachineSystem
{
    public class TouchState : StateBase
    {
        private readonly TouchControl input;

        public TouchState(StateMachine stateMachine) : base(stateMachine)
        {
            input = stateMachine.Input;
        }

        public override void EnterState()
        {
            input.enabled = true;
        }

        public override void ExitState()
        {
            input.enabled = false;
        }
    }
}