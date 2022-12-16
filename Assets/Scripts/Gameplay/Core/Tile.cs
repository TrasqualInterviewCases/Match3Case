using Main.Gameplay.CommandSystem;
using Main.Gameplay.Enums;
using Main.Gameplay.Pieces;
using Main.Gameplay.StateMachineSystem;
using System.Collections.Generic;
using UnityEngine;

namespace Main.Gameplay.Core
{
    public class Tile : MonoBehaviour
    {
        [SerializeField] bool isSpawner;

        public int X { get; private set; }
        public int Y { get; private set; }

        public Board Board { get; private set; }
        private PieceSpawner spawner;
        private StateMachine stateMachine;

        private bool spawnInProgress;

        public Piece Piece { get; private set; }

        public Dictionary<DirectionType, Tile> Neighbours { get; private set; } = new Dictionary<DirectionType, Tile>();

        private void Awake()
        {
            stateMachine = StateMachine.Instance;
        }

        public void Init(int x, int y, Board board)
        {
            X = x;
            Y = y;
            Board = board;

            transform.localPosition = new Vector3(X, Y, 0f);
            gameObject.name = $"({X},{Y})";
        }

        public void SetSpawner()
        {
            spawner = gameObject.AddComponent<PieceSpawner>();
            spawner.Init(this);
        }

        public void SetPiece(Piece piece)
        {
            Piece = piece;
            Piece.SetOwnerTile(this);
        }

        public void RecievePiece(Piece piece)
        {
            spawnInProgress = false;
            SetPiece(piece);
            if (GetNeighbourInDirection(DirectionType.Down, out var lowerNeighbour) && lowerNeighbour.Piece == null)
            {
                DoFall();
            }
            else
            {
                if (TryMatch())
                {
                    stateMachine.ChangeState(stateMachine.AnimationState);
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

            if (X >= 0 && X < Board.Columns - 1)
                Neighbours[DirectionType.Right] = Board.Tiles[X + 1, Y];

            if (X > 0 && X <= Board.Columns - 1)
                Neighbours[DirectionType.Left] = Board.Tiles[X - 1, Y];

            if (Y >= 0 && Y < Board.Rows - 1)
                Neighbours[DirectionType.Up] = Board.Tiles[X, Y + 1];

            if (Y > 0 && Y <= Board.Rows - 1)
                Neighbours[DirectionType.Down] = Board.Tiles[X, Y - 1];
        }

        public void PopPiece()
        {
            if (Piece == null) return;
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
            else if (spawner != null && !spawnInProgress)
            {
                spawner.SpawnPiece();
                spawnInProgress = true;
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