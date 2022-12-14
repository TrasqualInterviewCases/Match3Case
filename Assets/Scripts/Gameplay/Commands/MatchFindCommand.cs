using Main.Gameplay.Command;
using Main.Gameplay;
using System.Collections.Generic;
using System;

public class MatchFindCommand : ICommand
{
    List<Tile> _tiles;

    public MatchFindCommand(List<Tile> tiles)
    {
        _tiles = tiles;
        CommandManager.Instance.AddCommand(this);
    }

    public void Execute(Action OnCompleted)
    {
        for (int i = 0; i < _tiles.Count; i++)
        {
            _tiles[i].TryMatch();
        }
        OnCompleted();
    }
}
