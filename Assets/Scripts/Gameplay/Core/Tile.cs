using Main.Gameplay.Piece;
using System.Collections.Generic;
using UnityEngine;

namespace Main.Gameplay
{
    public class Tile : MonoBehaviour
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        Board _board;

        public PieceBase Piece { get; private set; }

        Dictionary<DirectionType, Tile> neighbours = new Dictionary<DirectionType, Tile>();

        public void Init(int x, int y, Board board)
        {
            X = x;
            Y = y;
            _board = board;

            transform.localPosition = new Vector3(X, Y, 0f);

            gameObject.name = $"({X},{Y})";
        }

        public void SetPiece(PieceBase piece)
        {
            Piece = piece;
        }

        private void SetupNeighbours()
        {
            neighbours.Clear();

            if (X > 0)
                neighbours[DirectionType.Left] = _board.Tiles[X + 1, Y];

            if (X <= _board.Tiles.GetLength(0) - 1)
                neighbours[DirectionType.Right] = _board.Tiles[X - 1, Y];

            if (Y > 0)
                neighbours[DirectionType.Up] = _board.Tiles[X, Y + 1];

            if (Y <= _board.Tiles.GetLength(1) - 1)
                neighbours[DirectionType.Down] = _board.Tiles[X, Y - 1];
        }

        public void RecieveInputDirection(DirectionType direction)
        {

        }
    }
}