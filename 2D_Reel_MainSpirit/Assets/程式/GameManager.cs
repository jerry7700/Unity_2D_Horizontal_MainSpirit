using UnityEngine;

public class GameManager : MonoBehaviour
{

    private Player player;
    
    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    public void GamePause()
    {
        Time.timeScale = 0;
        player.enabled = false;
    }

    public void GameRestar()
    {
        Time.timeScale = 1;
        player.enabled = true;
    }
}
