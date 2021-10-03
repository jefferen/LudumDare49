using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [Range(0.01f, 5.0f)]
    public float shakeStrength;
    [Range(5, 50)]
    public int Frequency;
    [Range(0.01f, 2)]
    public float time;
    CinemachineBasicMultiChannelPerlin Cam;

    public bool Shake;

    public void Awake()
    {
        Cam = GameObject.FindGameObjectWithTag("HelpCam").GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        StopTremble();
    }

    //private void Update() // for testing only
    //{
    //    if (Shake)
    //    {
    //        Shake = false;
    //        StartCoroutine(Tremble(time, shakeStrength));
    //    }
    //}

    public IEnumerator Tremble(float duration, float magnitude)
    {
        float t = 0;

        while (t < duration)
        {
            t += Time.deltaTime;
            Cam.m_AmplitudeGain = magnitude;
            Cam.m_FrequencyGain = Frequency;
            yield return new WaitForEndOfFrame();
        }
        StopTremble();
    }

    public void StopTremble()
    {
        Cam.m_AmplitudeGain = Cam.m_FrequencyGain = 0;
    }
}