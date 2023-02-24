using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Inscribed")]
     public float speed = 10f;   // The movement speed is 10m/s
     public float fireRate = 0.3f;  // Seconds/shot (Unused)
     public float health = 10;    // Damage needed to destroy this enemy
     public int score = 100;   // Points earned for destroying this
 
     // This is a Property: A method that acts like a field
     public Vector3 Pos
    {                                                       // a
         get
         {
             return this.transform.position;
         }
         set
         {
         this.transform.position = value;
         }
     }
 
     void Update()
     {
        Move();                                                                // b
     }
 
     public virtual void Move()
     { // c
        Vector3 tempPos = Pos;
        tempPos.y -= speed * Time.deltaTime;
        Pos = tempPos;
     }
}
