using Main.Gameplay;
using Main.Gameplay.Command;
using System;
using System.Collections.Generic;

public class FallCommand : ICommand
{
    List<Tile> _tiles;
    Board _board;
    public FallCommand(List<Tile> tiles, Board board)
    {
        _tiles = tiles;
        _board = board;
        CommandManager.Instance.AddCommand(this);
    }

    public void Execute(Action OnCompleted)
    {
        var columns = GetColumns();
        for (int i = 0; i < columns.Count; i++)
        {
            for (int j = 0; j < _board.Rows; j++)
            {
                _board.Tiles[columns[i], j].DoFall();
            }
        }
        OnCompleted();
    }

    private List<int> GetColumns()
    {
        var colums = new List<int>();
        for (int i = 0; i < _tiles.Count; i++)
        {
            if (!colums.Contains(_tiles[i].X))
            {
                colums.Add(_tiles[i].X);
            }
        }
        return colums;
    }
}
