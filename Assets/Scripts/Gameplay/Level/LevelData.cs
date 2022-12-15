using UnityEngine;

namespace Main.Gameplay.LevelSystem
{
    [CreateAssetMenu(menuName = "Level/Level Data")]
    public class LevelData : ScriptableObject
    {
        public bool[] spawners;
    }
}