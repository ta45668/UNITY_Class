using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

//駝峰式 = 單字的第一個英文字母要大寫
//只要是腳本的第一個都是大寫
//變數的第一個是小寫
//全域變數的第一個是大寫 = static
//全域變數 = 在所有的腳本都可以使用
//功能的第一個是大寫

public class ChooseLockCtrl : MonoBehaviour
{
    //protected =//小山貓
    //public = 公開變數 = 可以由外部作修改即使用
    //private = 私人變數 = 只能自己使用
    //public static = 全域變數 = 在所有的腳本都可以使用
    //什麼都不加 = 只能自己使用(沒有保護)

    //變數 = 關鍵字+修飾詞+型態

    //int = 整數
    //float = 小數
    //bool = 布林值

    [Header("密碼鎖的物件"), SerializeField]
    GameObject[] lockObj = new GameObject[6];

    Transform[][] lockNunbmerObj = new Transform[6][];//選擇的物件
    Vector3[][] lockNunbmerPost = new Vector3[6][];//選擇物件的原始位置
    
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
    /// 移動物件(LockNunbmer)方法
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
