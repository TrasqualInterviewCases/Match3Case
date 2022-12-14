using Main.Gameplay.Enums;
using Main.Gameplay.Piece;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Main.Gameplay
{
    public class Tile : MonoBehaviour
    {
        public event Action<Tile> OnMatchFound;
        public event Action<Tile> OnNoMatchFound;

        [SerializeField] bool isSpawner;

        public int X { get; private set; }
        public int Y { get; private set; }

        Board _board;

        public PieceBase Piece { get; private set; }

        public Dictionary<DirectionType, Tile> Neighbours { get; private set; } = new Dictionary<DirectionType, Tile>();

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

        public void RecievePiece(PieceBase newPiece)
        {
            SetPiece(newPiece);
            if (MatchFinder.FindMatches(this, out List<Tile> foundMatches))
            {
                OnMatchFound?.Invoke(this);
                foundMatches.Add(this);
                for (int i = 0; i < foundMatches.Count; i++)
                {
                    foundMatches[i].PopPiece();
                }
            }
            else
            {
                OnNoMatchFound?.Invoke(this);
            }
        }

        public void SetupNeighbours()
        {
            Neighbours.Clear();

            if (X >= 0 && X < _board.Columns - 1)
                Neighbours[DirectionType.Right] = _board.Tiles[X + 1, Y];

            if (X > 0 && X <= _board.Columns - 1)
                Neighbours[DirectionType.Left] = _board.Tiles[X - 1, Y];

            if (Y >= 0 && Y < _board.Rows - 1)
                Neighbours[DirectionType.Up] = _board.Tiles[X, Y + 1];

            if (Y > 0 && Y <= _board.Rows - 1)
                Neighbours[DirectionType.Down] = _board.Tiles[X, Y - 1];
        }

        public void PopPiece()
        {
            ObjectPoolManager.Instance.ReleaseObject(Piece);
            EmptyTile();
        }

        private void EmptyTile()
        {
            Piece = null;
            if (GetNeighbourInDirection(DirectionType.Up, out var neighbour))
            {
                if (neighbour.Piece != null)
                    neighbour.DoFall();
            }
        }

        public bool GetNeighbourInDirection(DirectionType direction, out Tile neighbour)
        {
            if (Neighbours.ContainsKey(direction))
            {
                neighbour = Neighbours[direction];
                return true;
            }
            neighbour = null;
            return false;
        }

        public void DoFall()
        {
            Piece.FallTo(Neighbours[DirectionType.Down]);
            EmptyTile();
        }
    }
}