using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class M_First_Stat
{
    public const int F_Hp = 1;
    public const int F_Power = 1;
    public const int F_Agility = 1;
    public const int F_Speed = 2;
}

public class Monster : MonoBehaviour
{
    public float M_R, M_C;
    public int M_hp;
    public int M_power;
    public int M_agility;
    public int M_speed;

    public Object Monster_obj;

    void Start()
    {
        Set_Monster_Stat(M_First_Stat.F_Hp, M_First_Stat.F_Power, M_First_Stat.F_Agility, M_First_Stat.F_Speed);
    }

    void Set_Monster_Stat(int hp, int power, int agility, int speed)
    {
        M_hp = hp;
        M_power = power;
        M_agility = agility;
        M_speed = speed;
    }
}
