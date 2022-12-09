using Main.Gameplay;
using Main.Gameplay.Piece;
using UnityEngine;

public class BoardFillHandler : MonoBehaviour
{
    public void DoInitialFill(Tile[,] tiles)
    {
        for (int i = 0; i < tiles.GetLength(1); i++)
        {
            for (int j = 0; j < tiles.GetLength(0); j++)
            {
                var newPiece = PieceProvider.Instance.GetRandomPiece();

                while (CheckInitialMatch(newPiece, j, i, tiles))
                {
                    ObjectPoolManager.Instance.ReleaseObject(newPiece);
                    newPiece = PieceProvider.Instance.GetRandomPiece();
                }
                tiles[j, i].SetPiece(newPiece);
            }
        }
    }

    private bool CheckInitialMatch(PieceBase piecesToCheck, int i, int j, Tile[,] tiles)
    {
        var downMatch = i >= 2 && tiles[i - 1, j].Piece.PieceType == piecesToCheck.PieceType && tiles[i - 2, j].Piece.PieceType == piecesToCheck.PieceType;

        var leftMatch = j >= 2 && tiles[i, j - 1].Piece.PieceType == piecesToCheck.PieceType && tiles[i, j - 2].Piece.PieceType == piecesToCheck.PieceType;

        return downMatch || leftMatch;
    }
}
