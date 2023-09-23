using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Spawner : MonoBehaviour
{
    public List<int> WaveCounts;
    public float spawnDelay;
    public float spawnPosDistance;
    public Transform player;
    public int spawnerIndex;

    public static UnityEvent OnSpawnerStart = new UnityEvent();
    public static UnityEvent OnWaveEnd = new UnityEvent();
    private int currentWaveIndex;
    private List<GameObject> spawnedCharacters = new List<GameObject>();
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
            var go = MultiGameObjectPool.Instance.GetObject("Skeleton");
            go.transform.position = RandomPosCalculator();
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
        for (int i = 0; i < spawnedCharacters.Count; i++)
        {
            if (spawnedCharacters[i].activeSelf)
            {
                return false;
            }
        }
        return true;
    }
    private Vector3 RandomPosCalculator()
    {
        Vector3 cameraPosition = Camera.main.transform.position;

        float horizontalFOV = Camera.main.fieldOfView;

        float randomAngle = Random.Range(0f, 360f);
        float randomDistance = Random.Range(10f, 20f);

        float x = cameraPosition.x + Mathf.Cos(randomAngle) * randomDistance;
        float z = cameraPosition.z + Mathf.Sin(randomAngle) * randomDistance;

        float y = 0f;

        return new Vector3(x, y, z);
    }
}
