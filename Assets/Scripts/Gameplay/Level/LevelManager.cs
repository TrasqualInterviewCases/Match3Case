using Main.Gameplay.Core;
using UnityEngine;

namespace Main.Gameplay.LevelSystem
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] LevelData levelData;

        public void AssignLevelData(Tile tile)
        {
            if (levelData.spawners[tile.X] == true)
            {
                tile.SetSpawner();
            }
        }
    }
}