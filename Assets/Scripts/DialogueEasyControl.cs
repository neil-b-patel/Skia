using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
 public class DialogueEasyControl : MonoBehaviour
 {
     public KeyCode key;
 
     void Update()
     {
        if (Input.GetKeyDown(key))
         {
             GetComponent<Button>().onClick.Invoke();
         }
     }
 }
