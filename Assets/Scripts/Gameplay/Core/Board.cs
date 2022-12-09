using UnityEngine;

namespace Main.Gameplay
{
    public class Board : MonoBehaviour
    {
        [Header("Prefabs")]
        [SerializeField] Tile tilePrefab;

        [Header("Board Dimensions")]
        [SerializeField] int columns = 8;
        [SerializeField] int rows = 8;

        BoardFillHandler boardFillHandler;

        public Tile[,] Tiles;

        private void Start()
        {
            Tiles = new Tile[rows, columns];
            boardFillHandler = GetComponent<BoardFillHandler>();

            InitializeBoard();
            boardFillHandler.DoInitialFill(Tiles);
        }

        private void InitializeBoard()
        {
            for (int j = 0; j < columns; j++)
            {
                for (int i = 0; i < rows; i++)
                {
                    Tiles[i, j] = Instantiate(tilePrefab, transform);
                    Tiles[i, j].Init(i, j, this);
                }
            }
        }
    }
}