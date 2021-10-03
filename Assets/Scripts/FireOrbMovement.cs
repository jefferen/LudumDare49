using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class FireOrbMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public int MoveSpeed, GravityScale;
    public AudioClip audioClip;
    SoundManager soundManager;
    public GameObject myChildObject;
    bool Hit;
    AudioSource AudioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * MoveSpeed;
        rb.gravityScale = GravityScale;
        AudioSource = GetComponent<AudioSource>();
        soundManager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SoundManager>();
    }

    private void OnCollisionEnter2D(Collision2D other) // I thought of a solution to the particle system problem, but at this point it's gonna take a lot of refactoring, don't have the time
    {
        if (Hit) return;
        Hit = true;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
        myChildObject.GetComponent<CircleCollider2D>().enabled = false;
        Destroy(gameObject.GetComponent<DealDmg>());             //arghghghghhg, the colliders are gone, can still take dmg, great, all because i want the particle system to sustain itself after hit
        AudioSource.Play();
        Invoke("DelayDestroy", 0.2f);
    }

    void DelayDestroy()
    {
        Destroy(gameObject);
    }
}
