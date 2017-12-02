using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : PhysicsObject {

    public bool isPickable;
    public Collider2D mainCollider;

    [SerializeField]    
    private bool _isDeadly;
    public bool isDeadly
    {
        get
        {
            return _isDeadly;
        }
        set
        {
            Physics2D.IgnoreCollision(mainCollider, Player.instance.mainCollider, value);
            _isDeadly = value;
        }
    }



    public void OnCollisionEnter2D(Collision2D collision)
    {
        isDeadly = false;
    }
    
}
