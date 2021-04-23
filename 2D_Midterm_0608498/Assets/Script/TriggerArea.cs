using UnityEngine;

public class TriggerArea : MonoBehaviour
{
    //陣列 在類型後面加上中括號
    //用於保存相同類型的複數資料
    [Header("要關閉的怪物們")]
    public GameObject[] monsters;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "球球")
            monsters[0].SetActive(false);
    }
}
