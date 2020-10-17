using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsBack : MonoBehaviour
{
    public Texture2D BackGrd;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnGUI()
    {
        //GUI.DrawTexture(new Rect(0, Screen.height/3*2,Screen.width, Screen.height/3), BackGrd);
        if(GUI.Button(new Rect(Screen.width / 4 *3, Screen.height / 4 * 3, 64, 64), "Draw"))
        {
            Debug.Log("Draw");
        }
    }
}
