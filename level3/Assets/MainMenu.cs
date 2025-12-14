using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene(1); // Level 1 index
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}