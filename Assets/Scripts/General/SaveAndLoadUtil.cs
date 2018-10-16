using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
using UnityEngine.UI;



public static class SaveAndLoadUtil {

    public const string playerStatusPath =  @"/Resources/PlayerSavedData/PlayerStatus.json";

    public class PlayerStatusSavableObject
    {
        public double health;
        public double attack;
        public double defense;
        public PlayerStatusSavableObject()
        {
            health = PlayerStatus.health;
            attack = PlayerStatus.attack;
            defense = PlayerStatus.defense;
        }
    }

    public static void SavePlayerStatus()
    {
        string filePath = Application.dataPath + playerStatusPath;
        string json = JsonMapper.ToJson(new PlayerStatusSavableObject());
        Debug.Log("Saving: " + json);
        FileInfo file = new FileInfo(filePath);

        StreamWriter sw = file.CreateText();

        sw.WriteLine(json);

        sw.Close();
        sw.Dispose();
    }

    public static void LoadPlayerStatus()
    {
        string FileName = Application.dataPath + playerStatusPath;
        StreamReader sr = File.OpenText(FileName);
        string input = sr.ReadToEnd();
        PlayerStatusSavableObject jsonObject = JsonMapper.ToObject<PlayerStatusSavableObject>(input);

        PlayerStatus.health = jsonObject.health;
        PlayerStatus.defense = jsonObject.defense;
        PlayerStatus.attack = jsonObject.attack;

        Debug.Log(PlayerStatus.health);

        sr.Close();
        sr.Dispose();
      
    }
}