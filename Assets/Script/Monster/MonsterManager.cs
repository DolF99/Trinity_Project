using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public SetMapArr Map;
    
    //public GameObject Monster_Model;
    public Monster[] Mon;    
    public Vector3[] MonPosArr;
    public int MaxMonster;
    public int PosCnt = 0;

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

    bool cnt = true;

    void Awake()
    {
        MaxMonster = D1_Monster_max + D2_Monster_max + D3_Monster_max + D4_Monster_max + D5_Monster_max;
        
        Mon = new Monster[MaxMonster];
        for (int i = 0; i < MaxMonster; i++)
        {
            Mon[i] = GameObject.Find("Monster_Manager").GetComponent<Monster>();
        }
        MonPosArr = new Vector3[MaxMonster];
        
    }

    void LateUpdate()
    {
        if (cnt)
        {
            Debug.Log("Update");
            
            Set_Monster();
            Spawn_Monster();
            cnt = false;
        }
    }
    void Set_Monster()
    {
        Debug.Log("Set_Monster");
        for (int i = 5; i < Map.MaxCol; i++)
        {
            for (int j = 2; j < Map.MaxRow; j++)
            {
                if (PosCnt != MaxMonster)
                {
                    if (Map.DangerArr[j, i] == 0)
                    {
                        if (D1_Monster_cnt == D1_Monster_max)
                            break;
                        Debug.Log("1");
                        //MonsterArr[i, j] = 1;
                        D1_Monster_cnt++;
                        MonPosArr[PosCnt].x = i;
                        MonPosArr[PosCnt].z = j;
                        PosCnt++;
                    }
                    else if(Map.DangerArr[j, i] == 2)
                    {
                        if (D2_Monster_cnt == D2_Monster_max)
                            break;
                        Debug.Log("2");
                        // MonsterArr[i, j] = 1;
                        D2_Monster_cnt++;
                        MonPosArr[PosCnt].x = i;
                        MonPosArr[PosCnt].z = j;
                        PosCnt++;
                    }
                    else if (Map.DangerArr[j, i] == 3)
                    {
                        if (D3_Monster_cnt == D3_Monster_max)
                            break;
                        Debug.Log("3");
                        // MonsterArr[i, j] = 1;
                        D3_Monster_cnt++;
                        MonPosArr[PosCnt].x = i;
                        MonPosArr[PosCnt].z = j;
                        PosCnt++;
                    }
                    else if (Map.DangerArr[j, i] == 4)
                    {
                        if (D4_Monster_cnt == D4_Monster_max)
                            break;
                        Debug.Log("4");
                        // MonsterArr[i, j] = 1;
                        D4_Monster_cnt++;
                        MonPosArr[PosCnt].x = i;
                        MonPosArr[PosCnt].z = j;
                        PosCnt++;
                    }
                    else if (Map.DangerArr[j, i] == 5)
                    {
                        if (D5_Monster_cnt == D5_Monster_max)
                            break;
                        Debug.Log("5");
                        // MonsterArr[i, j] = 1;
                        D5_Monster_cnt++;
                        MonPosArr[PosCnt].x = i;
                        MonPosArr[PosCnt].z = j;
                        PosCnt++;
                    }
                }
            }
        }
    }

    void Spawn_Monster()
    {
        //Set_Monster();
        for (int i = 0; i < MaxMonster; i++)
        {
            if (MonPosArr[i].z % 2 == 0)
                MonPosArr[i].x = MonPosArr[i].x * 41;
            else if(MonPosArr[i].z % 2 == 1)
                MonPosArr[i].x = (MonPosArr[i].x * 41) - 20;

            MonPosArr[i].y = 10;
            MonPosArr[i].z = MonPosArr[i].z * -35;
            Instantiate(Mon[i].Monster_obj, MonPosArr[i], Quaternion.identity);
        }
    }
}

