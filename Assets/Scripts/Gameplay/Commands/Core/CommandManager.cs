using Main.Gameplay.StateMachineSystem;
using System.Collections.Generic;

namespace Main.Gameplay.Command
{
    public class CommandManager : Singleton<CommandManager>
    {
        Queue<ICommand> activeCommands = new Queue<ICommand>();

        public void AddCommand(ICommand command)
        {
            activeCommands.Enqueue(command);
        }

        public void ExecuteCommands()
        {
            if (activeCommands.Count <= 0)
            {
                StateMachine.Instance.ChangeState(StateMachine.Instance.TouchState);
                return;
            }
            var command = activeCommands.Dequeue();
            command.Execute(ExecuteCommands);
        }
    }
}
