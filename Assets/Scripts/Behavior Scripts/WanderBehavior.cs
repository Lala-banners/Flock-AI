using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Wander")]
public class WanderBehavior : FilteredFlockBehavior //Check if following a path
{
    #region VARIABLES
    Path path = null;
    int currentWaypoint = -1; //not at a waypoint

    Vector2 waypointDirection = Vector2.zero;
    #endregion

    #region FUNCTIONS
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        //Find a path and follow the path
        if (path == null)
        {
            FindPath(agent, context);
        }
        return Vector3.zero; 
        //FollowPath();
    }

    //If inside radius of a path move to next path
    public bool InRadius(FlockAgent agent)
    {
        //Direction from a to b
        //b - a

        //Direction to the waypoint - current direction
        waypointDirection = (Vector2)path.waypoints[currentWaypoint].position - (Vector2)agent.transform.position;

        //waypointDirection.magnitude would give distance of Vector
        if(waypointDirection.magnitude < path.radius)
        {
            return true;

        }
        else
        {
            return false;
        }
    }

    private Vector2 FollowPath(FlockAgent agent)
    {
        if (path == null)
        {
            return Vector2.zero;
        }

        if(InRadius(agent))
        {
            currentWaypoint++; //go to next waypoint
            if (currentWaypoint >= path.waypoints.Count)
            {
                currentWaypoint = 0;
            }

            return Vector2.zero;
        }

        //If outside radius of waypoint, find a new path
        return waypointDirection;

    }

    public void FindPath(FlockAgent agent, List<Transform> context)
    {
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);

        //If cant find a path then return, if can find a path, set the variable
        if (filteredContext.Count == 0)
        {
            return;
        }

        if (currentWaypoint == -1)
        {
            currentWaypoint = 0;
        }

        //Find path in an area around each AI
        //If finds multiple paths, choose random one
        int randomPath = Random.Range(0, filteredContext.Count);
        path = filteredContext[randomPath].GetComponentInParent<Path>();
    }

    #endregion

}
