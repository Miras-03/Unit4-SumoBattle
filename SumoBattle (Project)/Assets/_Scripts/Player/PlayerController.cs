using UnityEngine;

public sealed class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform focusCentre;
    [SerializeField] private Transform powerupIndicator;
    private Rigidbody rb;
    private PlayerMove playerMove;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerMove = new PlayerMove(rb, focusCentre);
    }

    private void Start() => SetPowerupIndicator(false);

    private void Update() => playerMove.IsPressingBoost();

    private void FixedUpdate()
    {
        playerMove.CheckOrMove();
        powerupIndicator.position = transform.position;
    }

    public void SetPowerupIndicator(bool flag) => powerupIndicator.gameObject.SetActive(flag);
}