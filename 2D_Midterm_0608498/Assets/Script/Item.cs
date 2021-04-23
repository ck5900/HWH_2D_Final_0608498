﻿using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("掉落物品")]
    public GameObject prop;
    [Header("掉落機率"), Range(0f, 1f)]
    public float percent = 0.5f;

 
    public void DropProp()
    {
        //隨機值=隨機.範圍(最小值,最大值)
        float r = Random.Range(0f, 1f);

        if (r <= percent)
        {
            //生成(生成的物件,座標,角度)
            Instantiate(prop, transform.position, transform.rotation);
        }
        //刪除(此物件)
        Destroy(gameObject);

    }


}