using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UIElements;

public class Card : MonoBehaviour
{

    Vector3 TempPosi;
    Vector3 ttEMP;
    bool Clicking = false;
    Vector3 TempPos1i;  
    GameObject BackObj;
    Material nomal;
    Material Pick;

    // Start is called before the first frame update
    void Start()
    {
        BackObj = GameObject.Find("Cube");
        nomal = Resources.Load("New Material",typeof(Material)) as Material;
        Pick = Resources.Load("New Material 1", typeof(Material)) as Material;
        TempPos1i = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
        
    }
    
// Update is called once per frame
void Update()
    {
        TempPosi = this.gameObject.transform.position;
        ttEMP = TempPosi;
        Vector3 moutemp = Input.mousePosition;
        moutemp.z = +4;
       //  Debug.Log(Input.mousePosition+"스크린");
        
        Vector3 MCWT = Camera.main.ScreenToWorldPoint(moutemp);
       // Debug.Log(this.gameObject.transform.position+"물체");
        //Debug.Log(MCWT +"월드");
        if((Input.GetMouseButton(0)||Input.GetMouseButton(0))&& TempPosi.x >= MCWT.x && TempPosi.x - 1 <= MCWT.x && TempPosi.y <= MCWT.y && TempPosi.y + 1 >= MCWT.y)
        {
            this.gameObject.transform.position = MCWT;
            BackObj.GetComponent<MeshRenderer>().material = Pick;
            Debug.Log("클릭중");
            Clicking = true;
        }
        if(Clicking==true&& moutemp.y > Screen.height / 2&& Input.GetMouseButton(0)==false)
        {
            
            Debug.Log("카드발사");
            Destroy(this.gameObject);
        }
        else if (Input.GetMouseButtonUp(0) && Clicking == true)
        {
            BackObj.GetComponent<MeshRenderer>().material = nomal;
            this.gameObject.transform.position = TempPos1i;
            Debug.Log("손뗌");
            Clicking = false;
        }

    }
    void ClickBtn(Vector3 Cdvector)
    {
        Cdvector.x = Cdvector.x - 0.2f;
        Cdvector.y = Cdvector.y + 0.2f;
        Cdvector.z = Cdvector.z - 1.0f;
    }
    bool isColl(Vector3 TempPosi, Vector3 MCWT)
    {
        if (TempPosi.x >= MCWT.x && TempPosi.x- 1 <= MCWT.x && TempPosi.y <= MCWT.y && TempPosi.y +1 >= MCWT.y)
        {
            return true;
        }
        else
            return false;
    }
}
