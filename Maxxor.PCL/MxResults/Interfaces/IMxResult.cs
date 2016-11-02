namespace Maxxor.PCL.MxResults.Interfaces
{
    public interface IMxResult
    {
        bool IsSuccess { get; }
        bool IsFailure { get; }
        MxError Error { get; }
    }
}