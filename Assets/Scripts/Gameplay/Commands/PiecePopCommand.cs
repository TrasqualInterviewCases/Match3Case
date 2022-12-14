using Main.Gameplay.Core;
using System;
using System.Collections.Generic;

namespace Main.Gameplay.CommandSystem
{
    public class PiecePopCommand : ICommand
    {
        private readonly List<Tile> _tiles;

        public PiecePopCommand(List<Tile> tiles)
        {
            _tiles = tiles;
            CommandManager.Instance.AddCommand(this);
        }

        public void Execute(Action OnComplete)
        {
            for (int i = 0; i < _tiles.Count; i++)
            {
                _tiles[i].PopPiece();
            }
            OnComplete();
        }
    }
}