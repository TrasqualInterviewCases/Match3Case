using Main.Gameplay.Enums;
using Main.Gameplay.LevelSystem;
using Main.Gameplay.Piece;
using Main.ObjectPooling;
using UnityEngine;

namespace Main.Gameplay.Core
{
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
            var downMatch = tileToCheck.HasMatchInDirection(DirectionType.Down);

            var leftMatch = tileToCheck.HasMatchInDirection(DirectionType.Left);

            return downMatch || leftMatch;
        }
    }
}