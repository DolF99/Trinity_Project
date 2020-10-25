using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Status : MonoBehaviour
{
    Player_Stat p_stat;

    public Text hp;
    public Text power;
    public Text agility;
    public Text speed;
    public GameObject Status_Menu;
    public bool isClick = false;

    public bool isShow = false;
    // Start is called before the first frame update
    void Start()
    {
        p_stat = GameObject.Find("Player_Manager").GetComponent<Player_Stat>();
        hp = GameObject.Find("Hp").GetComponent<Text>();
        power = GameObject.Find("Power").GetComponent<Text>();
        agility = GameObject.Find("Agility").GetComponent<Text>();
        speed = GameObject.Find("Speed").GetComponent<Text>();
        Status_Menu = GameObject.Find("Status");

        Status_Menu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    
    public void Click_Status()
    {

        if (!isClick)
        {
            Debug.Log("스탯 클릭");
            Status_Menu.SetActive(true);
            hp.text = "HP : " + p_stat.P_hp;
            power.text = "공격력 : " + p_stat.P_power;
            agility.text = "민첩 : " + p_stat.P_agility;
            speed.text = "스피드 : " + p_stat.P_speed;
            isClick = true;
        }
        else
        {
            Status_Menu.SetActive(false);
            isClick = false;
        }
    }

    public void Click_Status_Exit()
    {
        Debug.Log("닫기 클릭");
        Status_Menu.SetActive(false);
        isClick = false;
    } 
    
}
