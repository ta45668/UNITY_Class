using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ChooseLockCtrl : MonoBehaviour
{
    [Header("密碼鎖的物件"), SerializeField]
    GameObject[] lockObj = new GameObject[6];

    Transform[][] lockNunbmerObj = new Transform[6][];//選擇的物件
    Vector3[][] lockNunbmerPost = new Vector3[6][];//選擇的物件

    int nowLockNubmer = 0;//現在所選擇的鎖
    int[] nowLockNubmerPost = new int[6] { 0, 0, 0, 0, 0, 0 };//現在所選擇的鎖位置(答案)
    bool isChoose = false;//正在選擇密碼
    // Use this for initialization
    void Start ()
    {
        for (int i = 0; i < lockObj.Length; i++)
        {
            lockNunbmerObj[i] = new Transform[10];
            lockNunbmerPost[i] = new Vector3[10];
            lockObj[i].SetActive(false);
            for (int j = 0; j < lockNunbmerObj[i].Length; j++)
            {
                lockNunbmerObj[i][j] = lockObj[i].transform.GetChild(j);//抓取物件(數字物件)
                lockNunbmerPost[i][j] = lockNunbmerObj[i][j].position;//抓取物件位置(數字物件位置)
            }
        }
        lockObj[0].SetActive(true);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            lockObj[nowLockNubmer].SetActive(false);
            nowLockNubmer++;
            if (nowLockNubmer >= 6)
            {
                nowLockNubmer = 0;
            }
            lockObj[nowLockNubmer].SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            lockObj[nowLockNubmer].SetActive(false);
            nowLockNubmer--;
            if (nowLockNubmer <= -1)
            {
                nowLockNubmer = 5;
            }
            lockObj[nowLockNubmer].SetActive(true);
        }

        if (!isChoose)//選擇答案
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                nowLockNubmerPost[nowLockNubmer]--;
                if (nowLockNubmerPost[nowLockNubmer] <= -1)
                {
                    nowLockNubmerPost[nowLockNubmer] = 9;
                }
                isChoose = true;
                MoveLockNunbmerObj(nowLockNubmer, 9);
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                nowLockNubmerPost[nowLockNubmer]++;
                if (nowLockNubmerPost[nowLockNubmer] >= 10)
                {
                    nowLockNubmerPost[nowLockNubmer] = 0;
                }
                isChoose = true;
                MoveLockNunbmerObj(nowLockNubmer, 8);
            }
        }
    }

    /// <summary>
    /// 移動物件方法
    /// </summary>
    /// <param name="reelNumber">滾輪編號</param>
    /// <param name="intervalNumber">區間數字(防止超出時會有破綻)</param>
    void MoveLockNunbmerObj(int reelNumber,int intervalNumber)
    {
        int post = 0;
        for (int i = 0; i < 10; i++)
        {
            if (i - nowLockNubmerPost[nowLockNubmer] >= 0)
            {
                post = i - nowLockNubmerPost[nowLockNubmer];
            }
            else
            {
                post = i - nowLockNubmerPost[nowLockNubmer];
                post += 10;
            }
            if (post != intervalNumber)
            {
                lockNunbmerObj[reelNumber][i].DOMove(lockNunbmerPost[reelNumber][post], 0.5f, false);
            }
            else
            {
                lockNunbmerObj[reelNumber][i].position = lockNunbmerPost[reelNumber][post];
            }
        }
        Invoke("UnLockAnswerChoosing", 0.65f);
    }
    /// <summary>
    /// 解鎖選擇答案
    /// </summary>
    void UnLockAnswerChoosing()
    {
        isChoose = false;
    }
}
