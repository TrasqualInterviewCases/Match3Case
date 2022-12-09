using Main.Gameplay.Enums;
using System;
using UnityEngine;

namespace Main.Gameplay.TouchControls
{
    public class TouchControl : MonoBehaviour
    {
        public event Action OnSwipeInputRecieved;

        [SerializeField] float minSwipeDistance = 10;

        Camera cam;

        private Vector2 startPos;
        private Vector2 endPos;

        Tile _selectedTile;

        private void Awake()
        {
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

                endPos = Input.mousePosition;
                if (CalculateSwipeDirection(out DirectionType direction))
                {
                    _selectedTile.RecieveInputDirection(direction);
                    _selectedTile = null;
                    OnSwipeInputRecieved?.Invoke();
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