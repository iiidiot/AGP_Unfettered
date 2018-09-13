using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;


// 游戏运行中的当前存档信息
// 在每个scene开始前loadFromTempJson，结束后SaveToTempJson 可以避免DontDestoryOnLoad实现Singeloton
// SaveTo存档Json可以实现存档

[CreateAssetMenu]
public static class CurrentGameData {

    public static List<int> itemIDs;
    public static int health;
}

public class CurrentGameDataObject
{
    public List<int> itemIDs;
    public int health;
    public CurrentGameDataObject()
    {
        itemIDs = CurrentGameData.itemIDs;
        health = CurrentGameData.health;
    }
}

public class SaveAndLoad
{
  
    public static void SaveToFile()
    {
        string filePath = Application.dataPath + @"/Resources/Json.json";
        string json = JsonMapper.ToJson(new CurrentGameDataObject());
        Debug.Log("Saving: " + json);
        FileInfo file = new FileInfo(filePath);
       
        StreamWriter sw = file.CreateText();
     
        sw.WriteLine(json);
  
        sw.Close();
        sw.Dispose();
    }

    public static void LoadFromFile()
    {
        // to do
    }
}