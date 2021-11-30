using UnityEngine;

public class AxeThrow : MonoBehaviour
{
    public Animator kratos;
    public Rigidbody axe, player;
    public Collider playerCollider;
    public Transform brute;
    public Camera cam;

    public static bool hasThrown = false;
    private bool canThrow = false;
    public static bool isHolding = true;
    public float axeThrowForwForce;
    public float axeThrowUpwForce;
    public float axeTorque;


    public void Update()
    {
        if (Input.GetMouseButtonDown(0) && AxeReturn.isReturning == false && isHolding == true) PlayThrowAnim();

        void PlayThrowAnim()
        {
            kratos.SetTrigger("throw");
            hasThrown = true;
            isHolding = false;

            kratos.SetBool("isWalking", false);
            kratos.SetBool("isRunning", false);
        }
        if (!axe.isKinematic)
        {
            RotateAxe();
        }
        if (AxeReturn.isReturning == true)
        {
                Vector3 axeRot = axe.transform.localEulerAngles;
                axeRot.z += 100;                      
                axe.transform.eulerAngles = axeRot;
        }
        foreach (Collider c in axe.GetComponents<BoxCollider>())
        {
            Physics.IgnoreCollision(c, playerCollider);
        }
    }

    private void FixedUpdate()
    {
        if (canThrow == true)
        {
            Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            Vector3 rayDist = ray.GetPoint(10);
            Vector3 targetPos = new Vector3(rayDist.x, player.transform.position.y, rayDist.z);
            player.transform.LookAt(targetPos);

            axe.AddForce(ray.direction * axeThrowForwForce * Time.fixedDeltaTime, ForceMode.Impulse);
            axe.AddForce(brute.transform.up * axeThrowUpwForce * Time.fixedDeltaTime, ForceMode.Impulse);
        }
    }

    void RotateAxe()
    {
        Vector3 axeRot = axe.transform.localEulerAngles;
        axeRot.x = -45;
        axeRot.y = -90+player.transform.eulerAngles.y;
        axeRot.z -= axeTorque;

        axe.transform.localEulerAngles = axeRot;
    }

    public void resetThrow()
    {
        kratos.ResetTrigger("throw");
        Movement.canWalk = true;
    }
    public void yeetAxe()
    {
        axe.gameObject.AddComponent<BoxCollider>();
        CameraShake.ShakeInstance.shakeCamera();
        axe.GetComponent<TrailRenderer>().enabled = true;
        axe.transform.parent = null;
        axe.isKinematic = false;
        canThrow = true;
    }
    public void stopYeet()
    {
        canThrow = false;
    }
}
