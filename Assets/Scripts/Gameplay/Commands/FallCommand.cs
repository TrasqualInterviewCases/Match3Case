using System;
using System.Collections.Generic;

namespace Main.Gameplay.Command
{
    public class FallCommand : ICommand
    {
        Board _board;
        List<Tile> _tiles = new();

        public void Init(List<Tile> tiles, Board board)
        {
            _board = board;
            _tiles = tiles;
        }

        public void Execute(Action OnComplete)
        {
            for (int i = 0; i < _tiles.Count; i++)
            {
                for (int j = 0; j < _board.Rows - 1; j++)
                {
                    var curEmpty = _board.Tiles[_tiles[i].X, j];
                    if (curEmpty.Piece == null)
                    {
                        for (int k = j + 1; k < _board.Rows; k++)
                        {
                            var curFull = _board.Tiles[_tiles[i].X, k];
                            if (curFull.Piece != null)
                            {
                                curFull.MakePieceFall(curEmpty);
                                break;
                            }
                        }
                    }
                }
            }
            OnComplete();
        }
    }
}