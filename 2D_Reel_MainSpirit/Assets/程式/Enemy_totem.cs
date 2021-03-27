using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_totem : MonoBehaviour
{
    [Header("子彈"), Tooltip("子彈")]
    public GameObject Bullet;
    [Header("子彈生成點"), Tooltip("子彈生成點")]
    public Transform BulletGenrate;
    [Header("子彈速度")]
    public float BulletSpeed;
    [Header("攻擊範圍"), Range(0, 100)]
    public float atkRang = 10;
    [Header("攻擊CD"), Range(0, 100)]
    public float atkCD = 10;


    [Header("發射音效")]
    public AudioClip augFire;
    [Header("受傷音效")]
    public AudioClip augHurt;

    private Player player;
    private Rigidbody2D rb;
    private Animator anim;
    private AudioSource aud;
    private float Timer;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        aud = GetComponent<AudioSource>();
        player = FindObjectOfType<Player>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawSphere(transform.position, atkRang);
    }

    private void Update()
    {
        //Damage();
    }

    private void Damage()
    {
        float y = transform.position.x > player.transform.position.x ? 0 : 180;
        transform.eulerAngles = new Vector3(0, y, 0);
        float dis = Vector2.Distance(transform.position, player.transform.position);

        if (Timer < atkCD)
        {
            Timer += Time.deltaTime;
        }
        else if (dis <= atkRang)
        {
            Timer = 0;
            StartCoroutine(DelaySendDamege());
        }
    }

    private IEnumerator DelaySendDamege()
    {
        yield return new WaitForSeconds(atkCD);

        GameObject tamp = Instantiate(Bullet, BulletGenrate.position, transform.rotation);
        tamp.GetComponent<Rigidbody2D>().AddForce(BulletGenrate.right * -1 * BulletSpeed + BulletGenrate.up * 150);
        aud.PlayOneShot(augFire, 0.4f);

        ParticleSystem ps = tamp.GetComponent<ParticleSystem>();
    }

    public void Death()
    {
        aud.PlayOneShot(augHurt);
        anim.SetBool("死亡觸發", true);
        GetComponent<BoxCollider2D>().enabled = false;
        rb.Sleep();
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        Destroy(gameObject);
    }
}
