using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
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
