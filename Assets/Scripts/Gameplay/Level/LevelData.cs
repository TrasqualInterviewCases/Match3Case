using UnityEngine;

namespace Main.Gameplay.LevelSystem
{
    [CreateAssetMenu(menuName = "Level/Level Data")]
    public class LevelData : ScriptableObject
    {
        [field: SerializeField] public int Columns { get; private set; }
        [field: SerializeField] public int Rows { get; private set; }
        [field: SerializeField] public bool[] Spawners { get; private set; }
    }
}