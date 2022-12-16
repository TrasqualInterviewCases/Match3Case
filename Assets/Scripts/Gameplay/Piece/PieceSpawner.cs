using Main.Gameplay.Core;
using Main.Gameplay.Pieces;
using UnityEngine;

public class PieceSpawner : MonoBehaviour
{
    private PieceProvider pieceProvider;

    private Tile _owner;

    private void Awake()
    {
        pieceProvider = PieceProvider.Instance;
    }

    public void Init(Tile owner)
    {
        _owner = owner;
    }

    public void SpawnPiece()
    {
        var newPiece = pieceProvider.GetRandomPiece();
        newPiece.transform.position = _owner.transform.position + new Vector3(0f, 2f, 0f);
        newPiece.FallTo(_owner);
    }
}
