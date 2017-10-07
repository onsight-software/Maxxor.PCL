namespace Maxxor.PCL.Collections
{
    public interface IMxSparseMatrix<T>
    {
        T this[int row, int col] { get; set; }
        T GetAt(int row, int col);
        void SetAt(int row, int column, T value);
        void RemoveAt(int row, int column);
    }
}