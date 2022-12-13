using Main.Gameplay.Enums;
using Main.Gameplay.Piece;
using Main.Gameplay.StateMachineSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Main.Gameplay
{
    public class Tile : MonoBehaviour
    {
        public event Action<Tile> OnTileEmptied;
        public event Action<Tile> OnTileFilled;

        public int X { get; private set; }
        public int Y { get; private set; }

        Board _board;

        public PieceBase Piece { get; private set; }

        public Dictionary<DirectionType, Tile> Neighbours { get; private set; } = new Dictionary<DirectionType, Tile>();

        public Tile fallTarget;

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
            OnTileFilled?.Invoke(this);
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
                fallTarget = Neighbours[DirectionType.Down] = _board.Tiles[X, Y - 1];
        }

        public void RecieveInputDirection(DirectionType direction)
        {
            if (!Neighbours.ContainsKey(direction) || Neighbours[direction].Piece == null || Piece == null)
            {
                //Do Shake Animation
                StateMachine.Instance.ChangeState(StateMachine.Instance.TouchState);
                return;
            }

            ProcessInput(direction);
        }

        private void ProcessInput(DirectionType direction)
        {
            var neighbour = Neighbours[direction];

            var neighbourPiece = neighbour.Piece;
            var currentPiece = Piece;

            var pieceSwapper = ObjectPoolManager.Instance.GetObject<SwapPieceCommand>();
            pieceSwapper.Init(currentPiece, neighbourPiece, 5f, () =>
            {
                neighbour.SetPiece(currentPiece);
                SetPiece(neighbourPiece);

                var neighbourMatchCheck = neighbour.FindMatches(out List<Tile> neighbourMatches);
                if (neighbourMatchCheck)
                {
                    neighbourMatches.Add(neighbour);
                }
                else
                {
                    neighbourMatches.Clear();
                }

                var currentMatchCheck = this.FindMatches(out List<Tile> currentMatches);
                if (currentMatchCheck)
                {
                    currentMatches.Add(this);
                }
                else
                {
                    currentMatches.Clear();
                }


                if (neighbourMatchCheck || currentMatchCheck)
                {
                    var combinedMatches = neighbourMatches.Union(currentMatches).ToList();

                    //process matches;
                    for (int i = 0; i < combinedMatches.Count; i++)
                    {
                        combinedMatches[i].PopPiece();
                    }
                    //DO Falls and Fills And then Do StateChange
                    StateMachine.Instance.ChangeState(StateMachine.Instance.TouchState);
                }
                else
                {
                    pieceSwapper.Rewind(() =>
                    {
                        neighbour.SetPiece(neighbourPiece);
                        SetPiece(currentPiece);
                        StateMachine.Instance.ChangeState(StateMachine.Instance.TouchState);

                    });
                }
            });
        }

        public void PopPiece()
        {
            ObjectPoolManager.Instance.ReleaseObject(Piece);
            Piece = null;
            OnTileEmptied?.Invoke(this);
        }
    }
}