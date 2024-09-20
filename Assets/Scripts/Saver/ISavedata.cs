namespace Assets.LoopBuild.Scripts.Saver
{
    public interface ISavedata
    {
        public void Save<T>(T data, string path);
        public void Save<T>(T[] data, string path);
        public T[] Load<T>(string path);
    }
}
