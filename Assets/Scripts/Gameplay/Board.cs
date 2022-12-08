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

        Tile[,] tiles;

        private void Start()
        {
            tiles = new Tile[rows, columns];
            boardFillHandler = GetComponent<BoardFillHandler>();

            InitializeBoard();
            boardFillHandler.DoInitialFill(tiles);
        }

        private void InitializeBoard()
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    tiles[i, j] = Instantiate(tilePrefab, transform);
                    tiles[i, j].Init(i, j, this);
                }
            }
        }
    }
}