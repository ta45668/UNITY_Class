using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseLockCtrl : MonoBehaviour
{
    [Header("密碼鎖的物件"), SerializeField]
    GameObject[] lockObj = new GameObject[6];

    int nowLockNubmer = 0;//現在所選擇的鎖
    // Use this for initialization
    void Start ()
    {
        for (int i = 0; i < lockObj.Length; i++)
        {
            lockObj[i].SetActive(false);
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
		
	}
}
