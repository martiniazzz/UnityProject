using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : Collectable {

    public float speed = 1;
    public float timeToGone = 3;

    Rigidbody2D carrot = null;

    protected override void OnRabitHit(RabbitControl rabit)
    {
        rabit.die();
        this.CollectedHide();
    }

    void Start()
    {
        StartCoroutine(destroyLater());
    }

    IEnumerator destroyLater()
    {
        yield return new WaitForSeconds(timeToGone);
        Destroy(this.gameObject);
    }

    public void launch(float direction)
    {
        carrot = this.GetComponent<Rigidbody2D>();
        if (Mathf.Abs(direction) > 0)
        {
            Vector2 vel = carrot.velocity;
            vel.x = direction * speed;
            carrot.velocity = vel;
        }
    }
}
