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

        [Header("Debug Neighbours")]
        [SerializeField] Tile neighbourUp;
        [SerializeField] Tile neighbourDown;
        [SerializeField] Tile neighbourLeft;
        [SerializeField] Tile neighbourRight;

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
            Piece.SetOwnerTile(this);
        }

        public void SetupNeighbours()
        {
            neighbours.Clear();

            if (X >= 0 && X < _board.Columns - 1)
                neighbourRight = neighbours[DirectionType.Right] = _board.Tiles[X + 1, Y];

            if (X > 0 && X <= _board.Columns - 1)
                neighbourLeft = neighbours[DirectionType.Left] = _board.Tiles[X - 1, Y];

            if (Y >= 0 && Y < _board.Rows - 1)
                neighbourUp = neighbours[DirectionType.Up] = _board.Tiles[X, Y + 1];

            if (Y > 0 && Y <= _board.Rows - 1)
                neighbourDown = neighbours[DirectionType.Down] = _board.Tiles[X, Y - 1];
        }

        public void RecieveInputDirection(DirectionType direction)
        {
            if (!neighbours.ContainsKey(direction)) return;

            //TODO Check Matches at Neighbours Position and This Tile Position of swapped PieceTypes
            var neighbour = neighbours[direction];
            var neighbourPiece = neighbour.Piece;
            neighbours[direction].SetPiece(Piece);
            SetPiece(neighbourPiece);
        }
    }
}