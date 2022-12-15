using Main.Gameplay.CommandSystem;
using Main.Gameplay.Core;
using System.Linq;
using UnityEngine;

namespace Main.Gameplay.Piece.Strategies
{
    [CreateAssetMenu(menuName = "Popping Strategies/NeighbourBomb")]
    public class NeighbourBombPoppingStrategy : PoppingStrategy
    {
        public override void DoOnPop(Tile tile)
        {
            var neighbours = tile.Neighbours.Values.ToList();
            new PiecePopCommand(neighbours);
            new FallCommand(neighbours);
        }
    } 
}
