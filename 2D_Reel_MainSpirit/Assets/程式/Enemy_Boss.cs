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

        Gizmos.color = new Color(0, 1f, 0f, 0.5f);
        Gizmos.DrawRay(transform.position, -transform.right * 1.5f);
    }

    private void Update()
    {
        Move();
        Death();
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

        float y = transform.position.x > player.transform.position.x ? 0 : 180;
        transform.eulerAngles = new Vector3(0, y, 0);

        float dis = Vector2.Distance(transform.position, player.transform.position);

        if (!isattack && dis > rangeattack)
        {
            rb.MovePosition(transform.position + -transform.right * Time.deltaTime * Speed);
        }
        else if (!isattack)
        {
            StartCoroutine(Attack());
        }

        if (!isattack) anim.SetBool("走路開關", rb.velocity.magnitude > 0);
    }

    /// <summary>
    /// 攻擊
    /// </summary>
   /*public void Attack()
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
    }*/

    bool isattack;
    public float waitattack = 1f;
    public float BossSpeed = 0.5f;
    public float waitBossSpeed = 2f;

    public IEnumerator Attack()
    {
        isattack = true;
        anim.SetBool("攻擊開關", true);
        yield return new WaitForSeconds(waitattack);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.right, 1.5f, 1 << 11);
        while(!hit)
        {
            hit = Physics2D.Raycast(transform.position, -transform.right, 1.5f, 1 << 11);
            rb.MovePosition(transform.position + -transform.right * BossSpeed);
            yield return new WaitForSeconds(waitBossSpeed);
        }

        print(hit.collider.name);
        isattack = false;
        anim.SetBool("攻擊開關", false);

    }
    public int hp = 2;

    private void Death()
    {
        if (hp <= 0)
        {
            anim.SetBool("死亡開關", true);
            rb.Sleep();
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            GetComponent<CapsuleCollider2D>().enabled = false;
            this.enabled = false;
        }
    }
    #endregion
}
