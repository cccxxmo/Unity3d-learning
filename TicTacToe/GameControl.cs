using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour 
{
    int turn;
    int count;
    int[,] matrix = new int[3, 3];

    // Use this for initialization
    void Start () 
    {
    	Init();
    }

   //初始化界面
    void Init()
    {
	turn = 1;
        for (int i = 0; i < 3; ++i)
        {
            for (int j = 0; j < 3; ++j)
            {
                matrix[i, j] = 0;
            }
        }
        count = 0;	
    }

    //验证是否结束游戏
    int Check()
    {
        for(int i = 0; i < 3; ++i)
        {
	        if (matrix[i, 0] != 0 && matrix[i,0]==matrix[i, 1] && matrix[i, 1] == matrix[i, 2])
            {
                return matrix[i, 0];
            }
        }
        for (int i = 0; i < 3; ++i)
        {
            if (matrix[0, i] != 0 && matrix[0, i] == matrix[1, i] && matrix[1, i] == matrix[2, i])
            {
                return matrix[0, i];
            }
        }
        if(matrix[1,1]!=0&&
            matrix[0,0]==matrix[1,1]&&
            matrix[1,1]==matrix[2,2]||
            matrix[0,2]==matrix[1,1]&&
            matrix[1,1]==matrix[2,0]
            )
            return matrix[1, 1];
        if (count == 9) return 2;
        return 0;
    }

    //构建UI
    void OnGUI()
    {	
        if(GUI.Button(new Rect(420, 300, 100, 50),"Restart"))
        {
            Init();
        }
        int result = Check();
		
         GUIStyle end = new GUIStyle
        {
            fontSize = 20
        };
        end.normal.textColor = Color.red;
        end.fontStyle = FontStyle.BoldAndItalic;

		GUIStyle hit = new GUIStyle
		{
		};
		
        if(result == 1)
                GUI.Label(new Rect(440, 200, 100, 50), "O WIN", style: end);
        else if (result == -1)
                GUI.Label(new Rect(440, 200, 100, 50), "X WIN", style: end);
        else if(result == 2)
                GUI.Label(new Rect(440, 200, 100, 50), "DUAL", style: end);
		
        for (int i = 0; i < 3; ++i)
        {
            for(int j = 0; j < 3; ++j)
            {
                if (matrix[i, j] == 1)
                {
                    GUI.Button(new Rect(400 + i * 50, j * 50, 50, 50), "O");
                }
				if (matrix[i, j] == -1)
                {
                    GUI.Button(new Rect(400 + i * 50, j * 50, 50, 50), "X");
                }
                if(GUI.Button(new Rect(400 + i * 50, j * 50, 50, 50), ""))
                {
                    if (result == 0)
                    {
                     	matrix[i, j] = turn;
                        ++count;
                        turn = -turn;
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update () 
    {
    }

}

