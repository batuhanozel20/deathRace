using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayEasy()
    {
        SceneManager.LoadScene("Game Scene Easy");
    }
    public void PlayMedium()
    {
        SceneManager.LoadScene("Game Scene Medium");
    }
    public void PlayHard()
    {
        SceneManager.LoadScene("Game Scene Hard");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
