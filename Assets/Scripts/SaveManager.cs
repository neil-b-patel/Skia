using UnityEngine;

public static class SaveManager
{

    public static void SaveGame(PlayerData playerData)
    {

        //BinaryFormatter formatter = new BinaryFormatter();
        //string path = Application.persistentDataPath + "/player.bin";
        //FileStream stream = new FileStream(path, FileMode.Create);

        //formatter.Serialize(stream, playerData);
        //stream.Close();

        PlayerPrefs.SetInt("numFeet", playerData.getNumFeet());
        PlayerPrefs.SetInt("numEyes", playerData.getNumEyes());

        float[] position = playerData.getPosition();
        PlayerPrefs.SetFloat("position.x", position[0]);
        PlayerPrefs.SetFloat("position.y", position[1]);
        PlayerPrefs.SetFloat("position.z", position[2]);

        Debug.Log("SAVE GAME!");
    }

    public static PlayerData LoadGame()
    {
        int numFeet = PlayerPrefs.GetInt("numFeet");
        int numEyes = PlayerPrefs.GetInt("numEyes");

        float[] position = new float[3]
        {
            PlayerPrefs.GetFloat("position.x"),
            PlayerPrefs.GetFloat("position.y"),
            PlayerPrefs.GetFloat("position.z")
        };

        //Dictionary<string, dynamic> data = new Dictionary<string, dynamic>
        //{
        //    { "numFeet", numFeet },
        //    { "numEyes", numEyes },
        //    { "position", position }
        //};

        PlayerData data = new PlayerData(numFeet, numEyes, position);

        return data;

        //string path = Application.persistentDataPath + "/player.bin";
        //if (File.Exists(path))
        //{
        //    BinaryFormatter formatter = new BinaryFormatter();
        //    FileStream stream = new FileStream(path, FileMode.Open);

        //    PlayerData data = formatter.Deserialize(stream) as PlayerData;
        //    stream.Close();

        //    Debug.Log("LOAD GAME!");
        //    return data;
        //}
        //else
        //{
        //    Debug.LogError("Save file not found in " + path);
        //    return null;
        //}

    }
}
