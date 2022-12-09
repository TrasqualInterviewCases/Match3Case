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

        public void SetOwnerTile(Tile tile)
        {
            transform.position = tile.transform.position;
            gameObject.name = GetType().Name + $"({tile.X}, {tile.Y})";
        }

        public void Init(PieceType pieceType, Sprite pieceSprite)
        {
            spriteRenderer.sprite = pieceSprite;
            PieceType = pieceType;
        }
    }
}