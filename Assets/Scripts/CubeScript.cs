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
        if (Input.GetKey(settings.controls[PlayerAction.MoveLeft])) {
            transform.Translate(new Vector3(-Time.deltaTime * 4.2f * Mathf.Sign(cubeRigid.gravityScale), 0, 0));
        } else if (Input.GetKey(settings.controls[PlayerAction.MoveRight])) {
            transform.Translate(new Vector3(Time.deltaTime * 4.2f * Mathf.Sign(cubeRigid.gravityScale), 0, 0));
        }

        if (Input.GetKeyUp(settings.controls[PlayerAction.MoveLeft])) {
           // add a bit of velocity to the left to make the cube feel more responsive
            GetComponent < Rigidbody2D > ().velocity = new Vector2(-0.5f, GetComponent < Rigidbody2D > ().velocity.y);
        } else if (Input.GetKeyUp(settings.controls[PlayerAction.MoveRight])) {
            // add a bit of velocity to the right to make the cube feel more responsive
            GetComponent < Rigidbody2D > ().velocity = new Vector2(0.5f, GetComponent < Rigidbody2D > ().velocity.y);
        }
        
        spriteClamp.movementLimiter(transform.position, gameObject.transform);
        cameraClamp.movementLimiter(transform.position, Camera.main.transform);
    }

    void Update() {
        if ((Input.GetKeyDown(settings.controls[PlayerAction.Jump])) && Mathf.Abs(GetComponent < Rigidbody2D > ().velocity.y) < 0.001f) {
            audioSource.clip = jumpClip;
            audioSource.Play();
            GetComponent < Rigidbody2D > ().AddForce(new Vector2(0, cubeRigid.gravityScale * jumpForce), ForceMode2D.Impulse);
        }

        if (Input.GetKeyDown(settings.controls[PlayerAction.FlipGravity])) {
            FlipGravity();
        }

        if (Input.GetKeyDown(settings.controls[PlayerAction.Reset])) {
            Reset();
        }
    }

   public void FlipGravity() {
    // do not rotate if flipping is in progress
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
    }

    public void Reset() {
        if (Time.timeScale == 0) return;
        transform.position = startPos;
        if (Camera.main.transform.rotation.z != startAngle) FlipGravity();
        cubeRigid.velocity = Vector2.zero;
    }
}
