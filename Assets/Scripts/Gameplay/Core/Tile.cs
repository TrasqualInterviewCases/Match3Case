using Main.Gameplay.Enums;
using Main.Gameplay.Piece;
using Main.Gameplay.StateMachineSystem;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Main.Gameplay
{
    public class Tile : MonoBehaviour
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        Board _board;

        public PieceBase Piece { get; private set; }

        public Dictionary<DirectionType, Tile> Neighbours { get; private set; } = new Dictionary<DirectionType, Tile>();

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
            Neighbours.Clear();

            if (X >= 0 && X < _board.Columns - 1)
                neighbourRight = Neighbours[DirectionType.Right] = _board.Tiles[X + 1, Y];

            if (X > 0 && X <= _board.Columns - 1)
                neighbourLeft = Neighbours[DirectionType.Left] = _board.Tiles[X - 1, Y];

            if (Y >= 0 && Y < _board.Rows - 1)
                neighbourUp = Neighbours[DirectionType.Up] = _board.Tiles[X, Y + 1];

            if (Y > 0 && Y <= _board.Rows - 1)
                neighbourDown = Neighbours[DirectionType.Down] = _board.Tiles[X, Y - 1];
        }

        public void RecieveInputDirection(DirectionType direction)
        {
            if (!Neighbours.ContainsKey(direction))
            {
                //Do Shake Animation
                return;
            }

            var pieceSwapper = ObjectPoolManager.Instance.GetObject<SwapPieceCommand>();

            var neighbour = Neighbours[direction];

            var neighbourPiece = neighbour.Piece;
            var currentPiece = Piece;

            pieceSwapper.Init(currentPiece, neighbourPiece, 5f, () =>
            {
                neighbour.SetPiece(currentPiece);
                SetPiece(neighbourPiece);

                var neighbourMatchCheck = MatchFinder.FindMatches(neighbour, out List<Tile> neighbourMatches);
                var currentMatchCheck = MatchFinder.FindMatches(this, out List<Tile> currentMatches);

                if (neighbourMatchCheck || currentMatchCheck)
                {
                    var combinedMatches = neighbourMatches.Union(currentMatches).ToList();
                    StateMachine.Instance.ChangeState(StateMachine.Instance.TouchState);

                    //process matches;
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
    }
}