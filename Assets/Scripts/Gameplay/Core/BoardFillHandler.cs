using Main.Gameplay;
using Main.Gameplay.Enums;
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
                tiles[j, i].SetPiece(newPiece);
                newPiece.SetPosition(tiles[j, i].transform.position);

                while (CheckInitialMatch(tiles[j, i]))
                {
                    ObjectPoolManager.Instance.ReleaseObject(newPiece);
                    newPiece = PieceProvider.Instance.GetRandomPiece();
                    tiles[j, i].SetPiece(newPiece);
                    newPiece.SetPosition(tiles[j, i].transform.position);
                }
            }
        }
    }

    private bool CheckInitialMatch(Tile tileToCheck)
    {
        var downMatch = MatchFinder.FindMatchInDirection(tileToCheck, DirectionType.Down);

        var leftMatch = MatchFinder.FindMatchInDirection(tileToCheck, DirectionType.Left);

        return downMatch || leftMatch;
    }
}
