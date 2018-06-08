using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RabbitControl : MonoBehaviour {

    public static RabbitControl rabbit = null;

    public float speed = 2;
    Rigidbody2D myBody = null;

    bool isGrounded = false;
    bool JumpActive = false;
    float JumpTime = 0f;
    public float MaxJumpTime = 2f;
    public float JumpSpeed = 2f;

    int lifes;

    Transform rabbitParent = null;

    bool isBig;
    Vector3 standartSize;
    Vector3 bigSize;
    int countBomb = 0;
    float timeToWait = 4;
    bool bombBonus = false;
    bool afterMushroom = false;

    Animator animator;

    // Use this for initialization
    void Start () {
        rabbit = this;
        myBody = this.GetComponent<Rigidbody2D>();
        LevelController.current.setStartPosition(transform.position);
        this.rabbitParent = this.transform.parent;
        isBig = false;
        standartSize = this.transform.localScale;
        bigSize.Set(standartSize.x * 1.5f, standartSize.y * 1.5f, standartSize.z);
        animator = this.GetComponent<Animator>();
        lifes = 3;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        if (afterMushroom && countBomb >= 1)
        {
            bombBonus = true;
            this.GetComponent<Renderer>().material.color = Color.red;
            timeToWait -= Time.deltaTime;
            if (timeToWait <= 0)
            {
                bombBonus = false;
                timeToWait = 4;
                countBomb = 0;
                afterMushroom = false;
                this.GetComponent<Renderer>().material.color = Color.white;
            }
        }
        
        float runVal = Input.GetAxis("Horizontal");
        if (Mathf.Abs(runVal) > 0)
        {
            Vector2 vel = myBody.velocity;
            vel.x = runVal * speed;
            myBody.velocity = vel;
        }
        if (Mathf.Abs(runVal) > 0 && !JumpActive && isGrounded)
            animator.SetBool("run", true);
        else
            animator.SetBool("run", false);
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (runVal < 0)
            sr.flipX = true;
        else if (runVal > 0)
            sr.flipX = false;
        float diff = Time.deltaTime;
        Vector3 from = transform.position + Vector3.up * 0.3f;
        Vector3 to = transform.position + Vector3.down * 0.1f;
        int layer_id = 1 << LayerMask.NameToLayer("Ground");
        RaycastHit2D hit = Physics2D.Linecast(from, to, layer_id);
        if (hit)
            isGrounded = true;
        else
            isGrounded = false;
        Debug.DrawLine(from, to, Color.red);
        if (Input.GetButtonDown("Jump") && isGrounded)
            this.JumpActive = true;
        if (this.JumpActive)
        {
            if (Input.GetButton("Jump"))
            {
                this.JumpTime += Time.deltaTime;
                if (this.JumpTime < this.MaxJumpTime)
                {
                    Vector2 vel = myBody.velocity;
                    vel.y = JumpSpeed * (1.0f - JumpTime / MaxJumpTime);
                    myBody.velocity = vel;
                }
            }
            else
            {
                this.JumpActive = false;
                this.JumpTime = 0;
            }
        }
        if (this.isGrounded)
            animator.SetBool("jump", false);
        else
            animator.SetBool("jump", true);

        RaycastHit2D hit_gr = Physics2D.Linecast(from, to, layer_id);
        if (hit_gr)
        {
            if (hit_gr.transform != null && hit_gr.transform.GetComponent<MovingPlatform>() != null)
            {
                SetNewParent(this.transform, hit_gr.transform);
            }
            else
            {
                SetNewParent(this.transform, hit_gr.transform);
            }
        }
        else
        {
            SetNewParent(this.transform, this.rabbitParent);
        }
    }

    static void SetNewParent(Transform obj, Transform new_parent)
    {
        if (obj.transform.parent != new_parent)
        {
            Vector3 pos = obj.transform.position;
            obj.transform.parent = new_parent;
            obj.transform.position = pos;
        }
    }

    public void setBig()
    {
        isBig = true;
        this.transform.localScale = bigSize;
    }

    public void setSmall()
    {
        isBig = false;
        this.transform.localScale = standartSize;
    }

    public bool getBig()
    {
        return isBig;
    }

    public void addBomb()
    {
        countBomb++;
    }

    public bool getBombBonus()
    {
        return bombBonus;
    }

    public void setAfterMushroom(bool val)
    {
        afterMushroom = val;
    }

    public bool getAfterMushroom()
    {
        return afterMushroom;
    }

    public void die()
    {
        lifes--;
        animator.SetBool("die", true);
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length + animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
        animator.SetBool("die", false);
        if(lifes > 0)
        {
            animator.SetBool("stay", true);
            LevelController.current.onRabitDeath(this);
        }
        else
            SceneManager.LoadScene("Levels");
    }

    public int getLifes()
    {
        return lifes;
    }

    public void fall()
    {
        lifes--;
    }

}
