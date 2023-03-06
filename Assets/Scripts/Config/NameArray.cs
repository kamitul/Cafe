namespace Config
{
    [System.Serializable]
    public class NameArray
    {
        public string[] Names;

        public string this[int key]
        {
            get => Names[key];
        }

        public int Count { get => Names.Length; }
    }
}