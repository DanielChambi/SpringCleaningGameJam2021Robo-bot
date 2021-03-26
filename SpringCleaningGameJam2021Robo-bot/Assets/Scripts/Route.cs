using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{
    [SerializeField]
    Transform[] controlPoints;

    private void OnDrawGizmos()
    {
        /*Draw 2d representation of route*/
        for (int i = 0; i < controlPoints.Length-1; i++)
        {
            Vector2 pointStart = new Vector2(controlPoints[i].position.x, controlPoints[i].position.y);
            Vector2 pointEnd = new Vector2(controlPoints[i+1].position.x, controlPoints[i+1].position.y);

            Gizmos.DrawLine(pointStart, pointEnd);
        }
    }

    /*Return list of 2D points in route*/
    public Vector2[] RoutePoints2D()
    {
        Vector2[] points = new Vector2[controlPoints.Length];

        for(int i = 0; i < controlPoints.Length; i++)
        {
            points[i] = new Vector2(controlPoints[i].position.x, controlPoints[i].position.y);
        }

        return points;
    }

    public int RouteLength()
    {
        return controlPoints.Length;
    }

    public Transform RoutePointIndex(int index)
    {
        return controlPoints[index];
    }
}
