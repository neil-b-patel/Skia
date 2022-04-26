using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public int numFeet = 0;
    public int numEyes = 0;
    public float[] position = new float[] {0f, 0f, 0f};
    public int numItems = 0;
    public List<string> items = new List<string>();


    public void SaveData()
    {
        PlayerPrefs.SetInt("numFeet", numFeet);
        PlayerPrefs.SetInt("numEyes", numEyes);

        PlayerPrefs.SetFloat("position.x", position[0]);
        PlayerPrefs.SetFloat("position.y", position[1]);
        PlayerPrefs.SetFloat("position.z", position[2]);

        PlayerPrefs.SetInt("numItems", items.Count);

        for (int i = 0; i < items.Count; i++)
        {
            PlayerPrefs.SetString("item" + i, items[i]);
        }

        Debug.Log("SAVE GAME!");
    }

    public void LoadData()
    {
        numFeet = 0;
        numEyes = 0;
        position = new float[] { 0f, 0f, 0f };
        numItems = 0;
        items = new List<string>();

        List<string> keys = new List<string> {
            "numFeet",
            "numEyes",
            "position.x",
            "position.y",
            "position.z",
            "numItems"
        };

        if (LoadDataExists(keys, items))
        {
            numFeet = LoadInt("numFeet");
            numEyes = LoadInt("numEyes");

            position = new float[] { 0f, 0f, 0f };

            position[0] = LoadFloat("position.x");
            position[1] = LoadFloat("position.y");
            position[2] = LoadFloat("position.z");

            numItems = LoadInt("numItems");

            for (int i = 0; i < numItems; i++)
            {
                items.Add(LoadString("item" + i));
            }
        }
        else
        {
            Debug.LogWarning("SAVE DATA CORRUPTED!");
        }

        Debug.Log("LOAD GAME!");
    }

    public void ResetData()
    {
        numFeet = 0;
        numEyes = 0;
        position = new float[] { 0f, 0f, 0f };
        items = new List<string>();

        PlayerPrefs.DeleteAll();

        Debug.Log("RESET GAME!");
    }

    bool LoadDataExists(List<string> keys, List<string> items)
    {
        foreach (string key in keys)
        {
            if (!PlayerPrefs.HasKey(key))
            {
                return false;
            }
        }
        
        int _numItems = PlayerPrefs.GetInt("numItems");

        try
        {
            for (int i = 0; i < _numItems; i++)
            {
                if (!PlayerPrefs.HasKey("item" + i))
                {
                    return false;
                }
            }
        }
        catch
        {
            return false;
        }

        return true;
    }

    int LoadInt(string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            return PlayerPrefs.GetInt(key);
        }
        else
        {
            Debug.LogWarning("NO SAVED " + key + " FOUND!");
            return 0;
        }
    }

    float LoadFloat(string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            return PlayerPrefs.GetFloat(key);
        }
        else
        {
            Debug.LogWarning("NO SAVED " + key + " FOUND!");
            return 0f;
        }
    }

    string LoadString(string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            return PlayerPrefs.GetString(key);
        }
        else
        {
            Debug.LogWarning("NO SAVED " + key + " FOUND!");
            return "";
        }
    }

}
