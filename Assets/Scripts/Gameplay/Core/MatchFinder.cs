using Main.Gameplay.Enums;
using System.Collections.Generic;
using System.Linq;

namespace Main.Gameplay
{
    public static class MatchFinder
    {
        private static List<Tile> processedTiles = new List<Tile>();

        private static int indexPointer;

        private static void AddMember(Tile tile)
        {
            if (!processedTiles.Contains(tile))
            {
                processedTiles.Add(tile);
                indexPointer++;
            }
        }

        private static void ClearBuffer()
        {
            processedTiles.Clear();
            indexPointer = 0;
        }

        public static bool FindMatches(this Tile tile, out List<Tile> tiles, int minMatches = 2)
        {
            ClearBuffer();

            tile.FindHorizontalMatches(minMatches);

            tile.FindVerticalMatches(minMatches);

            tiles = new List<Tile>(processedTiles);

            return tiles.Count >= minMatches;
        }

        private static void FindHorizontalMatches(this Tile tile, int minMatches = 2)
        {
            var foundMatches = 0;
            foundMatches += tile.FindMatchInDirection(DirectionType.Left, 1);

            foundMatches += tile.FindMatchInDirection(DirectionType.Right, 1);

            if (foundMatches > 0 && foundMatches < minMatches)
            {
                processedTiles.RemoveRange(indexPointer - 1, foundMatches);
                indexPointer -= foundMatches;
            }
        }

        private static void FindVerticalMatches(this Tile tile, int minMatches = 2)
        {
            var foundMatches = 0;
            foundMatches += tile.FindMatchInDirection(DirectionType.Up, 1);

            foundMatches += tile.FindMatchInDirection(DirectionType.Down, 1);

            if (foundMatches > 0 && foundMatches < minMatches)
            {
                processedTiles.RemoveRange(indexPointer - 1, foundMatches);
                indexPointer -= foundMatches;
            }
        }

        private static int FindMatchInDirection(this Tile tile, DirectionType direction, int minMatches = 2)
        {
            var counter = 0;
            while (tile.GetMatchingNeighbour(direction, out Tile foundTile))
            {
                AddMember(foundTile);
                tile = foundTile;
                counter++;
            }
            if (counter > 0 && counter < minMatches)
            {
                processedTiles.RemoveRange(indexPointer - 1, counter);
                indexPointer -= counter;
                return 0;
            }
            return counter;
        }

        public static bool HasMatchInDirection(this Tile tile, DirectionType direction, int minMatches = 3)
        {
            var foundMatches = 1;
            while (tile.GetMatchingNeighbour(direction, out Tile foundTile))
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

        private static bool GetMatchingNeighbour(this Tile tile, DirectionType direction, out Tile matchingTile)
        {
            if (tile.Neighbours.ContainsKey(direction))
            {
                if (tile.Neighbours[direction].Piece != null && tile.Neighbours[direction].Piece.PieceType == tile.Piece.PieceType)
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