using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbRouteManager : MonoBehaviour
{
    [SerializeField]
    private Transform[] routePoints;
    private Vector2 gizmoPos;

    private void OnDrawGizmos()
    {
        for(float t = 0; t <=1; t+= 0.07f){
            gizmoPos = Mathf.Pow(1-t, 3) * routePoints[0].position + 
            3 *  Mathf.Pow(1-t, 2) * t * routePoints[1].position + 
            3 * (1 - t) * Mathf.Pow(t, 2) * routePoints[2].position +
            Mathf.Pow(t,3) * routePoints[3].position;

            Gizmos.DrawSphere(gizmoPos, 0.1f);

        }
    }

}
