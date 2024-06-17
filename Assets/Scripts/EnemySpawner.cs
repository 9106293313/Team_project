using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // 存放小怪的預置物
    public int numberOfEnemies = 5; // 生成的小怪數量
    public float spawnRadius = 5f; // 生成的範圍

    void Start()
    {
        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            Vector2 randomLocalPosition = Random.insideUnitCircle * spawnRadius; // 在半徑範圍內生成隨機局部位置

            int randomEnemyIndex = Random.Range(0, enemyPrefabs.Length); // 隨機選擇一種小怪

            Vector3 randomWorldPosition = transform.TransformPoint(randomLocalPosition); // 將局部位置轉換為世界位置

            GameObject enemy = Instantiate(enemyPrefabs[randomEnemyIndex], randomWorldPosition, Quaternion.identity);

            // 將生成的小怪放置在Spawner下面，以便更好的組織場景中的物件
            enemy.transform.parent = transform;
        }
    }
}
