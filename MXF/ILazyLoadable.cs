namespace Myriadbits.MXF
{
    public interface ILazyLoadable
    {
        bool IsLoaded { get; set; }
        void Load();
    }
}