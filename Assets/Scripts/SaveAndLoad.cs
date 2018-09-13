using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
using UnityEngine.UI;

public class SaveAndLoad : MonoBehaviour {

    void Start()
    {
        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(SaveToFile);
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

    public void SaveToFile()
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

    public void LoadFromFile()
    {
        // to do
    }
}