using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    [SerializeField]private Enemy_respawn enemy_spawn;
    public static UI instance;
    private int killCount;
    private float levelTime=60;
    [Header("Tutorial UI")]
    [SerializeField]private TextMeshProUGUI actionText;
    [SerializeField]private TextMeshProUGUI keyText;
    [SerializeField] private GameObject tutorialUI;
    private int currentStep=0;
    [Header("Timer UI")]
    [SerializeField] private GameObject timerUI;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI killCountText;
    private Transform player;
    private Transform charToProtect;
    private void Awake()
    {
        instance=this;
        Time.timeScale=1;
        player=FindFirstObjectByType<PlayerScript>().transform;
        charToProtect=FindFirstObjectByType<charToProtect>().transform;
    }
    private void Start()
    {
        killCount=enemy_spawn.MaxSpawn;
        killCountText.text=killCount.ToString();
        ShowStep();
    }
    private void Update()
    {
        CheckInput();
        enableTimerUI();
    } 
    public void enableGameOverUI()
    {
        Time.timeScale=.5f;
        gameOverUI.SetActive(true);
    }
    public void RestartLevel()
    {
        levelTime=60;
        killCount=15;
        int sceneIndex=SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex);
    }
    public void AddKillCount()
    {
        killCount--;
        killCountText.text=killCount.ToString();
    }
    public void enableTimerUI()
    {
        if (player.position.x > charToProtect.position.x)
        {
            timerUI.SetActive(true);
        }
    }
    private void ShowStep()
    {
        switch (currentStep)
        {
            case 0:
                actionText.text = "JUMP";
                keyText.text = "SPACE";
                break;

            case 1:
                actionText.text = "ATTACK";
                keyText.text = "MB1";
                break;
        }
    }
    private void CheckInput()
    {
        if (currentStep == 0 && Input.GetKeyDown(KeyCode.Space))
        {
            NextStep();
        }
        else if (currentStep == 1 && Input.GetMouseButtonDown(0))
        {
            NextStep();
        }
        else if(currentStep==2)
        {
            tutorialUI.SetActive(false);
        }
    }
    private void NextStep()
    {
        currentStep++;
        ShowStep();
    }
}
