using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ElRaccoone.Tweens;
using ElRaccoone.Tweens.Core;

public class MovingPlatform : MonoBehaviour
{
    public float travelSpeed = 5f;

    public Vector3[] waypoints;
    private int currentWaypoint = 0;
    private bool goingForward = true;

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

            // calculate the distance to the next waypoint
            float distanceToNextWaypoint = Vector3.Distance(transform.position, waypoints[currentWaypoint]);

            // calculate the duration of the Tween based on the distance and travel speed
            float duration = distanceToNextWaypoint / travelSpeed;

            // Tween the platform to the next waypoint with the calculated duration
            transform.TweenPosition(waypoints[currentWaypoint], duration).SetEase(EaseType.Linear);
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
