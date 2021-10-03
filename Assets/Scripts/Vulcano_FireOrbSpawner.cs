using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vulcano_FireOrbSpawner : MonoBehaviour
{
    public Transform FirePoint;
    public GameObject FireOrb;
    public AudioClip audioClip;
    public float FireAttackSpeed;
    AudioSource AudioSource;

    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        InvokeRepeating("FireInTheHole",FireAttackSpeed,FireAttackSpeed);
    }


    void FireInTheHole()
    {
        Instantiate(FireOrb, FirePoint.transform.position, transform.rotation);
        AudioSource.Play();
    }
}
