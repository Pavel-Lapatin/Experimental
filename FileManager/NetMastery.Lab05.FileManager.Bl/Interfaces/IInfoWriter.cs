

namespace NetMastery.FileManeger.Bl.Interfaces
{
    public interface IInfoWriter<T> where T : class
    {
        void WriteInfo(T obj);
    }
}
