using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using System;
public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public GameObject transitionContainer;
    private SceneTransition[] transitions;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance=this;
            DontDestroyOnLoad(gameObject);
        }
    }
    private void Start()
    {
        transitions=transitionContainer.GetComponentsInChildren<SceneTransition>();
    }
    public void LoadScene(string sceneName,string transitionName)
    {
        StartCoroutine(LoadSceneAsync(sceneName,transitionName));
    }
    private IEnumerator LoadSceneAsync(string sceneName, string transitionName)
{
    SceneTransition transition = transitions.FirstOrDefault(t => t.name == transitionName);

    if (transition == null)
    {
        Debug.LogError("Transition not found: " + transitionName);
        yield break;
    }

    // Fade screen to black first
    yield return transition.AnimateTransitionIn();

    AsyncOperation scene = SceneManager.LoadSceneAsync(sceneName);
    scene.allowSceneActivation = false;

    while (scene.progress < 0.9f)
    {
        yield return null;
    }
    // Now activate the scene
    scene.allowSceneActivation = true;

    // Wait one frame so scene fully switches
    yield return null;

    // Fade back from black
    yield return transition.AnimateTransitionOut();
}
}
