using UnityEngine;

public class Movement : MonoBehaviour
{
    public Camera cam;
    public Rigidbody rb;
    public Animator player;
    public float forceSpeed = 5f;
    public float moveSpeed;
    public float runSpeed, walkSpeed;
    public float speedIndicator;

    public static bool canWalk = true;
    public float turnSmoothTime = .1f;

    private float turnSmoothVelocity;

    private float horizontal;
    private float vertical;

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        speedIndicator = Mathf.Round(rb.velocity.magnitude);

        if (Input.GetKey(KeyCode.LeftShift) && rb.velocity.magnitude > 0)
        {
            moveSpeed = runSpeed;
            player.SetBool("isRunning", true);
        }

        if (Input.GetKey(KeyCode.LeftShift) && rb.velocity.magnitude  < 1)
        {
            moveSpeed = runSpeed;
            player.SetBool("isRunning", false);
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            moveSpeed = walkSpeed;
            player.SetBool("isRunning", false);
        }
    }

    private void FixedUpdate()
    {
        Vector3 dir = new Vector3(horizontal, 0f, vertical).normalized;

        if (dir.magnitude >= .1f && canWalk == true)
        {
            startMove();
        }

        if (dir.magnitude <= .1f && rb.velocity.z != 0) stopMove();

        void startMove()
        {
            float targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            float currentSpeed = Mathf.Round(rb.velocity.magnitude);
            if(currentSpeed < moveSpeed) rb.AddForce(moveDir * Time.fixedDeltaTime * forceSpeed * 100);
            player.SetBool("isWalking", true);
        }

        void stopMove()
        {
            rb.velocity = Vector3.zero;
            player.SetBool("isWalking", false);
        }
    }
}
