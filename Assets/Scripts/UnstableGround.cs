using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnstableGround : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 origin;
    Quaternion rot;
    public AudioClip audioClip;
    SoundManager soundManager;
    public float FallingAfterDelay;

    void Start()
    {
        rot = transform.rotation;
        origin = transform.position;
        rb = GetComponent<Rigidbody2D>();
        soundManager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SoundManager>();
        rb.gravityScale = 0;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Invoke("FallingPlatform",FallingAfterDelay); // play sound that indicates the ground is gonna fall
            Invoke("ResetPlatform", 5);
        }
    }

    void FallingPlatform()
    {
        soundManager.PlayClip(audioClip);
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 0.8f;
    }

    void ResetPlatform()
    {
        transform.position = origin;
        transform.rotation = rot;
        rb.bodyType = RigidbodyType2D.Static;
        rb.gravityScale = 0;
    }
}
