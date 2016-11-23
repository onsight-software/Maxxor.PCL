namespace Maxxor.PCL.Result.Interfaces
{
    public interface IMxResult
    {
        bool IsSuccess { get; }
        bool IsFailure { get; }
        MxError Error { get; }
    }
}