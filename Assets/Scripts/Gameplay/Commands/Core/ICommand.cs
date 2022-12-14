using System;

namespace Main.Gameplay.Command
{
    public interface ICommand
    {
        public void Execute(Action OnCompleted);
    }
}