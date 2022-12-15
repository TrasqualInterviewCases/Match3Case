using Main.Gameplay.Core;
using Main.Gameplay.Enums;
using Main.ObjectPooling;
using System;
using System.Collections;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

namespace Main.Gameplay.Piece
{
    public class PieceBase : MonoBehaviour
    {
        [SerializeField] float fallSpeed = 5f;

        private ObjectPoolManager poolManager;
        private PieceData _pieceData;
        public PieceType PieceType => _pieceData.PieceType;

        private SpriteRenderer spriteRenderer;

        private Tile owner;

        private bool isFalling;

        private void Awake()
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            poolManager = ObjectPoolManager.Instance;
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void SetOwnerTile(Tile tile)
        {
            owner = tile;
            gameObject.name = GetType().Name + $"({owner.X}, {owner.Y})";
        }

        public void Init(PieceData pieceData)
        {
            _pieceData = pieceData;
            spriteRenderer.sprite = pieceData.PieceVisual;
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
            _pieceData.PoppingStrategy.DoOnPop(owner);
            StartCoroutine(PlayAnimation());
        }

        private IEnumerator PlayAnimation()
        {
            var t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime * 10f;
                transform.localScale = Vector3.Lerp(Vector3.one, Vector3.one * 1.15f, t);
                yield return null;
            }
            transform.localScale = Vector3.one;
            poolManager.ReleaseObject(this);
        }
    }
}