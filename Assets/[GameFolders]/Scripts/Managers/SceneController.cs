using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


public class SceneController : Singleton<SceneController>
{
    #region Params
    public bool loadingInProgress { get; private set; }

    #endregion
    #region Events
    [HideInInspector]
    public UnityEvent OnSceneStartedLoading = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnSceneUnloading = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnSceneLoaded = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnSceneUnLoaded = new UnityEvent();
    [HideInInspector]
    public SceneEvent OnSceneInfo = new SceneEvent();
    [HideInInspector]
    public UnityEvent OnSceneTransitionStart = new UnityEvent();
    #endregion
    #region Methods
    public void LoadScene(string sceneName)
    {
        if (loadingInProgress)
            return;

        StartCoroutine(LoadSceneCo(sceneName));
    }

    IEnumerator LoadSceneCo(string sceneName)
    {
        loadingInProgress = true;
        yield return new WaitForSeconds(2f);

        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            var scene = SceneManager.GetSceneAt(i);
            if (scene.name.Contains("Level"))
                yield return UnloadSceneCo(scene);
        }

        OnSceneStartedLoading.Invoke();

        yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        var loadedScene = SceneManager.GetSceneByName(sceneName);
        if (loadedScene.name.Contains("Level"))
        {
            SceneManager.SetActiveScene(loadedScene);
            yield return new WaitForSeconds(0.2f);
            OnSceneLoaded.Invoke();
        }

        OnSceneInfo.Invoke(loadedScene, true);
        GameManager.Instance.IsGameStarted = true;
        loadingInProgress = false;
    }


    public void UnloadScene(string sceneName)
    {
        Scene scene = SceneManager.GetSceneByName(sceneName);
        SceneManager.SetActiveScene(SceneManager.GetSceneAt(0));
        StartCoroutine(UnloadSceneCo(scene));
    }

    IEnumerator UnloadSceneCo(Scene scene)
    {
        OnSceneInfo.Invoke(scene, false);
        OnSceneUnloading.Invoke();
        yield return SceneManager.UnloadSceneAsync(scene.buildIndex);
        OnSceneUnLoaded.Invoke();
    }
    #endregion
}

public class SceneEvent : UnityEvent<Scene, bool> { }
