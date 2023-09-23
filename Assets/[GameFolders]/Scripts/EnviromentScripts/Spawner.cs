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

    public static IntEvent OnSpawnerStart = new IntEvent();
    public static UnityEvent OnWaveEnd = new UnityEvent();
    private int currentWaveIndex;
    private List<GameObject> spawnedCharacters=new List<GameObject>();
    private void OnEnable()
    {
        LevelManager.Instance.OnLevelStart.AddListener(() => Spawn(0));
        OnSpawnerStart.AddListener((int spawnIndex) => Spawn(spawnIndex));
    }
    private void OnDisable()
    {
        LevelManager.Instance.OnLevelStart.RemoveListener(()=>Spawn(0));
        OnSpawnerStart.RemoveListener((int spawnIndex) => Spawn(spawnIndex));
    }
    void Spawn(int index)
    {
        if(spawnerIndex==index)
            StartCoroutine(SpawnWithDelayCO(WaveCounts[currentWaveIndex]));
    }
    IEnumerator SpawnWithDelayCO(int spawnCount)
    {
        for (int i = 0; i < spawnCount; i++)
        {
            var go=MultiGameObjectPool.Instance.GetObject("Skeleton");
            go.transform.position=RandomPosCalculator();
            go.GetComponent<EnemyStateController>().SetTarget(player);
            go.GetComponent<EnemyStateController>().Initialize();
            yield return new WaitForSeconds(spawnDelay);
        }
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