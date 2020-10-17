using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform target;

    Transform cam;

    void Awake()
    {
        cam = GetComponent<Transform>();
    }

    void Update()
    {
    }
}
