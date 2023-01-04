using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityIntensifier : MonoBehaviour
{
    GameObject cube;
    Rigidbody2D rb;
    public float intensity = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        cube = GameObject.FindGameObjectWithTag("Player");   
        rb = cube.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        
    }

    // When the player enters the trigger, the gravity is increased
    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject == cube) {
            if (Mathf.Sign(rb.gravityScale) == 1) {
                rb.gravityScale = intensity;
            } else {
                rb.gravityScale = -intensity;
            }
        }
    }

    // When the player exits the trigger, the gravity is reset
    void OnTriggerExit2D(Collider2D other) {
       if (other.gameObject == cube) {
            if (Mathf.Sign(rb.gravityScale) == 1) {
                rb.gravityScale = 1;
            } else {
                rb.gravityScale = -1;
            }
        }
    }
}