using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Card_Ctrl : MonoBehaviour
{
    private Vector3 ScreenSpace;
    private Vector3 offset;
    Vector3 Toglle;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        //Toglle = this.transform.position;
        Debug.Log("zmffl");
        ScreenSpace = Camera.main.WorldToScreenPoint(transform.position);
        offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, ScreenSpace.z));
    }
    private void OnMouseDrag()
    {
        var ClickPosi = new Vector3(Input.mousePosition.x, Input.mousePosition.y, ScreenSpace.z);
        var CurPositon = Camera.main.ScreenToWorldPoint(ClickPosi) + offset;
        transform.position = CurPositon;
        Debug.Log("snfmsmswnd");
    }
    private void OnMouseUp()
    {
        Debug.Log("thsEp");
    }
}
