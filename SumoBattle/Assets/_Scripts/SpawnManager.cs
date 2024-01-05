using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public sealed class SpawnManager : MonoBehaviour
{
    [SerializeField] private Transform enemy;
    [SerializeField] private Transform powerup;

    private int enemiesQuantity = 1;

    private void Start()
    {
        SpawnPowerup();
        SpawnEnemy();
    }

    public void SpawnPowerup()
    {
        Vector3 randVector;
        GeneratyRandomVector(out randVector);
        Instantiate(powerup, randVector, Quaternion.identity);
    }

    public void SpawnEnemy() => StartCoroutine(SpawnWithDelay());

    private void GeneratyRandomVector(out Vector3 randVector)
    {
        const int edgeOfCliff = 6;
        int randXPos = Random.Range(-edgeOfCliff, edgeOfCliff);
        int randZPos = Random.Range(-edgeOfCliff, edgeOfCliff);

        randVector = new Vector3(randXPos, 0, randZPos);
    }

    private IEnumerator SpawnWithDelay()
    {
        yield return new WaitForSeconds(2);
        for (int i = 0; i < enemiesQuantity; i++)
        {
            Vector3 randVector;
            GeneratyRandomVector(out randVector);
            Instantiate(enemy, randVector, Quaternion.identity);
        }
        EnemyDeathCount.Instance.Count = enemiesQuantity;
        enemiesQuantity++;
    }
}