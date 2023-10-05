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

    public Transform topTransform;
    public Transform bottomTransform;
    public Transform leftTransform;
    public Transform rightTransform;

    private List<GameObject> spawnedCharacters = new List<GameObject>();
    #endregion
    #region Events
    public static UnityEvent OnSpawnerStart = new UnityEvent();
    public static UnityEvent OnWaveEnd = new UnityEvent();
    #endregion
    #region MonoBehaviours
    private void OnEnable()
    {
        LevelManager.Instance.OnLevelStart.AddListener(Spawn);
        OnSpawnerStart.AddListener(Spawn);
    }
    private void OnDisable()
    {
        LevelManager.Instance.OnLevelStart.RemoveListener(Spawn);
        OnSpawnerStart.RemoveListener(Spawn);
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
    #endregion
    #region MyMethods
    void Spawn()
    {
        if (currentWaveIndex == WaveCounts.Count)
            currentWaveIndex = 0;
        StartCoroutine(SpawnWithDelayCO(WaveCounts[currentWaveIndex]));
    }
    IEnumerator SpawnWithDelayCO(int spawnCount)
    {
        for (int i = 0; i < spawnCount; i++)
        {
            var go = ObjectPoolManager.SpawnObject(ObjectPoolManager.Instance.GetObjectFromName("Skeleton"), RandomPosCalculator(),Quaternion.identity);
            go.GetComponent<EnemyStateController>().SetTarget(player);
            go.GetComponent<EnemyStateController>().Initialize();
            spawnedCharacters.Add(go);
            yield return new WaitForSeconds(spawnDelay);
        }
        currentWaveIndex++;
    }
    private void WaveAllDie()
    {
        spawnedCharacters.Clear();
        OnWaveEnd.Invoke();
    }
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
    #endregion
}
