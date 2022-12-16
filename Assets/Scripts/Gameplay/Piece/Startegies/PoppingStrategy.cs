using Main.Gameplay.Core;
using UnityEngine;

namespace Main.Gameplay.Pieces.Strategies
{
    public abstract class PoppingStrategy : ScriptableObject
    {
        public abstract void DoOnPop(Tile tile);
    }
}