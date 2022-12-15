using Main.Gameplay.CommandSystem;
using Main.Gameplay.Core;
using Main.Gameplay.Enums;
using Main.Gameplay.StateMachineSystem;
using UnityEngine;

namespace Main.Gameplay.TouchControls
{
    [RequireComponent(typeof(PieceSwapper))]
    public class TouchControl : MonoBehaviour
    {
        [SerializeField] float minSwipeDistance = 10;

        private StateMachine stateMachine;
        private PieceSwapper pieceSwapper;
        private Camera cam;

        private Vector2 startPos;
        private Vector2 endPos;

        private Tile _selectedTile;

        private void Awake()
        {
            stateMachine = StateMachine.Instance;
            pieceSwapper = GetComponent<PieceSwapper>();
            cam = Camera.main;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var ray = cam.ScreenPointToRay(Input.mousePosition);
                var hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);
                if (hit.transform != null)
                {
                    if (hit.transform.TryGetComponent(out Tile selectedPiece))
                    {
                        _selectedTile = selectedPiece;
                        startPos = Input.mousePosition;
                    }
                }
            }

            if (Input.GetMouseButton(0))
            {
                if (_selectedTile == null) return;
                if (_selectedTile.Piece == null) return;

                endPos = Input.mousePosition;
                if (CalculateSwipeDirection(out DirectionType direction))
                {
                    if (_selectedTile.CanSwap(direction))
                    {
                        pieceSwapper.Init(_selectedTile, direction);
                        stateMachine.ChangeState(stateMachine.AnimationState);
                    }
                    _selectedTile = null;
                }
            }
        }

        private bool CalculateSwipeDirection(out DirectionType direction)
        {
            direction = DirectionType.None;
            var directionVector = endPos - startPos;

            if (directionVector.magnitude < minSwipeDistance)
            {
                direction = DirectionType.None;
                return false;
            }

            var directionAngle = Mathf.Atan2(directionVector.y, directionVector.x) * Mathf.Rad2Deg;
            if (directionAngle >= -45 && directionAngle <= 45)
            {
                direction = DirectionType.Right;
            }
            else if (directionAngle > 45 && directionAngle < 135)
            {
                direction = DirectionType.Up;
            }
            else if (directionAngle >= 135 || directionAngle <= -135)
            {
                direction = DirectionType.Left;
            }
            else if (directionAngle > -135 && directionAngle < -45)
            {
                direction = DirectionType.Down;
            }
            return true;
        }
    }
}