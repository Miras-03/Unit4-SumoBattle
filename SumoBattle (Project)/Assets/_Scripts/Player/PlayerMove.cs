using UnityEngine;

public sealed class PlayerMove
{
    private Rigidbody rb;
    private Transform focusCentre;

    private const int moveSpeed = 10;
    private const int boostSpeed = 20;
    private bool isPressingBoost = false;

    public PlayerMove(Rigidbody rb, Transform focusCentre)
    {
        this.rb = rb;
        this.focusCentre = focusCentre;
    }

    public void CheckOrMove()
    {
        if (GetVerticalInput() != 0)
            Move();
    }

    private void Move()
    {
        int speed = isPressingBoost ? boostSpeed : moveSpeed;
        rb.AddForce(focusCentre.forward * GetVerticalInput() * speed * Time.fixedDeltaTime, ForceMode.Impulse);
    }

    public void IsPressingBoost()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
            isPressingBoost = true;
        else if (Input.GetKeyUp(KeyCode.LeftShift))
            isPressingBoost = false;
    }

    private float GetVerticalInput() => Input.GetAxis("Vertical");
}