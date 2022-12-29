using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ElRaccoone.Tweens;
using ElRaccoone.Tweens.Core;

public class MovingPlatform : MonoBehaviour
{
    public float TIME = 1.75f;

    public float START_X;
    public float END_X;

    public float START_Y;
    public float END_Y;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // check to see if the object is at one end (start or end)
        // tween it to the other end if so
        if (transform.position.x == START_X && transform.position.y == START_Y)
        {
            transform.TweenPositionX(END_X, TIME).SetEase(EaseType.Linear);
            transform.TweenPositionY(END_Y, TIME).SetEase(EaseType.Linear);
        }
        else if (transform.position.x == END_X && transform.position.y == END_Y)
        {
            transform.TweenPositionX(START_X, TIME).SetEase(EaseType.Linear);
            transform.TweenPositionY(START_Y, TIME).SetEase(EaseType.Linear);
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