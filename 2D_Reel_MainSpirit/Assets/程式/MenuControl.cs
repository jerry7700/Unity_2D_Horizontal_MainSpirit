using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
    
    
    public void StartGame()
    {
        
        Invoke("DelayStartGame", 1.5f);
    }

    public void BackToMenu()
    {
        
        Invoke("DelayMenuGame", 0.5f);
    }

    public void QuitGame()
    {
        
        Invoke("DelayQuitGame", 0.5f);
    }

    #region [DelayGame]
    private void DelayStartGame()
    {
        SceneManager.LoadScene(1);
    }

    private void DelayMenuGame()
    {
        SceneManager.LoadScene(0);
    }

    private void DelayQuitGame()
    {
        Application.Quit();
    }
    #endregion

}
