using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaedalusDialogue : MonoBehaviour
{
    [SerializeField]Yarn.Unity.DialogueRunner dialogueRunner;

   void OnTriggerEnter(Collider other)
    {   
            Debug.Log("In");
           Rigidbody rb = other.GetComponent<Rigidbody>();
           dialogueRunner.StartDialogue("Daedalus");
           
           rb.constraints = RigidbodyConstraints.None;
            rb.constraints = RigidbodyConstraints.FreezePositionZ |
                             RigidbodyConstraints.FreezeRotationX |
                             RigidbodyConstraints.FreezeRotationY |
                             RigidbodyConstraints.FreezeRotationZ;
    }
}
