using Main.Gameplay.Core;
using Main.Gameplay.Enums;
using Main.ObjectPooling;
using System;
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

        bool isFalling;

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
            if (isFalling) return;
            StartCoroutine(FallCo(targetTile));
        }

        private IEnumerator FallCo(Tile targetTile)
        {
            isFalling = true;
            while (Vector3.Distance(transform.position, targetTile.transform.position) > 0.05f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetTile.transform.position, Time.deltaTime * fallSpeed);
                yield return null;
            }
            transform.position = targetTile.transform.position;
            isFalling = false;
            targetTile.RecievePiece(this);
        }

        public void Pop()
        {
            StartCoroutine(PlayAnimation());
        }

        private IEnumerator PlayAnimation()
        {
            var t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime * 5f;
                transform.localScale = Vector3.Lerp(Vector3.one, Vector3.one * 1.15f, t);
                yield return null;
            }
            transform.localScale = Vector3.one;
            ObjectPoolManager.Instance.ReleaseObject(this);
        }
    }
}