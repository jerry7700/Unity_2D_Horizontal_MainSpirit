using UnityEngine;

public class TeleportManager : MonoBehaviour
{
    [Header("傳送門")]
    public Transform otherTeleport;

    private bool PlayerIN;
    private Transform Player;

    private void Awake()
    {
        Player = GameObject.Find("主角").transform;
    }

    private void Teleport()
    {
        if (PlayerIN && Input.GetKeyDown(KeyCode.UpArrow))
        {
            Player.position = otherTeleport.position + Vector3.up * 1.5f;
        }
    }

    private void Update()
    {
        Teleport();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "主角")
        {
            PlayerIN = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "主角")
        {
            PlayerIN = false;
        }
    }
}
