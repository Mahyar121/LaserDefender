using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Position : MonoBehaviour
{
    // Gizmos is the outline of the objects, The camera has the square gizmo
    private void OnDrawGizmos()
    {
        // (Location, Radius)
        Gizmos.DrawWireSphere(transform.position, 1);
    }
    
}
