using Main.Gameplay.CommandSystem;
using Main.Gameplay.Core;
using System.Collections.Generic;
using UnityEngine;

namespace Main.Gameplay.Pieces.Strategies
{
    [CreateAssetMenu(menuName = "Popping Strategies/RowPop")]
    public class RowPop : PoppingStrategy
    {
        public override void DoOnPop(Tile tile)
        {
            var tilesInRow = new List<Tile>();
            for (int i = 0; i < tile.Board.Columns; i++)
            {
                tilesInRow.Add(tile.Board.Tiles[i, tile.Y]);
            }
            new PiecePopCommand(tilesInRow);
            new FallCommand(tilesInRow);
        }
    }
}