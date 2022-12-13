using Main.Gameplay.StateMachineSystem;
using System.Collections.Generic;

namespace Main.Gameplay
{
    public class MatchProcessor : Singleton<MatchProcessor>
    {
        Board board;

        protected override void Awake()
        {
            base.Awake();
            board = GetComponent<Board>();
        }

        public void ProcessMatches(List<Tile> matchedTiles)
        {
            for (int i = 0; i < matchedTiles.Count; i++)
            {
                matchedTiles[i].PopPiece();
            }
            DOFalls(matchedTiles);
        }

        private void DOFalls(List<Tile> emptyTiles)
        {
            List<int> matchedColumns = new List<int>();

            for (int i = 0; i < emptyTiles.Count; i++)
            {
                if (!matchedColumns.Contains(emptyTiles[i].X))
                {
                    matchedColumns.Add(emptyTiles[i].X);
                }
            }

            for (int i = 0; i < matchedColumns.Count; i++)
            {

            }
            //StateMachine.Instance.ChangeState(StateMachine.Instance.TouchState);
        }
    }
}