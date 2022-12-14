using Main.Gameplay;
using Main.Gameplay.Command;
using System;
using System.Collections.Generic;

public class FallCommand : ICommand
{
    List<Tile> _tiles;

    public FallCommand(List<Tile> tiles)
    {
        _tiles = tiles;
        CommandManager.Instance.AddCommand(this);
    }

    public void Execute(Action OnCompleted)
    {

    }
}
