using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{


    Sprite Card_image;


    // Start is called before the first frame update
    void Start()
    {
        Card_image = gameObject.GetComponent<SpriteRenderer>().sprite;
        this.gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("ride_SEc");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
