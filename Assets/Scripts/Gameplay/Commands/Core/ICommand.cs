using System;

namespace Main.Gameplay.CommandSystem
{
    public interface ICommand
    {
        public void Execute(Action OnCompleted);
    }
}