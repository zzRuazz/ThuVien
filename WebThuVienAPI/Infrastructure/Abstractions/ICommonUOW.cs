namespace WebThuVienAPI.Infrastructure.Abstractions;

public interface ICommonUOW
{
    /// <summary>
    /// Begin
    /// </summary>
    void Begin();

    /// <summary>
    /// Commit
    /// </summary>
    void Commit();
}
