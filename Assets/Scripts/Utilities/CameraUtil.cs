using Main.Gameplay.LevelSystem;
using UnityEngine;

namespace Main.Utilities
{
    public class CameraUtil : MonoBehaviour
    {
        [SerializeField] LevelManager levelManager;

        Camera cam;

        private void Awake()
        {
            cam = GetComponent<Camera>();
            AdjustOrthoSize();
        }

        private void AdjustOrthoSize()
        {
            cam.orthographicSize = levelManager.GetSize().x;
        }
    }
}