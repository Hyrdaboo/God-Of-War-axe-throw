using UnityEngine;


public class AxeReturn : MonoBehaviour
{
    public Rigidbody axe;
    public static bool isReturning = false;
    public Transform curvePoint, target;
    public Animator axeAnimator;

    private Vector3 oldPos;
    private Vector3 savedPosition;
    private Vector3 savedRotation;
    private float time = 0.0f;
    public float returnSpeed = 2f;
    public float axeTorque;

    private void Start()
    {
        savedPosition = axe.transform.localPosition;
        savedRotation = axe.transform.localRotation.eulerAngles;
        savedRotation.y = 0;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1) && AxeThrow.hasThrown == true) axeReturnAnim();
        
        if (isReturning)
        {
            if (time < 1.0f)
            {
                axe.position = getBezierQuadraticCurvePoint(time, oldPos, curvePoint.position, target.position);
                time += returnSpeed * Time.deltaTime;
            }
            else
            {
                ResetAxe();
            }
        }
    }


    void axeReturnAnim()
    {
        axeAnimator.SetBool("catch", true);
    }

   public void axeReturn()
    {
        isReturning = true;
        axe.velocity = Vector3.zero;
        oldPos = axe.position;
        axe.interpolation = RigidbodyInterpolation.Extrapolate;
    }

    void ResetAxe()
    {
        CameraShake.ShakeInstance.shakeCamera();
        time = 0.0f;
        isReturning = false;
        AxeThrow.isHolding = true;
        AxeThrow.hasThrown = false;
        axe.isKinematic = true;
        axe.transform.parent = target;
        axe.transform.localPosition = savedPosition;
        axe.transform.localEulerAngles = savedRotation;
        axeAnimator.SetBool("catch", false);
        axe.interpolation = RigidbodyInterpolation.None;
        axe.GetComponent<TrailRenderer>().enabled = false;
        BoxCollider axeCol = axe.GetComponent<BoxCollider>();
        Destroy(axeCol);
    }

    public Vector3 getBezierQuadraticCurvePoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        Vector3 p = (uu * p0) + (2 * u * t * p1) + (tt * p2);
        return p;
    }
}
