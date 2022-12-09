using UnityEngine;

public class PieceBase : MonoBehaviour
{
    [field: SerializeField] public PieceType PieceType { get; private set; }

    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void SetPosition(Vector3 tilePos)
    {
        transform.position = tilePos;
    }

    public void Init(PieceType pieceType, Sprite pieceSprite)
    {
        spriteRenderer.sprite = pieceSprite;
        PieceType = pieceType;
    }
}
