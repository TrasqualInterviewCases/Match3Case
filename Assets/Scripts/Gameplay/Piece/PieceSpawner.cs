using Main.Gameplay.Core;
using Main.Gameplay.Piece;
using UnityEngine;

public class PieceSpawner : MonoBehaviour
{
    Tile _owner;

    public void Init(Tile owner)
    {
        _owner = owner;
    }

    public void SpawnPiece()
    {
        var newPiece = PieceProvider.Instance.GetRandomPiece();
        newPiece.transform.position = _owner.transform.position + new Vector3(0f, 2f, 0f);
        newPiece.FallTo(_owner);
    }
}
