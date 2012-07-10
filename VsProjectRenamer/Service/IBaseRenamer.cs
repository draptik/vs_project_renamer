namespace Service
{
    public interface IBaseRenamer
    {
        bool Rename(string oldDir, string newDir);
    }
}