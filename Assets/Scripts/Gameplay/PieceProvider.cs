using System.Collections.Generic;
using UnityEngine;

public class PieceProvider : Singleton<PieceProvider>
{
    [SerializeField] List<PieceBase> pieces = new();

    public PieceBase GetRandomPiece()
    {
        return Instantiate(pieces[Random.Range(0, pieces.Count)]);
    }
}
