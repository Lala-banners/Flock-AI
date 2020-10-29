using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    [SerializeField] public List<Transform> waypoints;

    public float radius;

    [SerializeField] private Vector3 gizmoSize = Vector3.zero;

    private void OnDrawGizmos()
    {
        if (waypoints == null || waypoints.Count == 0)
        {
            return; //return exists loop
        }

        for (int i = 0; i < waypoints.Count; i++)
        {
            Transform waypoint = waypoints[i];

            //Check if waypoints[i] exists
            if (waypoint == null)
            {
                continue; //continue goes to next loop
            }
            Gizmos.color = Color.cyan; //Make color of the cube cyan
            Gizmos.DrawCube(waypoint.position, gizmoSize);


            //draw line to next waypoint & check if it exists
            if (i + 1 < waypoints.Count && waypoints[i + 1] != null)
            {
                //Draw line
                Gizmos.DrawLine(waypoint.position, waypoints[i + 1].position);
            }
        }
    }
}
