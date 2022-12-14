using Main.Gameplay.Enums;
using Main.ObjectPooling;
using Main.Utilities;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Main.Gameplay.Piece
{
    public class PieceProvider : Singleton<PieceProvider>
    {
        [SerializeField] List<PieceData> pieceDatas = new();

        public PieceBase GetRandomPiece()
        {
            var newPiece = ObjectPoolManager.Instance.GetObject<PieceBase>();
            var randType = EnumUtilities.GetRandomFromEnum<PieceType>();
            newPiece.Init(GetDataByType(randType));
            return newPiece;
        }

        private PieceData GetDataByType(PieceType pieceType)
        {
            return pieceDatas.FirstOrDefault((data) => data.pieceType == pieceType);
        }
    }
}