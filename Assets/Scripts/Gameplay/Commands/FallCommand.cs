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
            Tile fallTarget;
            for (int i = 0; i < _tiles.Count; i++)
            {
                fallTarget = _tiles[i];
                for (int j = _tiles[i].Y + 1; j < _board.Rows; j++)
                {
                    var curTile = _board.Tiles[_tiles[i].X, j];
                    if (curTile.Piece != null)
                    {
                        curTile.Piece.FallTo(fallTarget);
                        fallTarget = _board.Tiles[fallTarget.X, fallTarget.Y + 1];
                    }
                }
            }
            OnComplete();
        }
    }
}