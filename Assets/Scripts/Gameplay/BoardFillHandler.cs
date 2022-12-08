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
                newPiece.SetPosition(tiles[i, j].transform.position);
                tiles[i, j].SetPiece(newPiece);
            }
        }
    }
}
