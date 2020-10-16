using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class P_First_Stat
{
    public const int F_Hp = 1;
    public const int F_Power = 1;
    public const int F_Agility = 1;
    public const int F_Speed = 2;
}

    public class Player_Stat : MonoBehaviour
{
    Game_State G_State;
    CreateMap Map;
    public Transform Player_Obj;

    public int P_hp;
    public int P_power;
    public int P_agility;
    public int P_speed;
    public float P_R, P_C;

    bool isSetPos=false;

    void Awake()
    {
        G_State = GameObject.Find("Game_Manager").GetComponent<Game_State>();
        Map = GameObject.Find("MapCreator").GetComponent<CreateMap>();
    }

    // Update is called once per frame
    void Update()
    {
        if(G_State.G_state==2)
        {
            // 게임 로드가 아닐경우 캐릭터 스탯 초기값 적용
            if(!G_State.isLoad)
            {
                Set_Player_Stat(P_First_Stat.F_Hp, P_First_Stat.F_Power, P_First_Stat.F_Agility, P_First_Stat.F_Speed);
                Set_Player_Pos();
                Spawn_Player(P_R, P_C);
            }

            G_State.G_state = 3;
        }
    }

    void Set_Player_Stat(int hp, int power , int agility , int speed)
    {
        P_hp = hp;
        P_power = power;
        P_agility = agility;
        P_speed = speed;
    }

    void Set_Player_Pos()
    {
        if (isSetPos == false)
        {
            for (int i = 0; i < Map.MaxCol; i++)
            {
                for (int j = 0; j < Map.MaxRow; j++)
                {
                    if (Map.Map[j, i] == Map_Type.Plain&&isSetPos==false)
                    {
                        P_R = j;
                        P_C = i;
                        isSetPos = true;
                    }
                }
            }
        }
    }

    void Spawn_Player(float r,float c)
    {
        if(P_R%2==0)
        {
            Instantiate(Player_Obj, new Vector3((45 * P_C), 10, -40 * P_R+1), Quaternion.identity);
        }
        else if(P_R%2==1)
        {
            Instantiate(Player_Obj, new Vector3((45 * P_C)-22, 10, -40 * P_R+1), Quaternion.identity);
        }
    }
}
