using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine;

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
        using(StreamReader sr = new StreamReader(Application.dataPath + "/ScriptableObjects" + "/names.json"))
        {
            string json = sr.ReadToEnd();
            sr.Close();
            Names = JsonUtility.FromJson<NameArray>(json);
        }
    }
}
