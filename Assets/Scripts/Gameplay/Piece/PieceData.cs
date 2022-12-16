using Main.Gameplay.Enums;
using Main.Gameplay.Pieces.Strategies;
using UnityEngine;

namespace Main.Gameplay.Pieces
{
    [CreateAssetMenu(menuName = "Piece Data")]
    public class PieceData : ScriptableObject
    {
        [field: SerializeField] public Sprite PieceVisual { get; private set; }
        [field: SerializeField] public PieceType PieceType { get; private set; }
        [field: SerializeField] public PoppingStrategy PoppingStrategy { get; private set; }
    }
}
