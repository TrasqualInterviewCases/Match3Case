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
            for (int j = 0; j < _board.Rows - 1; j++)
            {
                var emptyTile = _board.Tiles[columns[i], j];
                if (emptyTile.Piece == null)
                {
                    for (int k = j + 1; k < _board.Rows; k++)
                    {
                        var filledTile = _board.Tiles[columns[i], k];
                        if (filledTile.Piece != null)
                        {
                            filledTile.DoFall(emptyTile);
                            break;
                        }
                    }
                }
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
