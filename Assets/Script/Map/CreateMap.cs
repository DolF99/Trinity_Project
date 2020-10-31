using System.Collections;
using System.Collections.Generic;
using UnityEngine;


static class Map_Type
{
    public const int Non = 0; //빈 타일
    public const int Forest = 1; //숲
    public const int Lake = 2; // 호수
    public const int Mountain = 3; // 산
    public const int Chapel = 4; // 예배당
    public const int Cave = 5; // 산적 소굴
    public const int Arcademy = 6; // 학원
    public const int Village = 7; // 마을
    public const int Hunter = 8; // 사냥꾼
    public const int Dungeon = 9; // 던전
    public const int Dimension = 10; // 차원의 틈
    public const int Ruins= 11; // 유적
    public const int End = 12; // 엔딩
    public const int Plain = 13; //평지
    public const int V_Castle = 14; //마을 - 성
    public const int V_Blacksmith = 15; //마을 - 대장간
    public const int V_House = 16; // 마을 - 민간인의 집
    public const int V_Chapel = 17; // 마을 - 예배당
    public const int V_Arcademy = 18; // 마을 - 학원
    public const int V_Inn = 19; //마을 - 여관
    public const int V_Plain = 20; // 마을 - 평지
    public const int Volcano = 21; // 산 - 화산
    public const int Dungeon2 = 22; // 던전2
    public const int V_Plain2 = 23; // 마을 - 평지2
    public const int Plain_Volcano = 24; // 평지2 - 화산
    public const int Plain2 = 25; // 평지3
}

public class CreateMap : MonoBehaviour
{
    MapObj map_obj;
    Game_State G_State;
    public SetMapArr SetMap;
    public Transform[] Map_Obj;

    //맵 크기 받아올 변수
    public int MaxRow;
    public int MaxCol;

    //생성중인 맵의 위험도 저장하는 변수
    public int Danger = 1;

    //지형 개수
    int MaxType = 15;

    

    //만들어진 지형 카운트
    int create_cnt = 0;

    //맵이 완전히 생성되었을 경우 true
    bool isCreate = false;

    public bool isStart = false;

    bool isCreateInn = false;

    //만들어질 맵 배열
    public int[,] Map;
    //마을 생성에 필요한 중심타일 데이터만 있는  배열
    int[,] Map_Center;

    // 만들어질 지형의 최소값
    int[] Min_Create;
    // 최대값
    int[] Max_Create;
    // 만들어진 지형 개수
    int[] Now_Create;
    // 마을 내의 지형
    int[] Village_Create;
    int[] Set_Village_Create;

    void Awake()
    {
        G_State = GameObject.Find("Game_Manager").GetComponent<Game_State>();
        SetMap = GameObject.Find("MapCreator").GetComponent<SetMapArr>();
        Min_Create = new int[MaxType];
        Max_Create = new int[MaxType];
        Now_Create = new int[MaxType];
        Village_Create = new int[7];
        Set_Village_Create = new int[7];
    }

    void Update()
    {
        if (!isStart)
            isStart = SetMap.isReset;

        if (isStart)
        {
            if (!isCreate)
            {
                Change_Data();
                SetType();
                Spawn_Map();
                isCreate = true;
            }
        }
    }

    // 다른 스크립트에서 필요한 데이터를 받아오는 함수
    void Change_Data()
    {
        MaxRow = SetMap.MaxRow;
        MaxCol = SetMap.MaxCol;

        Map = new int[MaxRow, MaxCol];
        Map_Center = new int[MaxRow, MaxCol];

        Map_Center = SetMap.CenterArr;
    }

    //위험도에 따라 지형의 최소값, 최대값 변경 , 배열 초기화
    void Change_Min_Max()
    {
        //빈타일 ,숲 , 호수 , 산 , 예배당 , 산적 소굴 , 학원 , 마을 , 사냥꾼 , 던전 , 차원의 틈 , 유적 , 엔딩 , 평지
        if (Danger == 1)
        {
            Min_Create = new int[] { 0, 10, 3, 3, 2, 1, 1, 1, 0, 0, 0, 0, 0, 0 };
            Max_Create = new int[] { 0, 50, 18, 18, 8, 3, 10, 2, 0, 0, 0, 0, 0, 58 };
            // 마을 -  성 ,대장간 ,상점 ,예배당 ,학원, 여관 (14~ 19 )
            Village_Create = new int[] { 0, 0, 0, 0, 0 ,0};
            Set_Village_Create = new int[] { 0, 0, 0, 0, 0, 0, 0 };
        }
        else if (Danger == 2)
        {
            Min_Create = new int[] { 0, 15, 4, 4, 2, 3, 1, 1, 4, 1, 0, 0, 0, 0 };
            Max_Create = new int[] { 0, 55, 19, 19, 10, 15, 7, 2, 20, 7, 0, 0, 0, 64 };
            Village_Create = new int[] { 0, 0, 0, 0, 0, 0};
            Set_Village_Create = new int[] { 0, 0, 0, 0, 0, 0, 0 };
        }
        else if (Danger == 3)
        {
            Min_Create = new int[] { 0, 15, 2, 2, 1, 3, 1, 1, 4, 1, 0, 0, 0, 0 };
            Max_Create = new int[] { 0, 55, 10, 10, 5, 15, 3, 2, 20, 7, 0, 1, 0, 47 };
            Village_Create = new int[] { 0, 0, 0, 0, 0, 0};
            Set_Village_Create = new int[] { 0, 0, 0, 0, 0, 0, 0 };
        }
        else if (Danger == 4)
        {
            Min_Create = new int[] { 0, 0, 1, 1, 1, 1, 0, 0, 1, 4, 0, 0, 0, 1 };
            Max_Create = new int[] { 0, 32, 5, 5, 2, 5, 2, 1, 5, 20, 0, 2, 0, 5 };
            Village_Create = new int[] { 0, 0, 0, 0, 0, 0 };
            Set_Village_Create = new int[] { 0, 0, 0, 0, 0, 0, 0 };
        }
        else if (Danger == 5)
        {
            Min_Create = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0 };
            Max_Create = new int[] { 0, 20, 3, 2, 0, 0, 0, 0, 0, 0, 0, 0, 1, 3 };
        }
    }

    // 마을 개수 지정
    void Set_Village_Num()
    {
        // 마을 추가 생성 확률 0.7%
        int rand = Random.Range(0, 1000);

        if (rand < 7)
        {
            if (Danger <= 3)
            {
                Min_Create[Map_Type.Village] = 2;
                // 마을은 7개의 타을을 하나로 묶어 생성하기때문에 2개 마을 = 14 타일
                create_cnt += 14;
            }
            else if (Danger == 4)
            {
                Min_Create[Map_Type.Village] = 1;
                create_cnt += 7;
            }
        }
        else if (rand >= 7 && Danger <= 3)
        {
            Min_Create[Map_Type.Village] = 1;
            create_cnt += 7;
        }

    }

    // 마을 타일 지형 지정
    void Set_Village_Type()
    {

        if (!isCreateInn)
        {
            int rand_inn = Random.Range(0, 7);
            //여관
            Set_Village_Create[rand_inn] = Map_Type.V_Inn;
            isCreateInn = true;
        }

        int t = 0;
        for (int i = 0; i < 7; i++)
        {
            int rand_type;
            bool isCreate = false;

            while (!isCreate)
            {
                rand_type = Random.Range(0, 100);

                //성
                if (rand_type < 8)
                    t = 0;
                //대장간
                else if (rand_type >= 8 && rand_type < 23)
                    t = 1;
                //민간인의 집
                else if (rand_type >= 23 && rand_type < 33)
                    t = 2;
                //예배당
                else if (rand_type >= 33 && rand_type < 43)
                    t = 3;
                //학원
                else if (rand_type >= 43 && rand_type < 53)
                    t = 4;
                else
                {
                    t = 5;
                    if (Set_Village_Create[i] == 0) 
                    Set_Village_Create[i] = Map_Type.V_Plain;
                    isCreate = true;
                }

                if (t <= 4)
                {
                    if (Village_Create[t] == 0 && Set_Village_Create[i] == 0)
                    {
                        Set_Village_Create[i] = 14 + t;
                        Village_Create[t]++;
                        isCreate = true;
                    }
                }
            }
        }
    }


    // 확률에따라 지형 개수 지정
    // 위험도별 최대 생성 개수 + 개별 확률 1% = 100 으로 계산
    void RandomCreate(int max, int Fo, int La, int Mo, int Ch, int Ca, int Ar, int Vi, int Hu, int Du, int Ru, int Pl)
    {
        int rand;

        while (create_cnt < max)
        {
            rand = Random.Range(0, 10000);

            // 숲
            if (rand < Fo)
                AddCreateNum(Map_Type.Forest, 1);

            // 호수
            else if (rand >= Fo && rand < Fo + La)
                AddCreateNum(Map_Type.Lake, 1);

            // 산 
            else if (rand >= Fo + La && rand < Fo + La + Mo)
                AddCreateNum(Map_Type.Mountain, 1);

            // 예배당
            else if (rand >= Fo + La + Mo && rand < Fo + La + Mo + Ch)
                AddCreateNum(Map_Type.Chapel, 1);

            // 산적 소굴 
            else if (rand >= Fo + La + Mo + Ch && rand < Fo + La + Mo + Ch + Ca)
                AddCreateNum(Map_Type.Cave, 1);

            // 학원
            else if (rand >= Fo + La + Mo + Ch + Ca && rand < Fo + La + Mo + Ch + Ca + Ar)
                AddCreateNum(Map_Type.Arcademy, 1);

            // 마을 - 마을은 다른함수에서 가장 먼저 마을 수를 지정해주었음. 
            else if (rand >= Fo + La + Mo + Ch + Ca + Ar && rand < Fo + La + Mo + Ch + Ca + Ar + Vi) { }

            //사냥꾼
            else if (rand >= Fo + La + Mo + Ch + Ca + Ar + Vi && rand < Fo + La + Mo + Ch + Ca + Ar + Vi + Hu)
                AddCreateNum(Map_Type.Hunter, 1);

            // 던전
            else if (rand >= Fo + La + Mo + Ch + Ca + Ar + Vi + Hu && rand < Fo + La + Mo + Ch + Ca + Ar + Vi + Hu + Du)
                AddCreateNum(Map_Type.Dungeon, 1);

            // 유적
            else if (rand >= Fo + La + Mo + Ch + Ca + Ar + Vi + Hu + Du  && rand < Fo + La + Mo + Ch + Ca + Ar + Vi + Hu + Du  + Ru)
                AddCreateNum(Map_Type.Ruins, 1);


            // 평지
            else if (rand >= Fo + La + Mo + Ch + Ca + Ar + Vi + Hu + Du  + Ru && rand < Fo + La + Mo + Ch + Ca + Ar + Vi + Hu + Du  + Ru + Pl)
                AddCreateNum(Map_Type.Plain, 1);

        }

    }

    //생성 최소치 , 최대치 비교후 최소치 증가
    void AddCreateNum(int type, int num)
    {
        if (Min_Create[type] < Max_Create[type])
        {
            Min_Create[type] += num;
            create_cnt++;
        }
    }

    //맵 지형 지정
    void SetType()
    {
            while (Danger <= 5)
        {
            // 위험도에 따른 지형 최소,최대 개수 지정
            Change_Min_Max();
            
            // 위험도 3단계 이하 일경우 마을 존재
            if (Danger <= 4)
                Set_Village_Num();

            if (Danger == 1)
                RandomCreate(84, 3000, 1000, 1000, 500, 200, 800, 70, 0, 0, 0, 3430);

            if (Danger == 2)
                RandomCreate(105, 3000, 1000, 1000, 500, 800, 400, 70, 1000, 0, 0, 2230);

            if (Danger == 3)
                RandomCreate(84, 4500, 500, 500, 400, 1000, 200, 70, 1200, 200, 10, 1420);

            if (Danger == 4)
                RandomCreate(42, 7400, 400, 200, 100, 400, 10, 70, 500, 500, 20, 400);

            if (Danger == 5)
                RandomCreate(20, 8200, 400, 1000, 0, 0, 0, 0, 0, 0, 0, 400);

            ChangeTile();
            Set();
            create_cnt = 0;
            Danger++;
            isCreateInn = false;
        }
    }



    void ChangeTile()
    {
        Set_Village_Type();

        for (int i = 0; i < Min_Create[Map_Type.Village]; i++)
        {
            int randR = 0, randC = 0;

            bool isCreate = false;

            while (!isCreate)
            {
                
                randR = Random.Range(2, MaxRow - 2);
                randC = Random.Range(4, MaxCol - 4);

                if (Map_Center[randR, randC] == Danger)
                {

                    Map[randR, randC] = Map_Type.Village;

                    // 행 짝수
                    if (randR % 2 == 0)
                    {
                            Map[randR, randC + 1] = Set_Village_Create[0];
                            Map[randR, randC - 1] = Set_Village_Create[1];
                            Map[randR - 1, randC ] = Set_Village_Create[2];
                            Map[randR - 1, randC+1] = Set_Village_Create[3];
                            Map[randR + 1, randC ] = Set_Village_Create[4];
                            Map[randR + 1, randC+1] = Set_Village_Create[5];
                    }
                    // 행 홀수
                    else if (randR % 2 == 1)
                    {
                        Map[randR, randC + 1] = Set_Village_Create[0];
                        Map[randR, randC - 1] = Set_Village_Create[1];
                        Map[randR - 1, randC - 1] = Set_Village_Create[2];
                        Map[randR - 1, randC] = Set_Village_Create[3];
                        Map[randR + 1, randC - 1] = Set_Village_Create[4];
                        Map[randR + 1, randC] = Set_Village_Create[5];
                    }

                    isCreate = true;
                }
            }

            Map[randR, randC] = Set_Village_Create[6];
        }
    }


    void Set()
    {
        //Map 배열에 각 타일마다 지형 데이터 삽입
        for (int i = 0; i < MaxRow; i++)
        {
            for (int j = 0; j < MaxCol; j++)
            {
                if (SetMap.DangerArr[i, j] == Danger && Map[i, j] == 0)
                {

                    bool isSet = false;
                    while (!isSet)
                    {
                        int rand;

                            rand = Random.Range(1, 14);

                            if (rand != Map_Type.Village &&
                                Now_Create[rand] < Min_Create[rand])
                            {
                                Map[i, j] = rand;
                                Now_Create[rand]++;

                                isSet = true;
                            }
                        //}
                    }
                
                }


            }
        }

        for (int k = 0; k < MaxType; k++)
        {
            Now_Create[k] = 0;
        }
    }


    void Spawn_Map()
    {
        for (int i = 0; i < MaxRow; i++)
        {
            for (int j = 0; j < MaxCol; j++)
            {
                if (Map[i, j] == Map_Type.Mountain)
                {
                    int a = Random.Range(0, 2);
                    if (a == 0)
                        Map[i, j] = Map_Type.Volcano;
                }

                if (Map[i, j] == Map_Type.Dungeon)
                {
                    int a = Random.Range(0, 2);
                    if (a == 0)
                    {
                        Map[i, j] = Map_Type.Dungeon2;
                    }
                }

                if (Map[i, j] == Map_Type.V_Plain)
                {
                    int a = Random.Range(0, 2);
                    if (a == 0)
                    {
                        Map[i, j] = Map_Type.V_Plain2;
                    }
                }

                if (i % 2 == 0 && Map[i, j] > 0&&Map[i,j]!=Map_Type.Plain)
                {
                    Instantiate(Map_Obj[Map[i, j] - 1], new Vector3((41 * j), 0, -35f * i), Quaternion.identity);
                }
                if (i % 2 == 1 && Map[i, j] > 0 && Map[i, j] != Map_Type.Plain)
                    Instantiate(Map_Obj[Map[i, j] - 1], new Vector3((41 * j) - 20, 0, -35f * i), Quaternion.identity);
            }
        }

        for (int i = 0; i < MaxRow; i++)
        {
            for (int j = 0; j < MaxCol; j++)
            {
                if (i % 2 == 0 && Map[i, j] == Map_Type.Plain)
                {
                    if (Map[i, j + 1] == Map_Type.Volcano)
                        Map[i, j] = Map_Type.Plain_Volcano;
                    else if (Map[i, j - 1] == Map_Type.Volcano)
                        Map[i, j] = Map_Type.Plain_Volcano;
                    else if (Map[i - 1, j] == Map_Type.Volcano)
                        Map[i, j] = Map_Type.Plain_Volcano;
                    else if (Map[i - 1, j + 1] == Map_Type.Volcano)
                        Map[i, j] = Map_Type.Plain_Volcano;
                    else if (Map[i + 1, j] == Map_Type.Volcano)
                        Map[i, j] = Map_Type.Plain_Volcano;
                    else if (Map[i + 1, j + 1] == Map_Type.Volcano)
                        Map[i, j] = Map_Type.Plain_Volcano;
                    else
                    {
                        int a = Random.Range(0, 2);
                        if(a==0)
                            Map[i, j] = Map_Type.Plain2;
                    }

                    Instantiate(Map_Obj[Map[i, j] - 1], new Vector3((41 * j), 0, -35 * i), Quaternion.identity);
                }
                else if (i % 2 == 1 && Map[i, j] == Map_Type.Plain)
                {
                    if (Map[i, j + 1] == Map_Type.Volcano)
                        Map[i, j] = Map_Type.Plain_Volcano;
                    else if (Map[i, j - 1] == Map_Type.Volcano)
                        Map[i, j] = Map_Type.Plain_Volcano;
                    else if (Map[i - 1, j - 1] == Map_Type.Volcano)
                        Map[i, j] = Map_Type.Plain_Volcano;
                    else if (Map[i - 1, j] == Map_Type.Volcano)
                        Map[i, j] = Map_Type.Plain_Volcano;
                    else if (Map[i + 1, j - 1] == Map_Type.Volcano)
                        Map[i, j] = Map_Type.Plain_Volcano;
                    else if (Map[i + 1, j] == Map_Type.Volcano)
                        Map[i, j] = Map_Type.Plain_Volcano;
                    else
                    {
                        int a = Random.Range(0, 2);
                        if (a == 0)
                            Map[i, j] = Map_Type.Plain2;
                    }

                    Instantiate(Map_Obj[Map[i, j] - 1], new Vector3((41 * j) - 20, 0, -35 * i), Quaternion.identity);
                }
            }
        }
    }
}
