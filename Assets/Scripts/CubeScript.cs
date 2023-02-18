using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ElRaccoone.Tweens;

public class CubeScript: MonoBehaviour {
    public float jumpForce = 8;
    public float WORLD_MIN_X = -7.5f;
    public float WORLD_MIN_Y = -5.5f;
    public float WORLD_MAX_X = 20.0f;
    public float WORLD_MAX_Y = 7.5f;

    // Utility objects to limit the positions
    PositionClamp spriteClamp;
    PositionClamp cameraClamp;

    // position for resetting
    Vector3 startPos;

    Rigidbody2D cubeRigid;

    // audio playing
    AudioSource audioSource;
    public AudioClip jumpClip;
    public AudioClip gravityClip;

    public int startAngle = 0;

    public bool gravityFlipCooldown = false;
    bool isOnCooldown = false;

    SettingsData settings;

    // Start is called before the first frame update
    void Start() {
        // set up PositionClamp to limit sprite position within world boundaries
        spriteClamp = new PositionClamp(WORLD_MIN_X, WORLD_MIN_Y, WORLD_MAX_X, WORLD_MAX_Y, GetComponent < Renderer > ());

        // set up PositionClamp to limit camera position within world boundaries
        cameraClamp = new PositionClamp(WORLD_MIN_X, WORLD_MIN_Y, WORLD_MAX_X, WORLD_MAX_Y, Camera.main);

        // setup startpos
        startPos = transform.position;

        cubeRigid = GetComponent < Rigidbody2D > ();

        audioSource = GetComponent < AudioSource > ();

        DataManagement.SaveLatestLevel();
        settings = DataManagement.LoadSettings();
    }

    // Update is called once per frame
    void FixedUpdate() {
        settings = DataManagement.LoadSettings();

        // move the object forward or backwards according to the horizontal axis
        float horizontal = 0.0f;
        float deceleration = 0.1f;

        if (Input.GetKey(settings.controls[PlayerAction.MoveLeft])) {
            horizontal = -1.0f;
        } else if (Input.GetKey(settings.controls[PlayerAction.MoveRight])) {
            horizontal = 1.0f;
        } else {
            horizontal = Mathf.Lerp(horizontal, 0, deceleration);
        }
        transform.Translate(new Vector3(horizontal * 4.2f * Time.deltaTime * Mathf.Sign(cubeRigid.gravityScale), 0, 0));
        
        spriteClamp.movementLimiter(transform.position, gameObject.transform);
        cameraClamp.movementLimiter(transform.position, Camera.main.transform);
    }

    void Update() {
        // still jump if player is a child of a moving platform
        if ((Input.GetKeyDown(settings.controls[PlayerAction.Jump])) && (Mathf.Abs(cubeRigid.velocity.y) < 0.001f || transform.parent != null)) {
            if (transform.parent != null) cubeRigid.velocity = new Vector2(cubeRigid.velocity.x, 0f);
            audioSource.clip = jumpClip;
            audioSource.Play();
           cubeRigid.AddForce(new Vector2(0, cubeRigid.gravityScale * jumpForce), ForceMode2D.Impulse);
        }

        if (Input.GetKeyDown(settings.controls[PlayerAction.FlipGravity])) {
            Debug.Log("GetKeyDown reports: " + isOnCooldown);
            if (!isOnCooldown) FlipGravity();
        }

        if (Input.GetKeyDown(settings.controls[PlayerAction.Reset])) {
            Reset();
        }
    }

   public void FlipGravity() {
    // do not rotate if flipping is in progress
    Debug.Log("FlipGravity reports ioc:" + isOnCooldown + " gfc: " + gravityFlipCooldown);
    if (gravityFlipCooldown) isOnCooldown = true;
    if (Camera.main.transform.rotation.z != 0 && Camera.main.transform.rotation.z != 1) return;
    audioSource.clip = gravityClip;
    audioSource.Play();
    float rotation = 0;
    if (Camera.main.transform.rotation.z == 0) 
    { 
        rotation = -180; 
        cubeRigid.gravityScale = -Mathf.Abs(cubeRigid.gravityScale);            
    } else if (Camera.main.transform.rotation.z == 1) {
        cubeRigid.gravityScale = Mathf.Abs(cubeRigid.gravityScale);            
    }
        // keep a small amount of y velocity using Mathf.Clamp
        float yVelocity = Mathf.Clamp(cubeRigid.velocity.y, -1f, 1f);
        cubeRigid.velocity = new Vector2(cubeRigid.velocity.x, yVelocity);
        Camera.main.TweenRotationZ(rotation, 0.25f);
        if (gravityFlipCooldown) Invoke("CooldownFalse", 1.5f);
    }

    void CooldownFalse() {
        isOnCooldown = false;
    }

    public void Reset() {
        CooldownFalse();
        transform.SetParent(null);
        if (Time.timeScale == 0) return;
        transform.position = startPos;
        if (Camera.main.transform.rotation.z != startAngle) FlipGravity();
        cubeRigid.velocity = Vector2.zero;
    }
}
