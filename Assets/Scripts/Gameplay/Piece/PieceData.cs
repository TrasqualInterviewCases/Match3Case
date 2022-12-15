using Main.Gameplay.Enums;
using Main.Gameplay.Piece.Strategies;
using UnityEngine;

namespace Main.Gameplay.Piece
{
    [CreateAssetMenu(menuName = "Piece Data")]
    public class PieceData : ScriptableObject
    {
        [field: SerializeField] public Sprite PieceVisual { get; private set; }
        [field: SerializeField] public PieceType PieceType { get; private set; }
        [field: SerializeField] public PoppingStrategy PoppingStrategy { get; private set; }
    }
}
