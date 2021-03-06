﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenOrkControl : MonoBehaviour {

    public AudioClip attackSound = null;
    AudioSource source = null;

    public float len = 2;

    Vector3 pointA;
    Vector3 pointB;
    float fixedspeed = 1f;
    float speed;

    Rigidbody2D ork = null;
    Animator animator;

    enum Mode
    {
        GoToA,
        GoToB,
        Attack
    }
    Mode mode;
    bool isAttack = false;

    private void Awake()
    {
        pointA = this.transform.position;
        pointB = this.transform.position;
        pointB.Set(pointB.x + len, pointB.y, 0);
    }

    void Start () {
        source = gameObject.AddComponent<AudioSource>();
        source.clip = attackSound;

        speed = fixedspeed;
        ork = this.GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
        mode = Mode.GoToA;
        animator.SetBool("walk", true);
    }
	
	// Update is called once per frame
	void Update () {
        float value = this.GetDirection();
        if (Mathf.Abs(value) > 0)
        {
            Vector2 vel = ork.velocity;
            vel.x = value * speed;
            ork.velocity = vel;
        }

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (!isAttack && mode == Mode.GoToA)
        {
            sr.flipX = false;
            if (isArrived(pointA))
            {
                mode = Mode.GoToB;
            }
                
        }
        else if (!isAttack && mode == Mode.GoToB)
        {
            sr.flipX = true;
            if (isArrived(pointB))
            {
                mode = Mode.GoToA;
            }
        }

        Vector3 rabbit_pos = RabbitControl.rabbit.transform.position;
        Vector3 my_pos = this.transform.position;
        if (rabbit_pos.x > Mathf.Min(pointA.x, pointB.x) && rabbit_pos.x < Mathf.Max(pointA.x, pointB.x))
        {
            isAttack = true;
        }
        else
        {
            isAttack = false;
        }
    }

    private float GetDirection()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Vector3 rabbit_pos = RabbitControl.rabbit.transform.position;
        Vector3 my_pos = this.transform.position;
        if (isAttack)
        {
            if (my_pos.x < rabbit_pos.x)
            {
                sr.flipX = true;
                return 1;
            }
            else
            {
                sr.flipX = false;
                return -1;
            }
        }
        else if (mode == Mode.GoToA)
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
        Vector3 pos = this.transform.position;
        pos.z = 0;
        target.z = 0;
        return Vector3.Distance(pos, target) < 0.2f;
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.name == "Rabbit")
        {
            Vector3 rabit = RabbitControl.rabbit.transform.position;
            Vector3 ork = this.GetComponent<Collider2D>().bounds.size;
            if (rabit.y >= this.transform.position.y + ork.y / 2)
            {
                die();
            }
            else
            {
                attack();
                RabbitControl.rabbit.die();
            }
        }
    }

    void attack()
    {
        if (PlayerPrefs.GetInt("sound", 1) == 1)
            source.Play();
        this.animator.SetTrigger("attack");
        this.speed = 0;
        StartCoroutine(WaitForAttack());
        this.speed = fixedspeed;
    }

    void die()
    {
        animator.SetBool("death", true);
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        Destroy(this.gameObject);
    }

    IEnumerator WaitForAttack()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
    }

}
