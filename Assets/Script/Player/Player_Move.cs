using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    Game_State G_State;
    Player_Stat P_Stat;
    GameObject Player;
    float speed=50f;
    public bool isMove=false;
    public bool isGetPos=false;

    public float MoveR, MoveC;

    void Awake()
    {
        G_State = GameObject.Find("Game_Manager").GetComponent<Game_State>();
        P_Stat = GameObject.Find("Player_Manager").GetComponent<Player_Stat>();
    }
    
    void Update()
    {
        if (G_State.G_state == 3 && G_State.Player_State == 2) 
        {
            MovePlayer();
        }
    }

    void MovePlayer()
    {
        if(isMove)
        {
            if (Player != GameObject.Find("Player(Clone)"))
            Player = GameObject.Find("Player(Clone)");

            Player.transform.position = Vector3.MoveTowards(Player.transform.position, new Vector3(MoveC, 10, MoveR), speed * Time.deltaTime);
            if(Player.transform.position==new Vector3(MoveC,10,MoveR))
            {
                isMove = false;
            }
        }
    }
    
}
