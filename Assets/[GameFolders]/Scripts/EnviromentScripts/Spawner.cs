using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Spawner : MonoBehaviour
{
    #region Params
    public List<int> WaveCounts;
    public float spawnDelay;
    public Transform player;
    private int currentWaveIndex;
    const float NEXT_WAVE_WAIT_TIME=3f;
    const float BOSS_WAIT_TIME=3f;

    public Transform topTransform;
    public Transform bottomTransform;
    public Transform leftTransform;
    public Transform rightTransform;

    public Transform bossSpawnTransform;

    public bool canSpawnBoss;
    private List<GameObject> spawnedCharacters = new List<GameObject>();
    #endregion
    #region Events
    public static UnityEvent OnAllWavesEnd = new UnityEvent();
    public static UnityEvent OnWaveEnd= new UnityEvent();
    public static UnityEvent OnBossRound = new UnityEvent();
    #endregion
    #region MonoBehaviours
    private void OnEnable()
    {
        LevelManager.Instance.OnLevelStart.AddListener(Spawn);
    }
    private void OnDisable()
    {
        LevelManager.Instance.OnLevelStart.RemoveListener(Spawn);
    }
    private void Update()
    {
        if (!LevelManager.Instance.IsLevelStarted)
            return;
        if (spawnedCharacters.Count == 0)
            return;
        if (CheckAllEnemiesDie())
            WaveAllDie();
    }
    private void WaveAllDie()
    {
        spawnedCharacters.Clear();
        if (currentWaveIndex == WaveCounts.Count - 1)
        {
            if (canSpawnBoss)
            {
                OnBossRound.Invoke();
                StartCoroutine(WaitForBoss());
            }
            else
            {
                OnAllWavesEnd.Invoke();
                StartCoroutine(DirectToPortalCO());
            }
        }
        else
        {
            OnWaveEnd.Invoke();
            StartCoroutine(WaitForNextWave());
        }
    }
    #endregion
    #region SpawnMethods
    void Spawn()
    {
        if (currentWaveIndex == WaveCounts.Count)
            currentWaveIndex = 0;
        StartCoroutine(SpawnWithDelayCO(WaveCounts[currentWaveIndex]));
        currentWaveIndex++;
    }
    IEnumerator SpawnWithDelayCO(int spawnCount)
    {
        for (int i = 0; i < spawnCount; i++)
        {
            var go = ObjectPoolManager.SpawnObject(ObjectPoolManager.Instance.GetObjectFromName("Skeleton"));
            go.transform.position = RandomPosCalculator();
            go.transform.rotation = GetLookPlayerRotation(go.transform.position);
            go.GetComponent<EnemyStateController>().SetTarget(player);
            go.GetComponent<EnemyStateController>().Initialize();
            spawnedCharacters.Add(go);
            yield return new WaitForSeconds(spawnDelay);
        }
    }
    private void SpawnBoss()
    {
        var go = ObjectPoolManager.SpawnObject(ObjectPoolManager.Instance.GetObjectFromName("Boss"));
        go.transform.position = RandomPosCalculator();
        go.transform.rotation = Quaternion.Euler(0, 180, 0);
        go.GetComponent<EnemyStateController>().SetTarget(player);
        go.GetComponent<EnemyStateController>().Initialize();
        spawnedCharacters.Add(go);
    }
    #endregion
    #region Numerators
    IEnumerator DirectToPortalCO()
    {
        FeedbackPanel.OnFeedbackOpen.Invoke("ALL WAVES END!");
        yield return new WaitForSeconds(1);
        FeedbackPanel.OnFeedbackOpen.Invoke("YOU CAN USE PORTAL NOW!");
        yield return new WaitForSeconds(1);
        FeedbackPanel.OnFeedbackClose.Invoke();
    }
    IEnumerator WaitForNextWave()
    {
        FeedbackPanel.OnFeedbackOpen.Invoke("WAVE END!");
        yield return new WaitForSeconds(1);
        FeedbackPanel.OnFeedbackClose.Invoke();

        FeedbackPanel.OnFeedbackOpen.Invoke("WAIT FOR NEW WAVE!");
        yield return new WaitForSeconds(NEXT_WAVE_WAIT_TIME);
        FeedbackPanel.OnFeedbackClose.Invoke();

        Spawn();
    }
    IEnumerator WaitForBoss()
    {
        FeedbackPanel.OnFeedbackOpen.Invoke("WAIT FOR BOSS!");
        yield return new WaitForSeconds(BOSS_WAIT_TIME);
        FeedbackPanel.OnFeedbackClose.Invoke();

        SpawnBoss();
    }
    #endregion
    #region HelperMethods
    private bool CheckAllEnemiesDie()
    {
        bool allEnemiesDead = true;

        for (int i = 0; i < spawnedCharacters.Count; i++)
        {
            if (spawnedCharacters[i] != null)
            {
                allEnemiesDead = false;
                break; 
            }
        }

        return allEnemiesDead;
    }
    private Vector3 RandomPosCalculator()
    {
        float maxX = rightTransform.position.x;
        float minX = leftTransform.position.x;
        float maxZ = topTransform.position.z;
        float minZ = bottomTransform.position.z;

        Vector3 randomPos = new Vector3(Random.Range(minX,maxX),0,Random.Range(minZ,maxZ));
        return randomPos;
    }
    private Quaternion GetLookPlayerRotation(Vector3 spawnedPos)
    {
        Vector3 lookDirection = spawnedPos - player.transform.position;
        lookDirection.y = 0;

        return Quaternion.LookRotation(lookDirection);
    }
    #endregion
}
