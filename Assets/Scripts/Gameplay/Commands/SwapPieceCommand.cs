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

        float unmatchedTileCount;

        public void Init(Tile firstTile, DirectionType direction, float speed)
        {
            _firstTile = firstTile;
            if (!_firstTile.GetNeighbourInDirection(direction, out var neighbour))
            {
                ObjectPoolManager.Instance.ReleaseObject(this);
                return;
            }
            _secondTile = neighbour;
            _firstPiece = _firstTile.Piece;
            _secondPiece = _secondTile.Piece;
            _speed = speed;
            firstPiecePos = _firstPiece.transform.position;
            secondPiecePos = _secondPiece.transform.position;

            _firstTile.OnNoMatchFound += CheckMatch;
            _secondTile.OnNoMatchFound += CheckMatch;

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
            ObjectPoolManager.Instance.ReleaseObject(this);
            StateMachine.Instance.ChangeState(StateMachine.Instance.TouchState); //For test purpose, remove later
        }

        public void Execute()
        {
            StopPreviousMovement();
            movementCo = MovePiecesCo(secondPiecePos, firstPiecePos, _speed, () =>
            {
                _firstTile.RecievePiece(_secondPiece);
                _secondTile.RecievePiece(_firstPiece);
            });
            StartCoroutine(movementCo);
        }

        public void Rewind()
        {
            StopPreviousMovement();
            movementCo = MovePiecesCo(firstPiecePos, secondPiecePos, _speed, () =>
            {
                _firstTile.RecievePiece(_firstPiece);
                _secondTile.RecievePiece(_secondPiece);
                StateMachine.Instance.ChangeState(StateMachine.Instance.TouchState);
            });
            StartCoroutine(movementCo);
        }

        private void CheckMatch(Tile tile)
        {
            unmatchedTileCount++;
            if (unmatchedTileCount >= 2)
            {
                Rewind();
            }
        }

        private void StopPreviousMovement()
        {
            if (movementCo != null)
                StopCoroutine(movementCo);
        }
    }
}