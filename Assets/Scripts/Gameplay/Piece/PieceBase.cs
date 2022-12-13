using Main.Gameplay.Enums;
using System.Collections;
using UnityEngine;

namespace Main.Gameplay.Piece
{
    public class PieceBase : MonoBehaviour
    {
        [SerializeField] float fallSpeed = 5f;

        PieceData _pieceData;
        public PieceType PieceType => _pieceData.pieceType;

        SpriteRenderer spriteRenderer;

        IEnumerator fallCo;

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

        public void Init(PieceData pieceData)
        {
            _pieceData = pieceData;
            spriteRenderer.sprite = pieceData.pieceVisual;
        }

        public void FallTo(Tile targetTile)
        {
            if (fallCo != null) return;
            fallCo = FallCo(targetTile);
            StartCoroutine(fallCo);
        }

        private IEnumerator FallCo(Tile targetTile)
        {
            targetTile.SetPiece(this);
            while (Vector3.Distance(transform.position, targetTile.transform.position) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetTile.transform.position, Time.deltaTime * fallSpeed);
                yield return null;
            }
            transform.position = targetTile.transform.position;
            StopCoroutine(fallCo);
        }
    }
}