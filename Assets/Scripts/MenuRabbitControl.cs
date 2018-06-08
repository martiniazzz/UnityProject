using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuRabbitControl : MonoBehaviour {

    float len = 2;
    Vector3 pointA;
    Vector3 pointB;
    float speed = 0.8f;
    Animator animator;
    Rigidbody2D rabbit = null;

    enum Mode
    {
        GoToA,
        GoToB
    }

    Mode mode;

    private void Awake()
    {
        pointA = this.transform.localPosition;
        pointB = this.transform.localPosition;
        pointB.Set(pointB.x + len, pointB.y, 0);
    }

    // Use this for initialization
    void Start()
    {
        rabbit = this.GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
        mode = Mode.GoToA;
        animator.SetBool("run", true);
    }

    // Update is called once per frame
    void Update()
    {
        float value = this.GetDirection();
        if (Mathf.Abs(value) > 0)
        {
            Vector2 vel = rabbit.velocity;
            vel.x = value * speed;
            rabbit.velocity = vel;
        }

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (mode == Mode.GoToA)
        {
            sr.flipX = true;
            if (isArrived(pointA))
            {
                mode = Mode.GoToB;
            }

        }
        else if ( mode == Mode.GoToB)
        {
            sr.flipX = false;
            if (isArrived(pointB))
            {
                mode = Mode.GoToA;
            }
        }
    }

    private float GetDirection()
    {
        if (mode == Mode.GoToA)
        {
            return -1;
        }
        else if (mode == Mode.GoToB)
        {
            return 1;
        }
        return 0;
    }

    bool isArrived(Vector3 target)
    {
        Vector3 pos = this.transform.localPosition;
        pos.z = 0;
        target.z = 0;
        return Vector3.Distance(pos, target) < 0.2f;
    }
}
