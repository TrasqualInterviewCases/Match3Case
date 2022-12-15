using Main.Gameplay.StateMachineSystem;
using Main.Utilities;
using System.Collections.Generic;

namespace Main.Gameplay.CommandSystem
{
    public class CommandManager : Singleton<CommandManager>
    {
        private StateMachine stateMachine;

        private readonly Queue<ICommand> activeCommands = new Queue<ICommand>();

        protected override void Awake()
        {
            base.Awake();
            stateMachine = StateMachine.Instance;
        }

        public void AddCommand(ICommand command)
        {
            activeCommands.Enqueue(command);
        }

        public void ExecuteCommands()
        {
            if (activeCommands.Count <= 0)
            {
                stateMachine.ChangeState(stateMachine.TouchState);
                return;
            }
            var command = activeCommands.Dequeue();
            command.Execute(ExecuteCommands);
        }
    }
}
