using Main.Gameplay.LevelSystem;
using UnityEngine;

namespace Main.Gameplay.Core
{
    public class Board : MonoBehaviour
    {
        [Header("Prefabs")]
        [SerializeField] Tile tilePrefab;

        [field: Header("Board Dimensions")]
        [field: SerializeField] public int Columns { get; private set; } = 8;
        [field: SerializeField] public int Rows { get; private set; } = 8;

        [Header("References")]
        [SerializeField] LevelManager levelManager;
        BoardFillHandler boardFillHandler;

        public Tile[,] Tiles { get; private set; }

        private void Start()
        {
            Tiles = new Tile[Rows, Columns];
            boardFillHandler = GetComponent<BoardFillHandler>();

            InitializeBoard();
            boardFillHandler.DoInitialFill(Tiles);
        }

        private void InitializeBoard()
        {
            for (int j = 0; j < Columns; j++)
            {
                for (int i = 0; i < Rows; i++)
                {
                    Tiles[i, j] = Instantiate(tilePrefab, transform);
                    Tiles[i, j].Init(i, j, this);
                    if (j == Columns - 1)
                    {
                        levelManager.AssignLevelData(Tiles[i, j]);
                    }
                }
            }
            SetupTileNeighbours();
        }

        private void SetupTileNeighbours()
        {
            foreach (var tile in Tiles)
            {
                tile.SetupNeighbours();
            }
        }
    }
}



