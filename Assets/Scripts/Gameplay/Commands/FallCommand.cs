using System;
using System.Collections.Generic;

namespace Main.Gameplay.Command
{
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
            foreach (var tile in _tiles)
            {
                tile.RequestPiece();
            }
            OnCompleted();
        }
    }
}