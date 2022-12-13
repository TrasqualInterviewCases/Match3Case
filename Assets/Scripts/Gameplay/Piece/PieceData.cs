using Main.Gameplay.Enums;
using UnityEngine;

[CreateAssetMenu(menuName = "Piece Data")]
public class PieceData : ScriptableObject
{
    public Sprite pieceVisual;
    public PieceType pieceType;
}
