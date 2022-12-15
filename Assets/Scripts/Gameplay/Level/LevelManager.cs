using Main.Gameplay.Core;
using UnityEngine;

namespace Main.Gameplay.LevelSystem
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] LevelData levelData;

        public Vector2Int GetSize() => new (levelData.Columns, levelData.Rows);

        public void AssignLevelData(Tile tile)
        {
            if (levelData.Spawners[tile.X] == true)
            {
                tile.SetSpawner();
            }
        }
    }
}