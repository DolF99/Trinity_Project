using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_State : MonoBehaviour
{
    // 게임 상태를 받는 변수 
    // 0= 게임 시작 직후 1 = 맵 생성 , 2 = 캐릭터 세팅 및 기타 작업 , 3 = 플레이 , 4 = 일시 정지 , 5 = 게임 종료
    public int G_state = 0;

    // 플레이어의 행동 상태를 받는 변수
    // 1. 카드 선택 , 2. 이동 , 3. 전투
    public int Player_State = 2;

    public bool isLoad;
    public bool MapCreateStart = false;
    float time;

    void Awake()
    {
        
    }
    
    void Update()
    {
        if (G_state == 0)
        {
            G_state = 1;
        }
        else if (G_state == 1)
        {
            MapCreateStart = true;
        }
        else if (G_state == 2)
        {

        }
        else if (G_state == 3)
        {

        }
        else if (G_state == 4)
        {

        }
        else if (G_state == 5) 
        {
            
        }
        
    }
}
