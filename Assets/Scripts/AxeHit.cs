using UnityEngine;

public class AxeHit : MonoBehaviour
{
    public GameObject destructible;
    public LayerMask axe;
    public Transform detector;
    public float axeHitForce;
    public Rigidbody axeRb;

    private void Update()
    {
        if (Physics.CheckBox(detector.transform.position, destructible.transform.localScale, Quaternion.identity, axe)) 
        {
            Destruct();
        }
    }
    void Destruct()
    {
        foreach (Transform child in destructible.transform)
        {
            if (child.GetComponent<Rigidbody>() == null) child.gameObject.AddComponent<Rigidbody>();
            if (child.GetComponent<MeshCollider>() == null) child.gameObject.AddComponent<MeshCollider>().convex = true;
            child.gameObject.GetComponent<Rigidbody>().AddForce(axeRb.velocity * axeHitForce * Time.deltaTime * Random.Range(0, 5));
        }
    }
}
