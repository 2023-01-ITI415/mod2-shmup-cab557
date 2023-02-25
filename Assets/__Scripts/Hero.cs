using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    static public Hero S { get; private set; }

    [Header("Inscribed")]
    public float speed = 30;
    public float rollMult = -45;
    public float pitchMult = 30;


    [Header("Dynamic")] [Range(0, 4)]
    [SerializeField]
   
     private float _shieldLevel = 1;  // Remember the underscore
    //public float shieldLevel = 1;
    [Tooltip("This field holds a reference to the last triggering GameObject")]
    private GameObject lastTriggerGo = null;                                  // a

    // Start is called before the first frame update
    void Awake()
    {
        if (S == null)
        {
            S = this;
        }
        else
        {
            Debug.LogError("Hero.Awake( - Attempted to assign second Hero.S!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        Vector3 pos = transform.position;
        pos.x += hAxis * speed * Time.deltaTime;
        pos.y += vAxis * speed * Time.deltaTime;
        transform.position = pos;

        transform.rotation = Quaternion.Euler(vAxis * pitchMult, hAxis * rollMult, 0);
    }
    void OnTriggerEnter(Collider other)
    {
        Transform rootT = other.gameObject.transform.root;                    // a
        GameObject go = rootT.gameObject;
        // Debug.Log("Shield trigger hit by: " + go.gameObject.name);
        // Make sure it’s not the same triggering go as last time
        if (go == lastTriggerGo) return;                                    // c
        lastTriggerGo = go;                                                   // d

        Enemy enemy = go.GetComponent<Enemy>();                               // e
        if (enemy != null)
        {  // If the shield was triggered by an enemy
            shieldLevel--;        // Decrease the level of the shield by 1
            Destroy(go);          // … and Destroy the enemy                  // f
        }
        else
        {
            Debug.LogWarning("Shield trigger hit by non-Enemy: " + go.name);    // g
        }
    }
         
         public float shieldLevel
    {
         get { return (_shieldLevel); }                                      // b
         private set
        {                                                         // c
 _shieldLevel = Mathf.Min(value, 4);                             // d
             // If the shield is going to be set to less than zero…
             if (value < 0)
            {                                                  // e
 Destroy(this.gameObject);  // Destroy the Hero
                Main.HERO_DIED();
            }
         }
     }

}