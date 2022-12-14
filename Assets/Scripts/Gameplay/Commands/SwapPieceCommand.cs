using Main.Gameplay.Enums;
using Main.Gameplay.Piece;
using Main.Gameplay.StateMachineSystem;
using System;
using System.Collections;
using UnityEngine;

namespace Main.Gameplay.Command
{
    public class SwapPieceCommand : MonoBehaviour, ICommand
    {
        Tile _firstTile;
        Tile _secondTile;
        PieceBase _firstPiece;
        PieceBase _secondPiece;
        float _speed;

        Vector3 firstPiecePos;
        Vector3 secondPiecePos;

        IEnumerator movementCo;

        public void Init(Tile firstTile, DirectionType direction, float speed)
        {
            _firstTile = firstTile;
            if (!_firstTile.GetNeighbourInDirection(direction, out var neighbour))
            {
                StateMachine.Instance.ChangeState(StateMachine.Instance.TouchState);
                ObjectPoolManager.Instance.ReleaseObject(this);
                return;
            }
            if(neighbour.Piece == null)
            {
                StateMachine.Instance.ChangeState(StateMachine.Instance.TouchState);
                ObjectPoolManager.Instance.ReleaseObject(this);
                return;
            }
            _secondTile = neighbour;
            _firstPiece = _firstTile.Piece;
            _secondPiece = _secondTile.Piece;
            _speed = speed;
            firstPiecePos = _firstPiece.transform.position;
            secondPiecePos = _secondPiece.transform.position;

            Execute();
        }

        IEnumerator MovePiecesCo(Vector3 firstPieceTarget, Vector3 secondPieceTarget, float speed, Action OnComplete)
        {
            while (Vector3.Distance(_firstPiece.transform.position, firstPieceTarget) > 0.1f && Vector3.Distance(_secondPiece.transform.position, secondPieceTarget) > 0.1f)
            {
                _firstPiece.transform.position = Vector3.MoveTowards(_firstPiece.transform.position, firstPieceTarget, Time.deltaTime * speed);
                _secondPiece.transform.position = Vector3.MoveTowards(_secondPiece.transform.position, secondPieceTarget, Time.deltaTime * speed);
                yield return null;
            }
            _firstPiece.transform.position = firstPieceTarget;
            _secondPiece.transform.position = secondPieceTarget;
            OnComplete();
        }

        public void Execute()
        {
            StopPreviousMovement();
            movementCo = MovePiecesCo(secondPiecePos, firstPiecePos, _speed, () =>
            {
                _firstTile.SetPiece(_secondPiece);
                _secondTile.SetPiece(_firstPiece);
                var firstPieceMatched = _firstTile.CheckPiece();
                var secondPieceMatched = _secondTile.CheckPiece();
                if (!firstPieceMatched && !secondPieceMatched)
                {
                    Rewind();
                }
                else
                {
                    StateMachine.Instance.ChangeState(StateMachine.Instance.TouchState);
                    ObjectPoolManager.Instance.ReleaseObject(this);
                }
            });
            StartCoroutine(movementCo);
        }

        public void Rewind()
        {
            StopPreviousMovement();
            movementCo = MovePiecesCo(firstPiecePos, secondPiecePos, _speed, () =>
            {
                _firstTile.SetPiece(_firstPiece);
                _secondTile.SetPiece(_secondPiece);
                StateMachine.Instance.ChangeState(StateMachine.Instance.TouchState);
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