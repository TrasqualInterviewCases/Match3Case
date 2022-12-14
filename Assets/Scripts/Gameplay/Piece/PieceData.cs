using Main.Gameplay.Enums;
using UnityEngine;

namespace Main.Gameplay.Piece
{
	[CreateAssetMenu(menuName = "Piece Data")]
	public class PieceData : ScriptableObject
	{
		public Sprite pieceVisual;
		public PieceType pieceType;
	} 
}
