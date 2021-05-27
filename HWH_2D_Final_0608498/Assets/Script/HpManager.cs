using UnityEngine;
using UnityEngine.UI;
using System.Collections;   //引用 系統集合API裡包含協序

public class HpManager : MonoBehaviour
{
    [Header("血條")]
    public Image bar;
    [Header("傷害數值")]
    public RectTransform rectDamage;

    /// <summary>
    ///輸入血量與血量最大值並更新血條
    /// </summary>
    ///<param name="hp">當前血量</param>
    ///<param name="hpMax">血量最大值</param>
    public void UpdateHpBar(float hp, float hpMax)
    {
        //血條 填滿數值 = 當前血量 / 血量最大
        bar.fillAmount = hp / hpMax;
    }

    /// <summary>
    /// 顯示傷害值
    /// </summary>
    /// <returns></returns>
    public IEnumerator ShowDamage(float damage)
    {
        RectTransform rect =Instantiate(rectDamage, transform);    //生成傷害數值
        rect.anchoredPosition = new Vector2(0, 70);                //指定座標
        rect.GetComponent<Text>().text = damage.ToString();        //更新數值

        float y = rect.anchoredPosition.y;                         //取得原始Y軸

        while (y < 190)                                            //當Y小於400時持續執行
        {
            y += 20;                                               //每次上升的單位20
            rect.anchoredPosition = new Vector2(0, y);             //更新座標
            yield return new WaitForSeconds(0.02f);                //等待幾秒(秒數)
        }
        Destroy(rect.gameObject, 0.3f);                            //刪除傷害數值物件
    }
}
