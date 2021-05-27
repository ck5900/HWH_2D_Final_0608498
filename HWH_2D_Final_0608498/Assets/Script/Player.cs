using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //修飾詞 類型 名稱(指定值);


    //類型 四大類型
    //整數 int
    //浮點數 float
    //布林值 bool true是、false否
    //字串 string
    [Header("等級")]
    [Tooltip("這是角色的等級")]
    public int Lv = 1;
    [Header("移動速度"), Range(0, 300)]
    public float speed = 10.5f;
    [Header("角色是否死亡")]
    public bool isDead = false;
    [Header("角色名稱"), Tooltip("這是角色的名稱")]
    public string cName = "玩家";
    [Header("虛擬搖桿")]
    public FixedJoystick joystick;
    [Header("變形元件")]
    public Transform tra;
    [Header("動畫元件")]
    public Animator ani;
    [Header("偵測範圍")]
    public float rangeAttack = 2.5f;
    [Header("血量")]
    public float hp = 200;
    [Header("血條系統")]
    public HpManager hpManager;
    [Header("攻擊力"), Range(0, 1000)]
    public float attack = 20;
    [Header("音效來源")]
    public AudioSource aud;
    [Header("攻擊音效")]
    public AudioClip soundAttack;

    private float hpMax;

    //事件：繪製圖示
    private void OnDrawGizmos()
    {
        //指定圖示顏色(紅,綠,藍,透明)
        Gizmos.color = new Color(0, 1, 1, 0.3f);
        //繪製圖示 球體(中心點,半徑)
        Gizmos.DrawSphere(transform.position, rangeAttack);
    }

    //方法語法 Method-儲存複雜的程式區塊或演算法
    //修飾詞 類型 名稱(){程式區塊或演算法}
    //void無類型

    /// <summary>
    /// 移動
    /// </summary>

    private void Move()
    {
        if (isDead) return;               //如果死亡就跳出

        print("移動");

        float h = joystick.Horizontal;
        //float v = joystick.Vertical;

        tra.Translate(h * speed, 0, 0);
        ani.SetFloat("水平", h);
        //ani.SetFloat("垂直", v);


    }
    //要被按鈕呼叫必須設為公開
    public void Attack()
    {
        if (isDead) return;              //如果死亡就跳出 

        aud.PlayOneShot(soundAttack, 0.1f);


        print("攻擊");

        //2D物理 圓形碰撞(中心點,半徑,方向,距離,圖層編號)
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, rangeAttack, -transform.up, 0, 1 << 8);

        if (hit && hit.collider.tag == "道具") hit.collider.GetComponent<Item>().DropProp();
        if (hit && hit.collider.tag == "敵人") hit.collider.GetComponent<Enemy>().Hit(attack);

    }

    //要被其他腳本呼叫也要設定為公開
    /// <summary>
    /// 受傷
    /// </summary>
    /// <param name="damage">接收到的傷害值</param>
    public void Hit(float damage)
    {
        hp -= damage;                                    //扣除傷害值
        hpManager.UpdateHpBar(hp, hpMax);                //更新血條
        StartCoroutine(hpManager.ShowDamage(damage));    //啟動協同程序

        if(hp <= 0) Dead();                              //如果血量<=0就死亡
    }

    /// <summary>
    /// 死亡
    /// </summary>
    private void Dead()
    {
        hp = 0;
        isDead = true;
        Invoke("Replay", 2);
    }

    /// <summary>
    /// 重新遊戲
    /// </summary>
    private void Replay()
    {
        SceneManager.LoadScene("遊戲場景");
    }

    //事件-特定時間會執行的方法
    //開始事件：撥放後執行一次
    private void Start()
    {
        hpMax = hp;       //取得血量最大值

    }
    //更新事件：大約一秒執行六十次 60FPS
    private void Update()
    {
        Move();

    }

    [Header("金幣數量")]
    public Text textCoin;

    private int coin;

    //觸發事件-進入:兩個物件必須有一個勾選 Is Trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "金幣")
        {
            coin++;
            Destroy(collision.gameObject);
            textCoin.text = "金幣:" + coin;
        }
    }
}

