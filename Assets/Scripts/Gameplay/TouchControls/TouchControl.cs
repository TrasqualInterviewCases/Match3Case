using UnityEngine;

namespace Main.Gameplay.TouchControls
{
    public class TouchControl : MonoBehaviour
    {
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
                _selectedTile.RecieveInputDirection(CalculateSwipeDirection());
            }

            if (Input.GetMouseButtonUp(0))
            {
                _selectedTile = null;
            }
        }

        private DirectionType CalculateSwipeDirection()
        {
            var directionVector = endPos - startPos;

            if (directionVector.magnitude < minSwipeDistance) return DirectionType.None;

            var directionAngle = Mathf.Atan2(directionVector.y, directionVector.x) * Mathf.Rad2Deg;
            Debug.Log(directionAngle);
            if (directionAngle >= -45 && directionAngle <= 45)
            {
                return DirectionType.Right;
            }
            else if (directionAngle > 45 && directionAngle < 135)
            {
                return DirectionType.Up;
            }
            else if (directionAngle >= 135 || directionAngle <= -135)
            {
                return DirectionType.Left;
            }
            else if (directionAngle > -135 && directionAngle < -45)
            {
                return DirectionType.Down;
            }
            return DirectionType.None;
        }
    }
}