using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public static UI instance;
    private int killCount;
    private float levelTime;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI killCountText;
    private void Awake()
    {
        instance=this;
        Time.timeScale=1;
    }
    private void Update()
    {
        levelTime+=Time.deltaTime;
        timerText.text=levelTime.ToString("F1")+"s";
    } 
    public void enableGameOverUI()
    {
        Time.timeScale=.5f;
        gameOverUI.SetActive(true);
    }
    public void RestartLevel()
    {
        levelTime=0;
        killCount=0;
        int sceneIndex=SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex);
    }
    public void AddKillCount()
    {
        killCount++;
        killCountText.text=killCount.ToString();
    }
}
