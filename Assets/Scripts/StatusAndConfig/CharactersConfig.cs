using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CharactersConfig
{
    public const string NAME = "Name";
    public const string GAME_OBJECT_PATH = "Game Object Path";
    public const string SPRITE_PATH = "Sprite Path";

    public const string PLAYER_ID = "player";
    public const string Sister_ID = "sister";


    public static Dictionary<string, Dictionary<string, string>> character = new Dictionary<string, Dictionary<string, string>>()
    {
         {
            PLAYER_ID, new Dictionary<string, string>()
            {
                {NAME, "Gentian"},
                {GAME_OBJECT_PATH, "PlayerGroup/Player" },
                {SPRITE_PATH, "Sprites/CharacterHead/Gentian" }
            }
         },

          {
            Sister_ID, new Dictionary<string, string>()
            {
                {NAME, "Oenothera"},
                {GAME_OBJECT_PATH, "NPC/Sister" },
                {SPRITE_PATH, "Sprites/CharacterHead/sister" }
            }
         },
    };

}
