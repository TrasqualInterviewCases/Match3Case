using System;

namespace Main.Utilities
{
    [Serializable]
    public class ArrayData<T> where T : IConvertible
    {
        public ColumnData[] columnDatas = new ColumnData[5];

        public ArrayData(int rowCount, int columnCount)
        {
            columnDatas = new ColumnData[columnCount];
            for (int i = 0; i < columnDatas.Length; i++)
            {
                columnDatas[i] = new ColumnData(rowCount);
            }
        }

        [Serializable]
        public class ColumnData
        {
            public T[] rowDatas;

            public ColumnData(int rowCount)
            {
                rowDatas = new T[rowCount];
            }
        }
    } 
}