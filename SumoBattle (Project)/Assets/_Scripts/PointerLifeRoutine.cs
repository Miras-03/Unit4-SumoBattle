using System.Collections;
using UnityEngine;

public sealed class PointerLifeRoutine : MonoBehaviour
{
    private void Start() => StartCoroutine(PowerupCountDown());

    private IEnumerator PowerupCountDown()
    {
        yield return new WaitForSeconds(7);
        Destroy(gameObject);
    }
}