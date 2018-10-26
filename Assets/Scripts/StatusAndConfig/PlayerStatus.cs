using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerStatus {

    public const double MAX_HEALTH = 10;
    public static double health = 10;
    public static double attack = 1;
    public static double defense = 0;

    public static Dictionary<string, string> item = new Dictionary<string, string>();
}
