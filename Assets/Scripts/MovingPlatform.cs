using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ElRaccoone.Tweens;
using ElRaccoone.Tweens.Core;

public class MovingPlatform : MonoBehaviour
{
    public float TIME = 1.75f;

    public Vector3[] waypoints;
    private int currentWaypoint = 0;
    private bool goingForward = true;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // check to see if the object is at the current waypoint
        // tween it to the next or previous waypoint if so
        if (transform.position == waypoints[currentWaypoint])
        {
            if (currentWaypoint == waypoints.Length - 1 && goingForward)
            {
                goingForward = false;
                currentWaypoint--;
            }
            else if (currentWaypoint == 0 && !goingForward)
            {
                goingForward = true;
                currentWaypoint++;
            }
            else if (goingForward)
            {
                currentWaypoint++;
            }
            else
            {
                currentWaypoint--;
            }
            transform.TweenPosition(waypoints[currentWaypoint], TIME).SetEase(EaseType.Linear);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.transform.SetParent(transform);
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
