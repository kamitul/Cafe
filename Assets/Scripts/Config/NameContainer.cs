using UnityEngine;

namespace Config
{
    [CreateAssetMenu(menuName = "Names")]
    public class NameContainer : ScriptableObject
    {
        public NameArray Names;

        public string GetRandomName()
        {
            System.Random rnd = new System.Random();
            return Names[rnd.Next(Names.Count - 1)];
        }

        public void LoadData()
        {
            Names = new NameArray();
            Object obj = Resources.Load("names");
            Names = JsonUtility.FromJson<NameArray>(obj.ToString());
        }
    }
}