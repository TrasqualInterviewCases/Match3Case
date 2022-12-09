using Main.Gameplay.Enums;
using System.Collections.Generic;

namespace Main.Gameplay
{
    public static class MatchFinder
    {
        public static void FindMatches(Tile tile, PieceType pieceType, Board board)
        {

        }

        public static bool FindMatchInDirection(Tile tile, DirectionType direction, out List<Tile> tiles, int minMatches = 3)
        {
            tiles = new();

            while (GetMatchingNeighbour(tile, direction, out Tile foundTile))
            {
                tiles.Add(foundTile);
                tile = foundTile;
            }
            if (tiles.Count >= minMatches)
            {
                return true;
            }
            return false;
        }

        private static bool GetMatchingNeighbour(Tile tile, DirectionType direction, out Tile matchingTile)
        {
            if (tile.Neighbours.ContainsKey(direction))
            {
                matchingTile = tile.Neighbours[direction];
                return true;
            }
            matchingTile = null;
            return false;
        }
    }
}