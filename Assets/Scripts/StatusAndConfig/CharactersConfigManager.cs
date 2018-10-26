using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CharactersConfigManager
{
    public const string k_Name = "Name";
    public const string k_GameObjctPath = "Game Object Path";
    public const string k_SpritePath = "Sprite Path";

    public const string k_PlayerID = "player";
    public const string k_SisterID = "sister";

    // to do: maybe read this dictionary from config json when game start
    private static Dictionary<string, Dictionary<string, string>> m_CharacterDictionary = new Dictionary<string, Dictionary<string, string>>()
    {
         {
            k_PlayerID, new Dictionary<string, string>()
            {
                {k_Name, "Gentian"},
                {k_GameObjctPath, "PlayerGroup/Player" },
                {k_SpritePath, "Sprites/CharacterHead/Gentian" }
            }
         },

          {
            k_SisterID, new Dictionary<string, string>()
            {
                {k_Name, "Oenothera"},
                {k_GameObjctPath, "NPC/Sister" },
                {k_SpritePath, "Sprites/CharacterHead/sister" }
            }
         },
    };

    public static string GetCharacterName(string characterID)
    {
        if (m_CharacterDictionary.ContainsKey(characterID))
        {
            return m_CharacterDictionary[characterID][k_Name];
        }
        else
        {
            return "";
        }
    }

    public static string GetCharacterGameObjectPath(string characterID)
    {
        if (m_CharacterDictionary.ContainsKey(characterID))
        {
            return m_CharacterDictionary[characterID][k_GameObjctPath];
        }
        else
        {
            return "";
        }
    }

    public static string GetCharacterSpritePath(string characterID)
    {
        if (m_CharacterDictionary.ContainsKey(characterID))
        {
            return m_CharacterDictionary[characterID][k_SpritePath];
        }
        else
        {
            return "";
        }
    }


}
