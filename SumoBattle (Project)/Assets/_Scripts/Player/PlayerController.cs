using UnityEngine;

public sealed class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform focusCentre;
    [SerializeField] private Transform pushPowerupIndicator;
    [SerializeField] private Transform shootPowerupIndicator;
    private Rigidbody rb;
    private PlayerMove playerMove;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerMove = new PlayerMove(rb, focusCentre);
    }

    private void Start()
    {
        SetPushPowerup(false);
        SetShootpowerup(false);
    }

    private void Update() => playerMove.IsPressingBoost();

    private void FixedUpdate()
    {
        playerMove.CheckOrMove();

        pushPowerupIndicator.position = transform.position;
        shootPowerupIndicator.position = transform.position;
    }

    public void SetPushPowerup(bool flag) => pushPowerupIndicator.gameObject.SetActive(flag);

    public void SetShootpowerup(bool flag) => shootPowerupIndicator.gameObject.SetActive(flag);
}