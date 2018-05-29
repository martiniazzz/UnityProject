using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    public Vector3 MoveBy;
    Vector3 pointA;
    Vector3 pointB;
    bool gointToA;

    public float waiting;
    public float speed = 4;

    float time_to_wait;

    void Start()
    {
        this.pointA = this.transform.position;
        this.pointB = this.pointA + MoveBy;
        gointToA = false;
        time_to_wait = waiting;
    }

    void Update () {
        time_to_wait -= Time.deltaTime;
        if (time_to_wait <= 0)
        {
            float step = speed * Time.deltaTime;

            Vector3 my_pos = this.transform.position;
            Vector3 target;
            if (gointToA)
            {
                target = this.pointA;
            }
            else
            {
                target = this.pointB;
            }

            transform.position = Vector3.MoveTowards(my_pos, target, step);

            Vector3 destination = target - my_pos;
            destination.z = 0;
            if (isArrived(my_pos, target))
            {
                gointToA = (gointToA == false ? true : false);
                time_to_wait = waiting;
            }
        }
    }

    bool isArrived(Vector3 pos, Vector3 target)
    {
        pos.z = 0;
        target.z = 0;
        return Vector3.Distance(pos, target) < 0.02f;
    }
}
