using Main.Gameplay.Enums;
using UnityEngine;

namespace Main.Gameplay.Piece
{
    public class PieceBase : MonoBehaviour
    {
        [field: SerializeField] public PieceType PieceType { get; private set; }

        SpriteRenderer spriteRenderer;

        private void Awake()
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void SetOwnerTile(Tile tile)
        {
            gameObject.name = GetType().Name + $"({tile.X}, {tile.Y})";
        }

        public void Init(PieceType pieceType, Sprite pieceSprite)
        {
            spriteRenderer.sprite = pieceSprite;
            PieceType = pieceType;
        }
    }
}