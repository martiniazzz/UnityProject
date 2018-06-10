using System.Collections;
using UnityEngine;

public class BrownOrkControl : MonoBehaviour {

    public AudioClip attackSound = null;
    AudioSource source = null;

    public float len = 3;
    public GameObject prefabCarrot;

    Vector3 pointA;
    Vector3 pointB;
    float fixedspeed = 1f;
    float speed;

    float rad = 5f;

    Rigidbody2D ork = null;
    Animator animator;

    float last_carrot = 0;
    float timeToThrow = 3;

    enum Mode
    {
        GoToA,
        GoToB,
        Attack
    }
    Mode mode;
    bool isAttack;

    void Start () {
        source = gameObject.AddComponent<AudioSource>();
        source.clip = attackSound;

        isAttack = false;
        speed = fixedspeed;
        ork = this.GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
        mode = Mode.GoToA;
        pointA = this.transform.position;
        pointB = this.transform.position;
        pointB.Set(pointB.x + len, pointB.y, 0);
        animator.SetBool("walk", true);
    }
	
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
                mode = Mode.GoToB;
        }
        else if (!isAttack && mode == Mode.GoToB)
        {
            sr.flipX = true;
            if (isArrived(pointB))
                mode = Mode.GoToA;
        }

        Vector3 rabbit_pos = RabbitControl.rabbit.transform.position;
        Vector3 my_pos = this.transform.position;
        float direction = 0;
        if (Mathf.Abs(rabbit_pos.x - my_pos.x) <= rad)
        {
            isAttack = true;
            this.speed = 0;
            this.animator.SetBool("walk", false);
            this.animator.SetBool("stay", true);
            if (my_pos.x < rabbit_pos.x)
            {
                sr.flipX = true;
                direction = 1;
            }
            else
            {
                sr.flipX = false;
                direction = -1;
            }
            if (Time.time - last_carrot >= timeToThrow)
            {
                this.attack(direction);
                last_carrot = Time.time;
            }
        }
        else
        {
            isAttack = false;
            this.speed = fixedspeed;
            animator.SetBool("walk", true);
        }
    }

    private float GetDirection()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Vector3 my_pos = this.transform.position;
        if (isAttack)
            return 0;
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
        }
    }

    void attack(float direction)
    {
        if (PlayerPrefs.GetInt("sound", 1) == 1)
            source.Play();
        this.animator.SetTrigger("attack");
        StartCoroutine(WaitForAttack());
        throwCarrot(direction);
    }

    void throwCarrot(float direction)
    {
        GameObject obj = GameObject.Instantiate(this.prefabCarrot);
        Vector3 orkSize = this.GetComponent<Collider2D>().bounds.size;
        Transform carrotPos = this.transform;
        float x = carrotPos.position.x + 0.5f;
        float y = carrotPos.position.y + orkSize.y / 2;
        obj.transform.position = new Vector3(x, y, 0.0f);
        Carrot carrot = obj.GetComponent<Carrot>();
        carrot.launch(direction);
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
