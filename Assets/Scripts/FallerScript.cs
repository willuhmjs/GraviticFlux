using System.Collections;
using System.Linq;
using UnityEngine;
using ElRaccoone.Tweens;
using ElRaccoone.Tweens.Core;

public class FallerScript : MonoBehaviour
{
    Rigidbody2D rb;
    BoxCollider2D bc;

    public float travelSpeed = 2f;
    public GameObject[] possibleTargets;
    Vector2 originalPosition;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        
        rb.bodyType = RigidbodyType2D.Static;
        originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(Collision(collision));
    }

    IEnumerator Collision(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                Debug.Log("Contact: " + contact.normal.y);
                if (contact.normal.y < 0)
                {
                    collision.gameObject.transform.SetParent(transform);
                    rb.bodyType = RigidbodyType2D.Dynamic;
                    rb.gravityScale = 1f;
                    gameObject.TweenCancelAll();
                    break;
                } else if (contact.normal.y > 0) {
                    // fall opposite direction
                    collision.gameObject.transform.SetParent(transform);
                    rb.bodyType = RigidbodyType2D.Dynamic;
                    rb.gravityScale = -1f;
                    gameObject.TweenCancelAll();
                }
            }
        } else if (possibleTargets.Contains<GameObject>(collision.gameObject)) {
            // go back to original position
            // if player is a child, wait until it leaves to go back
            while (transform.Find("Player") != null) {
                yield return null;
            }
            Debug.Log("Moving back!");
            rb.gravityScale = 0;
            rb.bodyType = RigidbodyType2D.Static;
            float distanceToNextWaypoint = Vector2.Distance(transform.position, originalPosition);
            float duration = distanceToNextWaypoint / travelSpeed;
            transform.TweenPosition(originalPosition, duration).SetEase(EaseType.Linear);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.transform.SetParent(null);
        }
    }
}
