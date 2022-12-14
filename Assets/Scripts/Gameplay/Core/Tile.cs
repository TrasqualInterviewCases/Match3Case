using Main.Gameplay.Command;
using Main.Gameplay.Enums;
using Main.Gameplay.Piece;
using Main.Gameplay.StateMachineSystem;
using System.Collections.Generic;
using UnityEngine;

namespace Main.Gameplay
{
    public class Tile : MonoBehaviour
    {
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

        public void RecievePiece(PieceBase piece)
        {
            SetPiece(piece);
            if (GetNeighbourInDirection(DirectionType.Down, out var lowerNeighbour) && lowerNeighbour.Piece == null)
            {
                DoFall();
            }
            else
            {
                if (TryMatch())
                {
                    StateMachine.Instance.ChangeState(StateMachine.Instance.AnimationState);
                }
            }
        }

        public bool TryMatch()
        {
            if (MatchFinder.FindMatches(this, out List<Tile> foundMatches))
            {
                foundMatches.Add(this);

                new PiecePopCommand(foundMatches);
                new FallCommand(foundMatches);

                return true;
            }
            else
            {
                return false;
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
            Piece.Pop();
            Piece = null;
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

        public void RequestPiece()
        {
            if (GetNeighbourInDirection(DirectionType.Up, out var upperNeighbour))
            {
                upperNeighbour.DoFall();
            }
        }

        public void DoFall()
        {
            if (Piece != null)
            {
                Piece.FallTo(Neighbours[DirectionType.Down]);
                Piece = null;
            }
            RequestPiece();
        }

        public bool CanSwap(DirectionType direction)
        {
            return Piece != null && GetNeighbourInDirection(direction, out var neighbour) && neighbour.Piece != null;
        }
    }
}