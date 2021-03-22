using UnityEngine;

public class Enemy_insect : MonoBehaviour
{
    #region 欄位
    [Header("左")]
    public Transform leftPoint;
    [Header("右")]
    public Transform rightPoint;
    [Header("速度"),Range(1,50)]
    public float speed = 2.5f;

    private Animator anim;
    private float leftX, rightX;
    private bool faceLeft = true;
    private Rigidbody2D rb;
    #endregion

    #region 事件
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        //斷絕父子關係
        transform.DetachChildren();
        //取得左右的X
        leftX = leftPoint.position.x;
        rightX = rightPoint.position.x;
        //刪除左右
        Destroy(leftPoint.gameObject);
        Destroy(rightPoint.gameObject);
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        Move();
    }
    #endregion

    #region 方法
    /// <summary>
    /// 移動
    /// </summary>
    private void Move()
    {
        //如果(面向左)
        if(faceLeft)
        {
            //往左移動
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            if(transform.position.x < leftX)
            {
                //轉向
                transform.localScale = new Vector3(1,1,1);
                faceLeft = false;
            }
        }
        else
        {
            //往右移動
            rb.velocity = new Vector2(speed, rb.velocity.y);
            if (transform.position.x > rightX)
            {
                //轉向
                transform.localScale = new Vector3(-1, 1, 1);
                faceLeft = true;
            }
        }
    }

    /// <summary>
    /// 死亡
    /// </summary>
    public void Death()
    {
        anim.SetBool("死亡開關", true);
        GetComponent<BoxCollider2D>().enabled = false;
        rb.Sleep();
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        Destroy(gameObject);
    }
    #endregion
}
