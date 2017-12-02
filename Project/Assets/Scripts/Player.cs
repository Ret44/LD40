using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using InControl;
using Vectrosity;

public class Player : MonoBehaviour
{
    public static Player instance;
    public Collider2D mainCollider;

    public List<Coin> coinsInRange;

    public Queue<Coin> coinsInventory;

    public ParticleSystem regularSweat;
    public ParticleSystem excesiveSweat;

    private Vector3 aimingAngle;
    private VectorLine aimingVector;
    public Transform aim;

    private float predictTime;
    public float throwVelocity;

    public Material aimingVectorMaterial;

    public void Awake()
    {
        coinsInventory = new Queue<Coin>();
        coinsInRange = new List<Coin>();       
        instance = this;
        aimingVector = new VectorLine("AimingVector", new List<Vector2>(), 2);
        aimingVector.material = aimingVectorMaterial;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Coin")
        {
            Coin coin = collision.transform.parent.GetComponent<Coin>();
            coinsInRange.Add(coin);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            Coin coin = collision.transform.parent.GetComponent<Coin>();
            coinsInRange.Remove(coin);
        }
    }

    public void PickUp(Coin coin)
    {
        coinsInRange.Remove(coin);
        coinsInventory.Enqueue(coin);
        coin.gameObject.SetActive(false);
        if(coinsInventory.Count>2 && coinsInventory.Count<=4)
        {
            regularSweat.Play();            
        }
        else if(coinsInventory.Count>4)
        {
            excesiveSweat.Stop();
        }       

    }

    public void Aim()
    {        
        Vector3 momentum = aimingAngle * throwVelocity;
        Vector3 pos = gameObject.transform.position;
        Vector3 last = gameObject.transform.position;
        List<Vector2> position = new List<Vector2>();
        for (int i = 0; i < (int)(predictTime * 60); i++)
        {
            momentum += new Vector3(0f, -9.84f, 0f);
            pos += momentum;
            //Gizmos.DrawLine(last, pos); 
            position.Add(pos);
            last = pos;
        }
        aimingVector.points2 = position;
        aimingVector.Draw();
    }

    public void Throw()
    {

    }

    public void Update()
    {
        if(coinsInRange.Count>0)
        {
            if(InputManager.ActiveDevice.Action2.WasPressed)
            {
                PickUp(coinsInRange[0]);
            }
        }
        
        aimingAngle = new Vector3(0, 0, Mathf.Atan2(InputManager.ActiveDevice.RightStick.Y, InputManager.ActiveDevice.RightStick.X) * 180 / Mathf.PI);
        if(InputManager.ActiveDevice.RightTrigger.IsPressed)
        {
            aim.rotation = Quaternion.Euler(aimingAngle);
            Aim();
        }
    }

}