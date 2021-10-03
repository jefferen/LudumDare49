using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    float h, v;
    Vector2 JumpDir, LaunchDir;
    public GameObject JumpArrowSprite;
    Camera MyCamera;
    Rigidbody2D rb;
    Simple2DPlatformMovement PlayerMovement;
    bool CheckingForInput = false;
    public float JumpLaunchingSpeed;
    float t;
    public AudioClip audioClipZone, audioClipDash;
    SoundManager soundManager;

    void Start()
    {
        LaunchDir = Vector2.up;
        rb = GetComponent<Rigidbody2D>();
        PlayerMovement = GetComponent<Simple2DPlatformMovement>();
        soundManager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SoundManager>();
        MyCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    public IEnumerator ActivateJumpSequence(Vector3 launchArrowPos)
    {
        rb.velocity = Vector2.zero;
        Time.timeScale = 0;  // if it is between 0 and 1 the framerate looks weird, as it is lagging
        t = 0;
        CheckingForInput = true;
        soundManager.PlayClip(audioClipZone);

        PlaceJumpArrow(launchArrowPos);

        while (t <= 2) // doesn't allow player input checking but can check for the horizontal and vertical axis, no fair :(
        {
            t += Time.unscaledDeltaTime;
            PlayerInput();
            RotateJumpArrow();
            yield return new WaitForEndOfFrame();
        }
        Time.timeScale = 1;
        CheckingForInput = false;
        JumpLaunching();
    }

    void DisplayArrow(bool Display = true)
    {
        JumpArrowSprite.SetActive(Display);
    }

    public void PlaceJumpArrow(Vector3 launchArrowPos)
    {
        DisplayArrow();
        var newPos = MyCamera.WorldToScreenPoint(launchArrowPos);
        JumpArrowSprite.transform.position = newPos;
    }

    public void PlayerInput()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        JumpDir = new Vector2(h, v);
    }

    private void Update()
    {
        if (!CheckingForInput) return;

        if(Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Q))
        {
            if((Input.GetKeyUp(KeyCode.Mouse0) || Input.GetKeyUp(KeyCode.Q)))
            {
                t = 20;
            }
        }
        else if (Input.GetAxisRaw("RightTrigger") != 1) //   Input.GetKeyUp(KeyCode.Joystick1Button2)
        {
            t = 20;
        }
    }

    public void RotateJumpArrow()
    {
        if(JumpDir.magnitude > 0)
        {
            LaunchDir = JumpDir;
            float rotation = (Mathf.Atan2(JumpDir.y, JumpDir.x) * Mathf.Rad2Deg) +- 90; // this is ultimately fine
            JumpArrowSprite.transform.rotation = Quaternion.Euler(0, 0, rotation);
        }
    }

    public void JumpLaunching()
    {
        soundManager.PlayClip(audioClipDash);
        DisplayArrow(false);
        PlayerMovement.Dashing();
        rb.velocity = LaunchDir.normalized * JumpLaunchingSpeed;
    }
}
