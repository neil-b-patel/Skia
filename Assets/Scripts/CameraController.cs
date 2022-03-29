//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 offset = new Vector3(0, 0, -15);
    private Transform player;

    void Start()
    {
        player = GameObject.Find("Light Player").transform;
    }

    void LateUpdate()
    {
        transform.position = player.TransformPoint(offset);
    }
}
