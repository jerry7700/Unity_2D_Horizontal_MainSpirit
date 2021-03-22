using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_totem : MonoBehaviour
{
    [Header("子彈")]
    [Tooltip("子彈")]
    public GameObject Bullet;
    [Header("子彈生成點")]
    [Tooltip("子彈生成點")]
    public Transform BulletGenrate;
    [Header("子彈速度")]
    [Range(0, 5000)]
    public int BulletSpeed = 800;

    private Player player;
    private Rigidbody2D rb;
    private Animator anim;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        Damage();
    }

    private void Damage()
    {
        float y = transform.position.x > player.transform.position.x ? 0 : 180;
        transform.eulerAngles = new Vector3(0, y, 0);


        /*GameObject tamp = Instantiate(Bullet, BulletGenrate.position, transform.rotation);
        tamp.GetComponent<Rigidbody2D>().AddForce(BulletGenrate.right * BulletSpeed + BulletGenrate.up * 150);
        ParticleSystem ps = tamp.GetComponent<ParticleSystem>();
        */
    }

    public void Death()
    {
        anim.SetBool("死亡開關", true);
        GetComponent<BoxCollider2D>().enabled = false;
        rb.Sleep();
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        Destroy(gameObject);
    }
}
