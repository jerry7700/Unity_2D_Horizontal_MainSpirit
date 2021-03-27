using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    #region 欄位
    [Header("生命"),Range(0 , 10)]
    public int health;
    [Header("生命數量"), Range(1, 10)]
    public int numOfHearts;

    public Image[] hearts;
    [Header("滿的生命")]
    public Sprite fullHeart;
    [Header("空的生命")]
    public Sprite emptyHeart;
    #endregion

    #region 方法
    private void Update()
    {
        if(health > numOfHearts)
        {
            health = numOfHearts;
        }

        //打for快速按tab兩次 會出現for (int i = 0; i < length; i++)
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if(i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }
    #endregion
}
