using System.Collections;
using UnityEngine;

public class BrownOrkControl : MonoBehaviour {

    Vector3 pointA;
    Vector3 pointB;
    float fixedspeed = 1f;
    float speed;

    float rad = 2f;

    Rigidbody2D ork = null;
    Animator animator;

    enum Mode
    {
        GoToA,
        GoToB,
        Attack
    }
    Mode mode;

    // Use this for initialization
    void Start () {
        speed = fixedspeed;
        ork = this.GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
        mode = Mode.GoToA;
        pointA = this.transform.position;
        pointB = this.transform.position;
        pointB.Set(pointA.x + 2, pointB.y, 0);
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
        if (mode == Mode.GoToA)
        {
            sr.flipX = false;
            if (isArrived(pointA))
                mode = Mode.GoToB;
        }
        else if (mode == Mode.GoToB)
        {
            sr.flipX = true;
            if (isArrived(pointB))
                mode = Mode.GoToA;
        }
    }

    private float GetDirection()
    {
        Vector3 my_pos = this.transform.position;
        if (mode == Mode.GoToA)
        {
            if (my_pos.x < pointA.x)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }
        else if (mode == Mode.GoToB)
        {
            if (my_pos.x < pointB.x)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }
        return 0;
    }

    bool isArrived(Vector3 target)
    {
        Vector3 pos = this.transform.position;
        pos.z = 0;
        target.z = 0;
        return Vector3.Distance(pos, target) < 0.02f;
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.name == "Rabbit")
        {
            Debug.Log("Rabit entered");
            Vector3 rabit = RabbitControl.rabbit.transform.position;
            Vector3 ork = this.GetComponent<Collider2D>().bounds.size;
            if (rabit.y >= this.transform.position.y + ork.y / 2)
            {
                die();
            }
        }
    }

    void attack()
    {
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
