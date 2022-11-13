using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    public float Speed = 50f;
    public int Direction = 1;
    void Update()
    {
        transform.position += (Direction * transform.right) * Time.deltaTime * Speed;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player_Blue") {
            Destroy(gameObject);
        }
    }
}
