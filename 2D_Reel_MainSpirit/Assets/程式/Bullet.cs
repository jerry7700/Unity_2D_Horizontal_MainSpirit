using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Health hp;

    private void Awake()
    {
        hp = FindObjectOfType<Health>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == ("主角"))
        {
            hp.health--;
        }
        Destroy(gameObject);
    }
}
