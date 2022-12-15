using Main.Gameplay.LevelSystem;
using UnityEngine;

namespace Main.Gameplay.Core
{
    public class Board : MonoBehaviour
    {
        [Header("Prefabs")]
        [SerializeField] Tile tilePrefab;

        [Header("References")]
        [SerializeField] LevelManager levelManager;
        BoardFillHandler boardFillHandler;

        public int Columns { get; private set; } = 8;
        public int Rows { get; private set; } = 8;

        public Tile[,] Tiles { get; private set; }

        private void Awake()
        {
            var boardSize = levelManager.GetSize();
            Columns = boardSize.x;
            Rows = boardSize.y;
            transform.position = new Vector3(-(Columns - 1) / 2f, -(Rows - 1) / 2f, 0f);
        }

        private void Start()
        {
            Tiles = new Tile[Columns, Rows];
            boardFillHandler = GetComponent<BoardFillHandler>();

            InitializeBoard();
            boardFillHandler.DoInitialFill(Tiles);
        }

        private void InitializeBoard()
        {
            for (int j = 0; j < Rows; j++)
            {
                for (int i = 0; i < Columns; i++)
                {
                    Tiles[i, j] = Instantiate(tilePrefab, transform);
                    Tiles[i, j].Init(i, j, this);
                    if (j == Rows - 1)
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



