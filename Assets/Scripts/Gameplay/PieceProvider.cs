using UnityEngine;

public class PieceProvider : Singleton<PieceProvider>
{
    [SerializeField] Sprite redSprite;
    [SerializeField] Sprite greenSprite;
    [SerializeField] Sprite blueSprite;
    [SerializeField] Sprite yellowSprite;

    public PieceBase GetRandomPiece()
    {
        var newPiece = ObjectPoolManager.Instance.GetObject<PieceBase>();
        var randType = EnumUtilities.GetRandomFromEnum<PieceType>();
        newPiece.Init(randType, GetSpriteByType(randType));
        return newPiece;
    }

    private Sprite GetSpriteByType(PieceType pieceType)
    {
        switch (pieceType)
        {
            case PieceType.Red:
                return redSprite;
            case PieceType.Green:
                return greenSprite;
            case PieceType.Blue:
                return blueSprite;
            case PieceType.Yellow:
                return yellowSprite;
            default:
                return null;
        }
    }
}
