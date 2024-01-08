using System.Collections;
using UnityEngine;

public sealed class OutOfCliffDestroy : MonoBehaviour
{
    private SpawnManager spawner;

    private void Awake() => spawner = FindObjectOfType<SpawnManager>();

    private void Start() => StartCoroutine(DelayOrDestroy());

    private IEnumerator DelayOrDestroy()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            CheckOrDestroy();
        }
    }

    private void CheckOrDestroy()
    {
        if (transform.position.y < 0)
        {
            Destroy(gameObject);
            DecreaseCount();
        }
    }

    public void DecreaseCount()
    {
        int count = --EnemyDeathCount.Instance.Count;
        if (count == 0)
            spawner.OnNewWave.Invoke();
    }
}