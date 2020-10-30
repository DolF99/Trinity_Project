using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Scenes_UI : MonoBehaviour
{

    public void onSingleBtn()
    {
        Application.LoadLevel("SingleMode");
    }
    public void onMultiBtn()
    {
        //Application.LoadLevel("");
        Debug.Log("멀티");
    }
    public void onExitBtn()
    {
        //Application.LoadLevel("");
        Debug.Log("종료");
    }

}
