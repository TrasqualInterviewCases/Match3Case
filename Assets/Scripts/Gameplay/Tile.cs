using UnityEngine;

namespace Main.Gameplay
{
    public class Tile : MonoBehaviour
    {
        int X;
        int Y;

        Board _board;

        PieceBase _piece;

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
            _piece = piece;
        }
    }
}