using Main.Gameplay.Enums;
using System.Collections.Generic;
using System.Linq;

namespace Main.Gameplay
{
    public static class MatchFinder
    {
        public static bool FindMatches(Tile tile, out List<Tile> tiles, int minMatches = 3)
        {
            tiles = new();
            tiles.Add(tile);

            if (FindHorizontalMatches(tile, out List<Tile> horizontalMatches, minMatches))
            {
                tiles = tiles.Union(horizontalMatches).ToList();
            }
            if (FindVerticalMatches(tile, out List<Tile> verticalMatches, minMatches))
            {
                tiles = tiles.Union(verticalMatches).ToList();
            }
            if (tiles.Count >= minMatches)
            {
                return true;
            }
            return false;
        }

        private static bool FindHorizontalMatches(Tile tile, out List<Tile> tiles, int minMatches = 3)
        {
            tiles = new();
            tiles.Add(tile);

            if (FindMatchInDirection(tile, DirectionType.Left, out List<Tile> leftMatches, 2))
            {
                tiles = tiles.Union(leftMatches).ToList();
            }
            if (FindMatchInDirection(tile, DirectionType.Right, out List<Tile> rightMatches, 2))
            {
                tiles = tiles.Union(rightMatches).ToList();
            }

            if (tiles.Count >= minMatches)
            {
                return true;
            }
            return false;
        }

        private static bool FindVerticalMatches(Tile tile, out List<Tile> tiles, int minMatches = 3)
        {
            tiles = new();
            tiles.Add(tile);

            if (FindMatchInDirection(tile, DirectionType.Up, out List<Tile> upMatches, 2))
            {
                tiles = tiles.Union(upMatches).ToList();
            }
            if (FindMatchInDirection(tile, DirectionType.Down, out List<Tile> downMatches, 2))
            {
                tiles = tiles.Union(downMatches).ToList();
            }

            if (tiles.Count >= minMatches)
            {
                return true;
            }
            return false;
        }

        public static bool FindMatchInDirection(Tile tile, DirectionType direction, out List<Tile> tiles, int minMatches = 3)
        {
            tiles = new();
            tiles.Add(tile);

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

        public static bool FindMatchInDirection(Tile tile, DirectionType direction, int minMatches = 3)
        {
            var foundMatches = 1;
            while (GetMatchingNeighbour(tile, direction, out Tile foundTile))
            {
                foundMatches++;
                tile = foundTile;
            }
            if (foundMatches >= minMatches)
            {
                return true;
            }
            return false;
        }

        private static bool GetMatchingNeighbour(Tile tile, DirectionType direction, out Tile matchingTile)
        {
            if (tile.Neighbours.ContainsKey(direction))
            {
                if (tile.Neighbours[direction].Piece.PieceType == tile.Piece.PieceType)
                {
                    matchingTile = tile.Neighbours[direction];
                    return true;
                }
            }
            matchingTile = null;
            return false;
        }
    }
}