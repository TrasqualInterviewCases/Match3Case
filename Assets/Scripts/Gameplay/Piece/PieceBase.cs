using Main.Gameplay.Enums;
using System.Collections;
using UnityEngine;

namespace Main.Gameplay.Piece
{
    public class PieceBase : MonoBehaviour
    {
        [field: SerializeField] public PieceType PieceType { get; private set; }
        [SerializeField] float fallSpeed = 5f;

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

        public void FallTo(Tile targetTile)
        {
            StartCoroutine(FallCo(targetTile));
        }

        private IEnumerator FallCo(Tile targetTile)
        {
            while (Vector3.Distance(transform.position, targetTile.transform.position) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetTile.transform.position, Time.deltaTime * fallSpeed);
                yield return null;
            }
            transform.position = targetTile.transform.position;
            targetTile.SetPiece(this);
        }
    }
}