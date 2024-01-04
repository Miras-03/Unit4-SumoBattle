using UnityEngine;

public sealed class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform focusCentre;
    private Rigidbody rb;
    private const int moveSpeed = 400;

    private void Awake() => rb = GetComponent<Rigidbody>();

    private void FixedUpdate() => CheckOrMove();

    private void CheckOrMove()
    {
        if (GetVerticalInput() != 0)
            Move();
    }

    private void Move() => rb.AddForce(focusCentre.forward * GetVerticalInput() * moveSpeed * Time.fixedDeltaTime);

    private float GetVerticalInput() => Input.GetAxis("Vertical");
}