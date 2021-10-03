using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatedJump : MonoBehaviour
{
    public PlayerJump Player;
    public string JumpTag;

    public bool CanBeHit = true;
    bool InTriggerZone = false;

    Collider2D target;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!CanBeHit) return;
        if (gameObject.CompareTag("WillDoDmg")) return;

        if (other.gameObject.CompareTag(JumpTag))
        {
            target = other;
            InTriggerZone = true;
            CanBeHit = false;
        }
    }

    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Q) || Input.GetAxisRaw("RightTrigger") >  0) && InTriggerZone)
        {
            Dash();
        }
    }

    void Dash()
    {
        StartCoroutine(Player.ActivateJumpSequence(target.transform.position));
        target.transform.parent.gameObject.GetComponent<CircleCollider2D>().enabled = target.enabled = false;
        if(target != null) target.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
        NoTarget();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        NoTarget();
    }

    void NoTarget()
    {
        CanBeHit = true;
        target = null;
        InTriggerZone = false;
    }
}
