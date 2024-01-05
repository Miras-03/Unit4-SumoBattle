using UnityEngine;

public sealed class DeathManager : MonoBehaviour
{
    [SerializeField] private OutOfCliffDestroy outOfCliffDestroy;
    private DeathEnum deathEnum;

    private void Awake() => deathEnum = new DeathEnum();

    private void OnEnable() => deathEnum.Add(outOfCliffDestroy);

    private void OnDestroy() => deathEnum.Clear();
}
