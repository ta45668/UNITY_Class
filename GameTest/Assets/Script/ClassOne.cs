using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClassOne : MonoBehaviour
{
    [Header("GM程式碼(用來調用)")]
    public Gamemanager gamemanager;
    public GameObject game;
    // Use this for initialization
    private void OnTriggerEnter(Collider other)//接觸碰撞器(other = 碰撞的物體)
    {
        if (other.tag == "Player")//如果(if)當碰撞的物體(other)的標籤(tag)為"Player"
        {
            game.GetComponent<Gamemanager>().score += 1;//得到一分
            Destroy(this.gameObject);//刪除此物件
            gamemanager.score++;//得到一分
        }
    }

    /// <summary>
    /// 判斷名字
    /// </summary>
    public void JudgmentName()//功能
    {
    }
}
