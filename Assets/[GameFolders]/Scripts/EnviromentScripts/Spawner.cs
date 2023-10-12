using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Spawner : MonoBehaviour
{
    #region Params
    [Header("Spawner Info")]
    public List<int> WaveCounts;
    public float spawnDelay;
    public bool canSpawnBoss;

    [Space(10),Header("Target")]
    public Transform player;
    [Space(10), Header("Spawn Points")]
    public Transform topTransform;
    public Transform bottomTransform;
    public Transform leftTransform;
    public Transform rightTransform;
    public Transform bossSpawnTransform;

    private int currentWaveIndex=0;

    const float NEXT_WAVE_WAIT_TIME = 3f;
    const float BOSS_WAIT_TIME = 3f;

    bool isBossSpawned;

    private List<GameObject> spawnedCharacters = new List<GameObject>();
    #endregion
    #region Events
    public static UnityEvent OnAllEnemiesEnd = new UnityEvent();
    public static UnityEvent OnWaveEnd = new UnityEvent();
    public static UnityEvent OnBossRound = new UnityEvent();
    public static GameObjectEvent OnBossSpawned = new GameObjectEvent();
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
        if (isBossSpawned)
        {
            StartCoroutine(DirectToPortalCO());
            return;
        }
        
        if (currentWaveIndex == WaveCounts.Count)
        {
            if (canSpawnBoss)
            {
                OnBossRound.Invoke();
                StartCoroutine(WaitForBoss());
            }
            else
            {
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
        go.transform.position = bossSpawnTransform.position;
        go.transform.rotation = Quaternion.Euler(0, 180, 0);
        go.GetComponent<EnemyStateController>().SetTarget(player);
        go.GetComponent<EnemyStateController>().Initialize();
        spawnedCharacters.Add(go);
        OnBossSpawned.Invoke(go);
        isBossSpawned = true;
    }
    #endregion
    #region Numerators
    IEnumerator DirectToPortalCO()
    {
        FeedbackPanel.OnFeedbackOpen.Invoke("ALL WAVES END!");
        yield return new WaitForSeconds(1);
        FeedbackPanel.OnFeedbackOpen.Invoke("YOU CAN USE PORTAL NOW!");
        OnAllEnemiesEnd.Invoke();
        yield return new WaitForSeconds(1);
        FeedbackPanel.OnFeedbackClose.Invoke();
    }
    IEnumerator WaitForNextWave()
    {
        FeedbackPanel.OnFeedbackOpen.Invoke("NEW WAVE IS COMING!");
        yield return new WaitForSeconds(NEXT_WAVE_WAIT_TIME);
        FeedbackPanel.OnFeedbackClose.Invoke();

        Spawn();
    }
    IEnumerator WaitForBoss()
    {
        FeedbackPanel.OnFeedbackOpen.Invoke("WAVE END! BOSS IS COMING!");
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
            if (spawnedCharacters[i].activeSelf)
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

        Vector3 randomPos = Vector3.zero;
        while (true)
        {
            randomPos = new Vector3(Random.Range(minX, maxX), 0, Random.Range(minZ, maxZ));
            float distanceToPlayer =Vector3.Distance(player.position , randomPos);
            if (distanceToPlayer > 10)
            {
                break;
            }
        }
        return randomPos;

    }
    private Quaternion GetLookPlayerRotation(Vector3 spawnedPos)
    {
        Vector3 lookDirection = player.transform.position-spawnedPos ;
        lookDirection.y = 0;

        return Quaternion.LookRotation(lookDirection);
    }
    #endregion
}
