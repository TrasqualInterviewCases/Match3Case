using Main.Gameplay;
using UnityEngine;

public class BoardFillHandler : MonoBehaviour
{
    public void DoInitialFill(Tile[,] tiles)
    {
        for (int i = 0; i < tiles.GetLength(0); i++)
        {
            for (int j = 0; j < tiles.GetLength(1); j++)
            {
                var newPiece = PieceProvider.Instance.GetRandomPiece();

                while (CheckInitialMatch(newPiece, i, j, tiles))
                {
                    ObjectPoolManager.Instance.ReleaseObject(newPiece);
                    newPiece = PieceProvider.Instance.GetRandomPiece();
                }
                newPiece.SetPosition(tiles[i, j].transform.position);
                tiles[i, j].SetPiece(newPiece);
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
