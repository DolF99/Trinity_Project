using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform target;
    bool isFindTarget = false;
    
    public float p_x = 0f;
    public float p_y = 200f;
    public float p_z = 0f;

    Vector3 CameraPos;

    void Awake()
    {
    }

    void Update()
    {
        if (!isFindTarget)
        {
            target = GameObject.Find("Player(Clone)").GetComponent<Transform>();
            isFindTarget = true;
        }
        else
        {
            CameraPos.x = target.transform.position.x + p_x;
            CameraPos.y = target.transform.position.y + p_y;
            CameraPos.z = target.transform.position.z + p_z;

            transform.position = CameraPos;
        }
    }
}
