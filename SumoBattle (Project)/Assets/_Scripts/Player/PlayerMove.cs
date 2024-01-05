using UnityEngine;

public class PlayerMove
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
        Debug.Log(speed);
        rb.AddForce(focusCentre.forward * GetVerticalInput() * speed * Time.fixedDeltaTime, ForceMode.Impulse);
    }

    public void IsPressingBoost()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isPressingBoost = true;
            Debug.Log(isPressingBoost);
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isPressingBoost = false;
            Debug.Log(isPressingBoost);
        }
    }

    private float GetVerticalInput() => Input.GetAxis("Vertical");
}
