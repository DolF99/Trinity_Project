using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMapArr : MonoBehaviour
{

    Game_State G_State;

    // 육각형 타일 오브젝트
    public GameObject MapObj;

    public GameObject CenterObj;

     GameObject D1, D2, D3, D4, D5;

    // 맵 위치가 지정 되었음을 알리는 변수
    public bool isReset = false; 
    // 하나의 좌표를 랜덤으로 받아올때 지정이 가능한 좌표일시 true로 전환하여 while문에서 빠져나옴
    bool isCreate = false; 

    // 좌표가 생성된 개수를 카운트하는 변수. 48개의 좌표를 확인하기 위함
    int ArrNum;
    // 육각형에 번호를 부여하기 위한 변수 ( 맵의 위험도 설정에 쓰일 예정)
    int CreateNum;

    // 맵의 최대 범위 값이 클수록 맵이 길게 늘려질 확률이 Up
    public int MaxRow = 300; // 세로
    public int MaxCol = 300; // 가로

    // 위험도 5 타일 카운터를 위한 변수
    int Tile_D_5 = 0;
    int Tile_Place_R = 0;
    int Tile_Place_C = 0;

    public int BossR,BossC;

    // 맵이 생성된 순서를 저장하는 배열 
    public int[,] arr;
    // arr배열을 기반으로 각 타일에 어떤 지형이 생성될지 정하기 위한 배열
    public int[,] MapArr;
    // 각 타일의 위험도를 저장하는 배열
    public int[,] DangerArr;
    // 위험도 중간 타일의 데이터를 저장하는 배열 > 마을 생성에 필요한 배열
    public int[,] CenterArr;



    public void Awake()
    {
        G_State = GameObject.Find("Game_Manager").GetComponent<Game_State>();
        MapObj = GameObject.Find("Field");
        CenterObj = GameObject.Find("Center");
        D1 = GameObject.Find("D1");
        D2 = GameObject.Find("D2");
        D3 = GameObject.Find("D3");
        D4 = GameObject.Find("D4");
        D5 = GameObject.Find("D5");

        // 배열을 입력한 범위로 초기화
        arr = new int[MaxRow, MaxCol];
        MapArr = new int[MaxRow, MaxCol];
        DangerArr = new int[MaxRow, MaxCol];
        CenterArr = new int[MaxRow, MaxCol];
    }


    public void Update()
    {
        // 맵생성 단계에서 한번만 실행
        if (G_State.G_state==1)
        {
            //첫 타일 좌표 지정
            SelectFirstPos(MaxRow/2, 5);
            //배열 랜덤 위치 지정
            SetArr();
            //각 타일의 모음의 중심타일에 번호 지정
            SetMapNum();
            //번호가 매겨진 배열을 이용하여 위험도 배열에 각각 위험도 지정
            SetDanger();
            //위험도에 따라 랜덤한 지형을 생성하기위해 배열을 복사
            MapArr = DangerArr;
            //중심타일 근처에 6개의 타일을 생성하기위한 좌표 설정
            ChangeArr(arr,2,0); // 전체 맵 형태
            ChangeArr(DangerArr, 0, 1); // 위험도
            //맵생성이 완료됨을 알림. 다음 단계로 이동
            G_State.G_state = 2;
            isReset = true;
        }
    }

    // 첫 타일 좌표 지정
    public void SelectFirstPos(int row, int col)
    {
        arr[row, col] = 1;
        ArrNum = 1;
        CreateNum = 3;
    }

    // 배열 랜덤 위치 지정
    public void SetArr()
    {
        //47번 반복 - 처음 위치는 이미 저장되었기때문에 47번을 반복한다.
        while (ArrNum < 48)
        {
            // 반복 될때마다 false로 초기화하여 바로 빠져나오는 일이 없도록 설정
            isCreate = false; 

            while(isCreate==false)
            {
                // 랜덤 행렬 좌표 받아옴
                int randR = Random.Range(2, MaxRow-2);
                int randC = Random.Range(4, MaxCol-4);

                //행이 홀수인지 짝수인지 판단하여 지정된 좌표의 근처에 생성된 타일의 좌표가 있는지 확인 후
                //존재할시 좌표에 1을 저장하여 생성됨을 확인.
                if (randR % 2 == 1)
                {
                    if (arr[randR + 1, randC - 3] != 0 && arr[randR, randC] == 0)
                    {
                        ArrNum++;
                        arr[randR, randC] = 1;
                        isCreate = true;
                    }
                    if (arr[randR - 2, randC - 2] != 0 && arr[randR, randC] == 0)
                    {
                        ArrNum++;
                        arr[randR, randC] = 1;
                        isCreate = true;
                    }
                }
                if (randR % 2 == 0)
                {
                    if (arr[randR + 1, randC - 2] != 0 && arr[randR, randC] == 0)
                    {
                        ArrNum++;
                        arr[randR, randC] = 1;
                        isCreate = true;
                    }
                    if (arr[randR - 2, randC - 2] != 0 && arr[randR, randC] == 0)
                    {
                        ArrNum++;
                        arr[randR, randC] = 1;
                        isCreate = true;
                    }
                }
            }

        }
    }

    //각 타일의 모음의 중심타일에 번호 지정
    public void SetMapNum()
    {
        for(int i=0;i<MaxCol;i++)
        {
            for(int j=0;j<MaxRow;j++)
            {
                if(arr[j,i]!=0)
                {
                    // 1열의 모든 행을 검사하여 번호 지정후 다음 열 검사
                    // 번호는 3번부터 지정 , 1은 중심 타일이 생성될때 생성됨을 확인할때 사용, 
                    // 2는 중심 근처 타일이 생성됨을 확인할때 사용
                    arr[j, i] = CreateNum; 
                    CreateNum++;
                }
            }
        }
    }

    // 중심타일 근처에 6개의 타일을 생성하기위한 좌표 설정
    public void ChangeArr(int[,] arr_,int ResNum,int type)
    {
        for (int i = 0; i < MaxRow; i++)
        {
            for (int j = 0; j < MaxCol; j++)
            {
                //배열 (i,j) 좌표에 중심타일이 존재할 경우
               if(arr[i,j]>2)
                {
                    // 육각형 타일의 생성위치는 행이 홀수냐 짝수냐에 따라 위치가 조금씩 다름.
                    // type 0일때 중심 타일 근처 6개의 타일의 좌표를 2로 저장.
                    // type 1일때 중심 타일의 값을 근처 타일에 복사
                    if (i%2 == 0)
                    {
                        if (type == 0)
                        {
                            arr_[i, j + 1] = ResNum;
                            arr_[i, j - 1] = ResNum;
                            arr_[i - 1, j] = ResNum;
                            arr_[i - 1, j + 1] = ResNum;
                            arr_[i + 1, j] = ResNum;
                            arr_[i + 1, j + 1] = ResNum;
                        }
                        else if(type==1)
                        {
                            arr_[i, j + 1] = arr_[i,j];
                            arr_[i, j - 1] = arr_[i,j];
                            arr_[i - 1, j] = arr_[i,j];
                            arr_[i - 1, j + 1] = arr_[i,j];
                            arr_[i + 1, j] = arr_[i,j];
                            arr_[i + 1, j + 1] = arr_[i,j];
                        }
                    }

                    if(i%2==1)
                    {
                        if (type == 0)
                        {
                            arr_[i, j + 1] = ResNum;
                            arr_[i, j - 1] = ResNum;
                            arr_[i - 1, j - 1] = ResNum;
                            arr_[i - 1, j] = ResNum;
                            arr_[i + 1, j - 1] = ResNum;
                            arr_[i + 1, j] = ResNum;
                        }
                        else if(type==1)
                        {
                            arr_[i, j + 1] = arr_[i,j];
                            arr_[i, j - 1] = arr_[i,j];
                            arr_[i - 1, j - 1] = arr_[i,j];
                            arr_[i - 1, j] = arr_[i,j];
                            arr_[i + 1, j - 1] = arr_[i,j];
                            arr_[i + 1, j] = arr_[i,j];
                        }
                    }
                }
            }
        }
    }
    
    //위험도 배열에 위험도 지정
    public void SetDanger()
    {

        for(int i = 5 ;i<MaxCol;i++)
        {
            for(int j = 2;j<MaxRow;j++)
            {
                // 위험도 1단계 = 3~14 번 ( 12 묶음 )
                if (arr[i, j] >= 3 && arr[i, j] <= 14)
                {
                    DangerArr[i, j] = 1;
                    CenterArr[i, j] = 1;
                    
                }

                // 위험도 2단계 = 15~29 번 ( 15 묶음 )
                else if (arr[i, j] >= 15 && arr[i, j] <= 29)
                {
                    DangerArr[i, j] = 2;

                    CenterArr[i, j] = 2;
                }

                // 위험도 3단계 = 30~41 번 ( 12 묶음 )
                else if (arr[i, j] >= 30 && arr[i, j] <= 41)
                {
                    DangerArr[i, j] = 3;

                    CenterArr[i, j] = 3;
                }

                // 위험도 4단계 = 나머지 6개
                else
                {
                    if (DangerArr[i, j] == 0 && arr[i, j] > 2)
                    {
                        DangerArr[i, j] = 4;

                        CenterArr[i, j] = 4;
                    }
                }
                // 위험도 5단계 = 마지막 번호 + 근처 2개
                if (arr[i, j] == 50)
                {
                    BossR = j;
                    BossC = i;
                    DangerArr[i, j] = 5;

                    CenterArr[i, j] = 5;
                    //SearchArr 함수로 최소 1개의 근처 타일 탐색
                    SearchArr(arr, i, j);

                    //위에서 2개의 타일을 탐색하지 못했을 경우 한번더 실행
                    if (Tile_D_5 <3)
                        SearchArr(arr, Tile_Place_R,Tile_Place_C);
                }


            }

        }

    }

    //근처의 타일을 검사하여 위험도 5단계로 지정하는 함수
    public void SearchArr(int[,] arr_, int r, int c)
    {
        //홀수 행
        if (r % 2 == 1 && r >= 2 && c >= 5)
        {
            //아래 타일 검사
            if (arr_[r + 3, c-1] > 2 && Tile_D_5 < 2 && DangerArr[r + 3, c - 1] != 3)
            {
                DangerArr[r + 3, c-1] = 5;
                CenterArr[r+3, c-1] = 5;

                Tile_D_5++;
                Tile_Place_R = r + 3;
                Tile_Place_C = c-1;
            }
            //위 타일 검사
            if (arr_[r - 3, c] > 2 && Tile_D_5 < 2 && DangerArr[r + 3, c - 1] != 3)
            {
                DangerArr[r - 3, c] = 5;
                CenterArr[r-3, c] = 5;
                Tile_D_5++;
                Tile_Place_R = r - 3;
                Tile_Place_C = c;
            }
            //왼쪽 위 타일 검사
            if (arr_[r - 2, c - 2] > 2 && Tile_D_5 < 2 && DangerArr[r + 3, c - 1] != 3)
            {
                DangerArr[r - 2, c - 2] = 5;
                CenterArr[r-2, c-2] = 5;
                Tile_D_5++;
                Tile_Place_R = r - 2;
                Tile_Place_C = c - 2;
            }
            //왼쪽 아래 타일 검사
            if (arr_[r + 1, c - 3] > 2 && Tile_D_5 < 2 && DangerArr[r + 3, c - 1] != 3)
            {
                DangerArr[r + 1, c - 3] = 5;
                CenterArr[r+1, c-3] = 5;
                Tile_D_5++;
                Tile_Place_R = r + 1;
                Tile_Place_C = c - 3;
            }
        }
        //짝수 행
        if (r % 2 == 0 && r >= 2 && c >= 5)
        {
            if (arr_[r + 3, c ] > 2 && Tile_D_5 < 2 && DangerArr[r + 3, c - 1] != 3)
            {
                DangerArr[r + 3, c ] = 5;
                CenterArr[r+3, c] = 5;
                Tile_D_5++;
                Tile_Place_R = r + 3;
                Tile_Place_C = c ;
            }

            if (arr_[r - 3, c +1 ] > 2 && Tile_D_5 < 2 && DangerArr[r + 3, c - 1] != 3)
            {
                DangerArr[r - 3, c + 1] = 5;
                CenterArr[r-3, c+1] = 5;
                Tile_D_5++;
                Tile_Place_R = r - 3;
                Tile_Place_C = c + 1;
            }
            if (arr_[r + 1, c - 2] > 2 && Tile_D_5 < 2 && DangerArr[r + 3, c - 1] != 3)
            {
                DangerArr[r + 1, c - 2] = 5;
                CenterArr[r+1, c-2] = 5;
                Tile_D_5++;
                Tile_Place_R = r + 1;
                Tile_Place_C = c - 2;
            }
            if (arr_[r - 2, c - 2] > 2 && Tile_D_5 < 2 && DangerArr[r + 3, c - 1] != 3)
            {
                DangerArr[r - 2, c - 2] = 5;
                CenterArr[r-2, c-2] = 5;
                Tile_D_5++;
                Tile_Place_R = r - 2;
                Tile_Place_C = c - 2;
            }
        }
    }
    
}
