using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CharactersConfig
{
    public const string NAME = "Name";
    public const string GAME_OBJECT_PATH = "Game Object Path";
    public const string SPRITE_PATH = "Sprite Path";

    public static Dictionary<string, Dictionary<string, string>> character = new Dictionary<string, Dictionary<string, string>>()
    {
         {
            "player", new Dictionary<string, string>()
            {
                {NAME, "Gentian"},
                {GAME_OBJECT_PATH, "PlayerGroup/Player" },
                {SPRITE_PATH, "Assets/Resources/Sprites/CharacterHead/Gentian" }
            }
         },

          {
            "sister", new Dictionary<string, string>()
            {
                {NAME, "Oenothera"},
                {GAME_OBJECT_PATH, "NPC/Sister" },
                {SPRITE_PATH, "Assets/Resources/Sprites/CharacterHead/sister" }
            }
         },
    };

}
