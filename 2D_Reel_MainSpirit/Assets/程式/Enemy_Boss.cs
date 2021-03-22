using UnityEngine;
using System.Collections;

public class Enemy_Boss : MonoBehaviour
{
    #region 欄位
    [Header("移動速度")]
    [Range(0, 1000)]
    public float Speed = 10.5f;
    [Header("攻擊範圍")]
    [Range(0, 100)]
    public float rangeattack = 10;
    [Header("攻擊時間")]
    [Range(0, 10)]
    public float attackCD = 3.5f;
    [Header("攻擊延遲")]
    [Range(0, 10)]
    public float attackDelay = 0.7f;
    [Header("攻擊範圍位移")]
    public Vector3 offsetAttack;
    [Header("攻擊範圍大小")]
    public Vector3 sizeAttack;

    private float timer;
    private Player player;
    private Rigidbody2D rb;
    private Animator anim;
    #endregion

    #region 事件
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player = FindObjectOfType<Player>();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1f, 0f, 0f, 0.5f);
        Gizmos.DrawSphere(transform.position, rangeattack);
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
    public void Move()
    {
        AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);
        if (info.IsName("Boss_攻擊")) return;

        float y = transform.position.x > player.transform.position.x ? 180 : 0;
        transform.eulerAngles = new Vector3(0, y, 0);

        float dis = Vector2.Distance(transform.position, player.transform.position);

        if (dis > rangeattack)
        {
            rb.MovePosition(transform.position + transform.right * Time.deltaTime * Speed);
        }
        else
        {
            Attack();
        }
        anim.SetBool("走路開關", rb.velocity.magnitude > 0);
    }

    /// <summary>
    /// 攻擊
    /// </summary>
    public void Attack()
    {
        rb.velocity = Vector3.zero;
        if (timer < attackCD)
        {
            anim.SetBool("攻擊開關", true);
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
            StartCoroutine(DelaySendDamage());
            anim.SetBool("攻擊開關", false);
        }
    }

    public IEnumerator DelaySendDamage()
    {
        yield return new WaitForSeconds(attackDelay);
        Collider2D hit = Physics2D.OverlapBox(transform.position + transform.right * offsetAttack.x + transform.up * offsetAttack.y, sizeAttack, 0, 1 << 9);
        if (hit) Speed = 10f;
        rb.MovePosition(transform.position + transform.right * Time.deltaTime * Speed);
    }
    #endregion
}
