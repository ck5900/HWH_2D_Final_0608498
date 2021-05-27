
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("追蹤範圍"), Range(0, 500)]
    public float rangeTrack = 2;
    [Header("移動速度"),Range(0,50)]
    public float speed = 2;

    private Transform player;

    private void Start()
    {
        //玩家變形 = 尋找遊戲物件("物件名稱")變形
        player = GameObject.Find("玩家").transform;
    }

    //繪製圖示事件
    private void OnDrawGizmos()
    {
        //先指定顏色在畫圖
        Gizmos.color = new Color(0, 0, 1, 0.35f);
        //繪製球體
        Gizmos.DrawSphere(transform.position, rangeTrack);
    }

    private void Update()
    {
        Track();
    }


    /// <summary>
    ///追蹤玩家
    /// </summary>
    private void Track()
    {
        //距離等於三維向量
        float dis = Vector3.Distance(transform.position, player.position);

        print("距離:" + dis);

        //如果距離小於等於
        if (dis <= rangeTrack)
        {
            //物件的座標更新為三維向量
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed);
        }
    }
}
