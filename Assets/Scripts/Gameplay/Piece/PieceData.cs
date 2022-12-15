using Main.Gameplay.Enums;
using Main.Gameplay.Piece.Strategies;
using UnityEngine;

namespace Main.Gameplay.Piece
{
    [CreateAssetMenu(menuName = "Piece Data")]
    public class PieceData : ScriptableObject
    {
        public Sprite pieceVisual;
        public PieceType pieceType;
        public PoppingStrategy poppingStrategy;
    }
}
