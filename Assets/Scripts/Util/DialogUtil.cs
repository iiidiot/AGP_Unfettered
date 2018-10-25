using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public static class DialogUtil {

    public static List<DialogStruct> ReadDialogContent(string filepath)
    {
        List<DialogStruct> dialogList = new List<DialogStruct>();
        TextAsset ta = (TextAsset)Resources.Load(filepath, typeof(TextAsset)); //从表中加载数据
        string json = ta.text;
        JsonData data = LitJson.JsonMapper.ToObject(json);

        foreach (JsonData element in data)
        {
            DialogStruct dialog = new DialogStruct();
            dialog.speakerID = element["id"].ToString();
            dialog.content = element["content"].ToString();
            dialogList.Add(dialog);

        }
        Debug.Log("###################" + dialogList);
        return dialogList;
    }
}
