//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class TitleScreenManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            Debug.Log("START GAME!");
        }
    }
}
