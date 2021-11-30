using UnityEngine;

public class collisionDetector : MonoBehaviour
{
    public Rigidbody axe;
    public Transform colDetect;
    public Camera cam;
    public LayerMask WhatIsCollidable;

    private void Update()
    {
        if (Physics.CheckSphere(colDetect.transform.position, .5f, WhatIsCollidable))
        {
            axe.isKinematic = true;
        }
    }
}