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
            health = PlayerStatus.Health;
            attack = PlayerStatus.Power;
            defense = PlayerStatus.Defense;
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

        PlayerStatus.Health = jsonObject.health;
        PlayerStatus.Defense = jsonObject.defense;
        PlayerStatus.Power = jsonObject.attack;

        Debug.Log(PlayerStatus.Health);

        sr.Close();
        sr.Dispose();
      
    }
}