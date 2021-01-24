using UnityEngine;

public class Player : MonoBehaviour
{
    #region 欄位
    [Header("移動速度")]
    [Range(0, 1000)]
    public float Speed = 10.5f;
    [Header("跳躍高度")]
    [Range(0, 3000)]
    public int jump = 100;
    [Header("是否在地板上")]
    [Tooltip("是否在地板上")]
    public bool grond = false;
    [Header("攻擊範圍")]
    [Range(0, 100)]
    public float rangeattack = 10;
    [Header("攻擊範圍位移")]
    public Vector3 offsetAttack;
    [Header("攻擊範圍大小")]
    public Vector3 sizeAttack;
    [Header("攻擊力")]
    [Range(0, 100)]
    public float attack = 50;
    [Header("血量")]
    [Range(0, 200)]
    public float HP = 100;
    [Header("最大血量")]
    [Range(0, 200)]
    public float HPMax = 100;

    private Rigidbody2D rb;
    private AudioSource aud;
    private Animator anim;
    public float h;
    #endregion

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        GetHorizontal();
        Move();
    }

    #region 方法

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1f, 0f, 0f, 0.6f);
        Gizmos.DrawCube(transform.position + transform.right * offsetAttack.x + transform.up * offsetAttack.y, sizeAttack);
    }

    /// <summary>
    /// 移動
    /// </summary>
    /// 
    private void GetHorizontal()
    {
        //輸入取得軸向("水平")
        h = Input.GetAxis("Horizontal");
    }
    private void Move()
    {
        rb.velocity = new Vector2(h * Speed, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            //transform指的此腳本同一層的 transform元件
            // Rotation 角度在程式是 localEulerAngless
            transform.localEulerAngles = new Vector3(0, 180, 0);
        }
        //如果玩家按下A就執行
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.localEulerAngles = Vector3.zero;
        }
        anim.SetBool("跑步開關", h != 0);
    }


    #endregion
}
