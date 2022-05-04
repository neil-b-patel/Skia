using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
 public class DialogueEasyControl : MonoBehaviour
 {
     void Update()
     {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
         {
             GetComponent<Button>().onClick.Invoke();
         }
     }
 }
