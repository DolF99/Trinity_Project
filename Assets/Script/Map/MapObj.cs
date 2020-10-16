using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapObj : MonoBehaviour
{
    Player_Move P_Move;
    Player_Stat P_Stat;
    public float M_R, M_C;

    void Awake()
    {
        P_Move = GameObject.Find("Player_Manager").GetComponent<Player_Move>();
        P_Stat = GameObject.Find("Player_Manager").GetComponent<Player_Stat>();
    }
    
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        if (P_Move.isMove == false)
        {
            SetPos();
        }
    }

    void SetPos()
    {
        P_Move.MoveC = transform.position.x;
        P_Move.MoveR = transform.position.z;

        if(P_Move.MoveC%45==0)
        {
            M_R = (P_Move.MoveR-1) / (-40);
            M_C = (P_Move.MoveC) / 45;
        }
        else
        {
            M_R = (P_Move.MoveR - 1) / (-40);
            M_C = (P_Move.MoveC+22) / 45;
        }

        float r, c;
        r = M_R - P_Stat.P_R;
        c = M_C - P_Stat.P_C;

        if (P_Stat.P_R%2==0)
        {
            if ((r == 0 && c == 1) ||
                (r == 0 && c == -1) ||
                (r == -1 && c == 0) ||
                (r == -1 && c == 1) ||
                (r == 1 && c == 0) ||
                (r == 1 && c == 1))
            {
                P_Move.isMove = true;
                P_Stat.P_R += r;
                P_Stat.P_C += c;
            }
        }

        else if (P_Stat.P_R%2==1)
        {
            if ((r == 0 && c == 1) ||
               (r == 0 && c == -1) ||
               (r == -1 && c == -1) ||
               (r == -1 && c == 0) ||
               (r == 1 && c == -1) ||
               (r == 1 && c == 0))
            {
                P_Move.isMove = true;
                P_Stat.P_R += r;
                P_Stat.P_C += c;
            }
        }
    }
}
