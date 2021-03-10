using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMove : MonoBehaviour
{
    public Rigidbody2D me;
    public Vector2 speed;
    void Start()
    {
        me.velocity = speed;
    }
}
