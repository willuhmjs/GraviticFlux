using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ElRaccoone.Tweens;

public class CubeScript : MonoBehaviour
{
    public float jumpForce = 8;
    public float WORLD_MIN_X = -7.5f;
	public float WORLD_MIN_Y = -5.5f;
	public float WORLD_MAX_X =  20.0f;
	public float WORLD_MAX_Y =  7.5f;

	// Utility objects to limit the positions
	PositionClamp spriteClamp;
	PositionClamp cameraClamp;

    // position for resetting
    Vector3 startPos;

    Rigidbody2D cubeRigid;

    bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        // set up PositionClamp to limit sprite position within world boundaries
		spriteClamp = new PositionClamp(WORLD_MIN_X, WORLD_MIN_Y, WORLD_MAX_X, WORLD_MAX_Y, GetComponent<Renderer>());

		// set up PositionClamp to limit camera position within world boundaries
		cameraClamp = new PositionClamp(WORLD_MIN_X, WORLD_MIN_Y, WORLD_MAX_X, WORLD_MAX_Y, Camera.main);
    
        // setup startpos
        startPos = transform.position;

        cubeRigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate() {
        // move the object forward or backwards according to the horizontal axis
        transform.Translate(new Vector3(Input.GetAxis("Horizontal") * Time.deltaTime * 5 * cubeRigid.gravityScale,0,0));
        spriteClamp.movementLimiter(transform.position, gameObject.transform);
		cameraClamp.movementLimiter(transform.position, Camera.main.transform);
    }

    void Update() { 
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (isPaused) {
                Time.timeScale = 1;
                isPaused = false;
            } else {
                Time.timeScale = 0;
                isPaused = true;
            }
            //Application.Quit();
        }
        if (isPaused) return;
        
        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && Mathf.Abs(GetComponent<Rigidbody2D>().velocity.y) < 0.001f) {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, cubeRigid.gravityScale*jumpForce), ForceMode2D.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.F)) {
            FlipGravity();
        }
        
        if (Input.GetKeyDown(KeyCode.R)) {
            Reset();
            //Application.Quit();
        }
    }

    public void FlipGravity() {
        // do not rotate if flipping is in progress
        if (Camera.main.transform.rotation.z != 0 && Camera.main.transform.rotation.z != 1) return;

        float rotation = 0;
        if (Camera.main.transform.rotation.z == 0) 
        { 
            rotation = -180; 
            cubeRigid.gravityScale = -1;            
        } else if (Camera.main.transform.rotation.z == 1) {
            cubeRigid.gravityScale = 1;            
        }
        cubeRigid.velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0);
        Camera.main.TweenRotationZ(rotation, 0.25f);
    }

    public void Reset() {
        transform.position = startPos;
        if (Camera.main.transform.rotation.z != 0) FlipGravity();
    }    
}
