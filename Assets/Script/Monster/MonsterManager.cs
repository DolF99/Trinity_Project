using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    GameObject Player;
    SetMapArr Map;
    Game_State GM;
    public Monster[] Mon;
    public int MaxMonster;
    public int MonCnt = 0;
    public Object Monster_obj;

    int D1_Monster_max = 1;
    int D2_Monster_max = 1;
    int D3_Monster_max = 1;
    int D4_Monster_max = 1;
    int D5_Monster_max = 1;

    int D1_Monster_cnt = 0;
    int D2_Monster_cnt = 0;
    int D3_Monster_cnt = 0;
    int D4_Monster_cnt = 0;
    int D5_Monster_cnt = 0;

    void Awake()
    {
        MaxMonster = D1_Monster_max + D2_Monster_max + D3_Monster_max + D4_Monster_max + D5_Monster_max;

        GM = GameObject.Find("Game_Manager").GetComponent<Game_State>();
        
        Map = GameObject.Find("MapCreator").GetComponent<SetMapArr>();

        Mon = new Monster[MaxMonster];
        for (int i = 0; i < MaxMonster; i++)
            Mon[i] = new Monster();
    }

    public void Set_Monster()
    {
        for (int i = 5; i < Map.MaxCol; i++)
            for (int j = 2; j < Map.MaxRow; j++)
                if (MonCnt != MaxMonster)
                {
                    int rand = Random.Range(0, 99);

                    if (rand < 10)
                        switch (Map.DangerArr[j, i])
                        {
                            case 1:
                                if (D1_Monster_cnt == D1_Monster_max)
                                    break;
                                D1_Monster_cnt++;
                                Mon[MonCnt++].Pos = new Vector3(i, 10, j);
                                break;

                            case 2:
                                if (D2_Monster_cnt == D2_Monster_max)
                                    break;
                                D2_Monster_cnt++;
                                Mon[MonCnt++].Pos = new Vector3(i, 10, j);
                                break;

                            case 3:
                                if (D3_Monster_cnt == D3_Monster_max)
                                    break;
                                D3_Monster_cnt++;
                                Mon[MonCnt++].Pos = new Vector3(i, 10, j);
                                break;

                            case 4:
                                if (D4_Monster_cnt == D4_Monster_max)
                                    break;
                                D4_Monster_cnt++;
                                Mon[MonCnt++].Pos = new Vector3(i, 10, j);
                                break;

                            case 5:
                                if (D5_Monster_cnt == D5_Monster_max)
                                    break;
                                D5_Monster_cnt++;
                                Mon[MonCnt++].Pos = new Vector3(i, 10, j);
                                break;
                        }
                }
    }

    public void Spawn_Monster()
    {
        Set_Monster();
        for (int i = 0; i < MaxMonster; i++)
        {
            if (Mon[i].Pos.z % 2 == 0)
            {
                Instantiate(Monster_obj, new Vector3(Mon[i].Pos.x * 41, Mon[i].Pos.y, Mon[i].Pos.z * -35), Quaternion.identity);
                Mon[i].Pos2 = new Vector3(Mon[i].Pos.x * 41, Mon[i].Pos.y, Mon[i].Pos.z * -35);
            }
            else if (Mon[i].Pos.z % 2 == 1)
            {
                Instantiate(Monster_obj, new Vector3((Mon[i].Pos.x * 41) - 20, Mon[i].Pos.y, Mon[i].Pos.z * -35), Quaternion.identity);
                Mon[i].Pos2 = new Vector3((Mon[i].Pos.x * 41) - 20, Mon[i].Pos.y, Mon[i].Pos.z * -35);
            }
            
        }
    }

    public void Attack_Monster()
    {
        //for(int i = 0; i < MaxMonster; i++)
        //if(Mon[i].Player_Search)
        //{
        //        //기습ㄱ
        //}
    }

    public void Searching_Manager()
    {
        if (Player != GameObject.Find("Player(Clone)"))
            Player = GameObject.Find("Player(Clone)");
        for (int i = 0; i < MaxMonster; i++)
            Mon[i].Searching(Player.transform.position, Mon[i].Pos2);
    }
}

