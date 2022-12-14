using Main.Gameplay.Enums;
using Main.Gameplay.Piece;
using System;
using System.Collections;
using UnityEngine;

namespace Main.Gameplay.Command
{
    public class PieceSwapper : MonoBehaviour, ICommand
    {
        Tile _firstTile;
        Tile _secondTile;
        PieceBase _firstPiece;
        PieceBase _secondPiece;

        Vector3 firstPiecePos;
        Vector3 secondPiecePos;

        IEnumerator movementCo;


        public void Init(Tile firstTile, DirectionType direction)
        {
            _firstTile = firstTile;
            _firstTile.GetNeighbourInDirection(direction, out _secondTile);
            _firstPiece = _firstTile.Piece;
            _secondPiece = _secondTile.Piece;
            firstPiecePos = _firstPiece.transform.position;
            secondPiecePos = _secondPiece.transform.position;
            CommandManager.Instance.AddCommand(this);
        }

        IEnumerator MovePiecesCo(Vector3 firstPieceTarget, Vector3 secondPieceTarget, Action OnComplete)
        {
            while (Vector3.Distance(_firstPiece.transform.position, firstPieceTarget) > 0.1f && Vector3.Distance(_secondPiece.transform.position, secondPieceTarget) > 0.1f)
            {
                _firstPiece.transform.position = Vector3.MoveTowards(_firstPiece.transform.position, firstPieceTarget, Time.deltaTime * 5f);
                _secondPiece.transform.position = Vector3.MoveTowards(_secondPiece.transform.position, secondPieceTarget, Time.deltaTime * 5f);
                yield return null;
            }
            _firstPiece.transform.position = firstPieceTarget;
            _secondPiece.transform.position = secondPieceTarget;
            OnComplete();
        }

        public void Execute(Action OnComplete)
        {
            StopPreviousMovement();
            movementCo = MovePiecesCo(secondPiecePos, firstPiecePos, () =>
            {
                _firstTile.SetPiece(_secondPiece);
                _secondTile.SetPiece(_firstPiece);
                var firstPieceMatched = _firstTile.CheckPiece();
                var secondPieceMatched = _secondTile.CheckPiece();
                if (!firstPieceMatched && !secondPieceMatched)
                {
                    Rewind(OnComplete);
                }
                else
                {
                    OnComplete();
                }
            });
            StartCoroutine(movementCo);
        }

        public void Rewind(Action OnComplete)
        {
            StopPreviousMovement();
            movementCo = MovePiecesCo(firstPiecePos, secondPiecePos, () =>
            {
                _firstTile.SetPiece(_firstPiece);
                _secondTile.SetPiece(_secondPiece);
                OnComplete();
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