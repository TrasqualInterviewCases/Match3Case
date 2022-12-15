
namespace Main.Gameplay.StateMachineSystem
{
    public abstract class StateBase
    {
        protected StateMachine _stateMachine;

        public StateBase(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public abstract void EnterState();

        public abstract void ExitState();
    }
}