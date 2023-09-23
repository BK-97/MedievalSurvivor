using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class LevelManager : Singleton<LevelManager>
{
    [SerializeField]
    public LevelData LevelData;

    public Level CurrentLevel { get { return LevelData.Levels[LevelIndex]; } }

    [HideInInspector]
    public UnityEvent OnLevelStart = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnLevelFinish = new UnityEvent();

    private bool isLevelStarted;
    public bool IsLevelStarted { get { return isLevelStarted; } set { isLevelStarted = value; } }

    public int LevelIndex
    {
        get
        {
            int level = PlayerPrefs.GetInt(PlayerPrefKeys.LastLevel, 0);
            if (level > LevelData.Levels.Count - 1)
            {
                level = 0;
            }

            return level;
        }
        set
        {
            PlayerPrefs.SetInt(PlayerPrefKeys.LastLevel, value);
        }
    }

    private void OnEnable()
    {
        GameManager.Instance.OnStageFail.AddListener(ReloadLevel);
        Portal.OnUsePortal.AddListener(ChangeMap);
    }

    private void OnDisable()
    {
        GameManager.Instance.OnStageFail.RemoveListener(ReloadLevel);
        Portal.OnUsePortal.RemoveListener(ChangeMap);

    }
    public void ChangeMap(string nextLevelName)
    {
        FinishLevel();
        SceneController.Instance.OnSceneTransitionStart.Invoke();
        SceneController.Instance.UnloadScene(CurrentLevel.LoadLevelID);
        StartCoroutine(WaitForChangeMap(nextLevelName));
    }
    IEnumerator WaitForChangeMap(string nextLevelName)
    {
        yield return new WaitForSeconds(4);
        LoadThisLevel(GetLevelIndex(nextLevelName));
    }
    public void ReloadLevel()
    {
        FinishLevel();
        SceneController.Instance.LoadScene(CurrentLevel.LoadLevelID);
    }
    public void LoadLastLevel()
    {
        FinishLevel();
        SceneController.Instance.LoadScene(CurrentLevel.LoadLevelID);
    }
    public void LoadNextLevel()
    {
        FinishLevel();

        LevelIndex++;
        if (LevelIndex > LevelData.Levels.Count - 1)
        {
            LevelIndex = 0;
        }

        SceneController.Instance.LoadScene(CurrentLevel.LoadLevelID);
    }
    public void LoadThisLevel(int newLevelIndex)
    {
        FinishLevel();

        LevelIndex= newLevelIndex;
        if (LevelIndex > LevelData.Levels.Count - 1)
        {
            LevelIndex = 0;
        }

        SceneController.Instance.LoadScene(CurrentLevel.LoadLevelID);
    }
    public void LoadPreviousLevel()
    {
        FinishLevel();

        LevelIndex--;
        if (LevelIndex <= -1)
        {
            LevelIndex = LevelData.Levels.Count - 1;

        }

        SceneController.Instance.LoadScene(CurrentLevel.LoadLevelID);
    }
    public void StartLevel()
    {
        if (IsLevelStarted)
            return;
        IsLevelStarted = true;
        OnLevelStart.Invoke();
    }

    public void FinishLevel()
    {
        if (!IsLevelStarted)
            return;
        IsLevelStarted = false;
        OnLevelFinish.Invoke();
    }
    public int GetLevelIndex(string levelName)
    {
        for (int i = 0; i < LevelData.Levels.Count; i++)
        {
            if (LevelData.Levels[i].LoadLevelID == levelName)
                return i;
        }
        return 0;

    }
}
