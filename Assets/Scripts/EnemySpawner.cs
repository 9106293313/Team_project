using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // �s��p�Ǫ��w�m��
    public int numberOfEnemies = 5; // �ͦ����p�Ǽƶq
    public float spawnRadius = 5f; // �ͦ����d��

    void Start()
    {
        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            Vector2 randomLocalPosition = Random.insideUnitCircle * spawnRadius; // �b�b�|�d�򤺥ͦ��H��������m

            int randomEnemyIndex = Random.Range(0, enemyPrefabs.Length); // �H����ܤ@�ؤp��

            Vector3 randomWorldPosition = transform.TransformPoint(randomLocalPosition); // �N������m�ഫ���@�ɦ�m

            GameObject enemy = Instantiate(enemyPrefabs[randomEnemyIndex], randomWorldPosition, Quaternion.identity);

            // �N�ͦ����p�ǩ�m�bSpawner�U���A�H�K��n����´������������
            enemy.transform.parent = transform;
        }
    }
}
