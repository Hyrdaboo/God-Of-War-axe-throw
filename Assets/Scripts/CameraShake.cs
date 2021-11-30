using Cinemachine;
using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    public static CameraShake ShakeInstance { get; private set; }
    [SerializeField]
    private CinemachineFreeLook FreeLook = null;

    [System.NonSerialized]
    private CinemachineBasicMultiChannelPerlin shake = null;

    public float ShakeIntensity, ShakeTime;
    private void Start()
    {
        ShakeInstance = this;
       shake = FreeLook.GetComponentInChildren<CinemachineBasicMultiChannelPerlin>();
        if (shake == null)
        {
            Debug.LogError("the fuck is wrong??");
        }
    }

    public void shakeCamera()
    {
        shake.m_AmplitudeGain = ShakeIntensity;

        StartCoroutine(wait());

        IEnumerator wait()
        {
            yield return new WaitForSeconds(ShakeTime);
            shake.m_AmplitudeGain = 0;
        }
    }
}
