using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public void Start()
    {
        MusicManager.Instance.PlayMusic("MainMenu");
    }
    public void Play()
    {
        Debug.Log("Button clicked");
        LevelManager.Instance.LoadScene("Game","CrossFade");
        MusicManager.Instance.PlayMusic("Game");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
