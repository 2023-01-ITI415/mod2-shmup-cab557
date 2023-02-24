using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Checks whether a GameObject is on screen and can force it to stay on screen.
/// Note that this ONLY works for an orthographic Main Camera.
/// </summary>
public class BoundsCheck : MonoBehaviour {
     [System.Flags]
     public enum eScreenLocs { 
                                        // a
         onScreen = 0,  // 0000 in binary (zero)
         offRight = 1,  // 0001 in binary
         offLeft = 2,  // 0010 in binary
         offUp = 4,  // 0100 in binary
         offDown = 8   // 1000 in binary
     }

    public enum eType { center, inset, outset};

    [Header("Inscribed")]
    public eType boundsType = eType.center;
    public float radius = 1f;
    public bool keepOnScreen = true;

    [Header("Dynamic")]
    public float camWidth;
    public float camHeight;
    public eScreenLocs screenLocs = eScreenLocs.onScreen;
    // public bool isOnScreen = true;

    // Start is called before the first frame update
    void Awake()
    {
        camHeight = Camera.main.orthographicSize;
        camWidth = camHeight * Camera.main.aspect;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float checkRadius = 0;
        if (boundsType == eType.inset) checkRadius = -radius;
        if (boundsType == eType.outset) checkRadius = radius;

    
        Vector3 pos = transform.position;
        screenLocs = eScreenLocs.onScreen;
        //isOnScreen = true;

        if (pos.x > camWidth + checkRadius)
        {
            pos.x = camWidth + checkRadius;
            screenLocs |= eScreenLocs.offRight;
            //isOnScreen = false;
        }
        if (pos.x < -camWidth - checkRadius)
        {
            pos.x = -camWidth -checkRadius;
            screenLocs |= eScreenLocs.offLeft;
            // isOnScreen = false;
        }

        if (pos.y > camHeight + checkRadius)
        {
            pos.y = camHeight + checkRadius;
            screenLocs |= eScreenLocs.offUp;
            //isOnScreen = false;
        }

        if (pos.y < -camHeight - checkRadius)
        {
            pos.y = -camHeight - checkRadius;
            screenLocs |= eScreenLocs.offDown;
            //isOnScreen = false;
        }
        if (keepOnScreen && !isOnScreen)
        {
            transform.position = pos;
            screenLocs = eScreenLocs.onScreen;
            //isOnScreen = true;
        }
        }
    public bool isOnScreen
    {                                                  // e
         get { return (screenLocs == eScreenLocs.onScreen); }
     }
    public bool LocIs(eScreenLocs checkLoc)
    {
         if (checkLoc == eScreenLocs.onScreen) return isOnScreen;          // a
         return ((screenLocs & checkLoc) == checkLoc);                     // b
     }
}
