//using UnityEngine;

//public static class SaveManager
//{

//    public static void SaveGame(PlayerData playerData)
//    {

//        PlayerPrefs.SetInt("numFeet", playerData.numFeet);
//        PlayerPrefs.SetInt("numEyes", playerData.numEyes);

//        float[] position = playerData.position;
//        PlayerPrefs.SetFloat("position.x", position[0]);
//        PlayerPrefs.SetFloat("position.y", position[1]);
//        PlayerPrefs.SetFloat("position.z", position[2]);

//        Debug.Log("SAVE GAME!");
//    }

//    public static PlayerData LoadGame()
//    {
//        int numFeet = PlayerPrefs.GetInt("numFeet");
//        int numEyes = PlayerPrefs.GetInt("numEyes");

//        float[] position = new float[3]
//        {
//            PlayerPrefs.GetFloat("position.x"),
//            PlayerPrefs.GetFloat("position.y"),
//            PlayerPrefs.GetFloat("position.z")
//        };

//        //PlayerData data = new PlayerData(numFeet, numEyes, position);

//        return data;

//    }
//}
