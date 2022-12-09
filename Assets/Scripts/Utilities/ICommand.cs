using System;

public interface ICommand
{
    public void Execute(Action OnComplete);
    public void Rewind(Action OnComplete);
}
