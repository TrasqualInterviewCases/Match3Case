using Main.Gameplay.Piece;
using System;
using System.Collections;
using UnityEngine;

namespace Main.Gameplay.Command
{
    public class SwapPieceCommand : MonoBehaviour, ICommand, IRewind
    {
        Transform _firstPiece;
        Transform _secondPiece;
        float _speed;

        Vector3 firstPiecePos;
        Vector3 secondPiecePos;

        IEnumerator movementCo;

        public void Init(PieceBase firstPiece, PieceBase secondPiece, float speed, Action OnComplete)
        {
            _firstPiece = firstPiece.transform;
            _secondPiece = secondPiece.transform;
            _speed = speed;
            firstPiecePos = _firstPiece.transform.position;
            secondPiecePos = _secondPiece.transform.position;

            Execute(OnComplete);
        }

        IEnumerator MovePiecesCo(Vector3 firstPieceTarget, Vector3 secondPieceTarget, float speed, Action OnComplete)
        {
            while (Vector3.Distance(_firstPiece.position, firstPieceTarget) > 0.1f && Vector3.Distance(_secondPiece.position, secondPieceTarget) > 0.1f)
            {
                _firstPiece.position = Vector3.MoveTowards(_firstPiece.position, firstPieceTarget, Time.deltaTime * speed);
                _secondPiece.position = Vector3.MoveTowards(_secondPiece.position, secondPieceTarget, Time.deltaTime * speed);
                yield return null;
            }
            _firstPiece.position = firstPieceTarget;
            _secondPiece.position = secondPieceTarget;
            OnComplete();
        }

        public void Execute(Action OnComplete)
        {
            StopPreviousMovement();
            movementCo = MovePiecesCo(secondPiecePos, firstPiecePos, _speed, OnComplete);
            StartCoroutine(movementCo);
        }

        public void Rewind(Action OnComplete)
        {
            StopPreviousMovement();
            movementCo = MovePiecesCo(firstPiecePos, secondPiecePos, _speed, () =>
            {
                OnComplete();
                ObjectPoolManager.Instance.ReleaseObject(this);
            });
            StartCoroutine(movementCo);
        }

        private void StopPreviousMovement()
        {
            if (movementCo != null)
                StopCoroutine(movementCo);
        }
    }
}