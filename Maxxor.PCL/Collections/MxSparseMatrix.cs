using System.Collections.Generic;

namespace Maxxor.PCL.Collections
{
    public class MxSparseMatrix<T>
    {
        protected Dictionary<int, Dictionary<int, T>> Rows;

        public MxSparseMatrix()
        {
            Rows = new Dictionary<int, Dictionary<int, T>>();
        }

        public T this[int row, int col]
        {
            get => GetAt(row, col);
            set => SetAt(row, col, value);
        }

        public T GetAt(int row, int col)
        {
            if (Rows.TryGetValue(row, out Dictionary<int, T> cols))
            {
                if (cols == null) return default(T);
                if (cols.TryGetValue(col, out T value))
                    return value;
            }
            return default(T);
        }

        public void SetAt(int row, int column, T value)
        {
            if (EqualityComparer<T>.Default.Equals(value, default(T)))
            {
                RemoveAt(row, column);
            }
            else
            {
                if (!Rows.TryGetValue(row, out Dictionary<int, T> columns))
                {
                    columns = new Dictionary<int, T>();
                    Rows.Add(row, columns);
                }
                columns[column] = value;
            }
        }

        public void RemoveAt(int row, int column)
        {
            if (Rows.TryGetValue(row, out Dictionary<int, T> columns))
            {
                columns.Remove(column);
                if (columns.Count == 0)
                    Rows.Remove(row);
            }
        }
    }
}