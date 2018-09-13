using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;


// 游戏运行中的当前存档信息
// 在每个scene开始前loadFromTempJson，结束后SaveToTempJson 可以避免DontDestoryOnLoad实现Singeloton
// SaveTo存档Json可以实现存档

public static class CurrentGameData {

    public static List<int> itemIDs;
    public static int health;
}


