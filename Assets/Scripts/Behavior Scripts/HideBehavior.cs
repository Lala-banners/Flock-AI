using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//If enemy nearby, AI will find an obstacle and hide behind it

[CreateAssetMenu(menuName = "Flock/Behavior/Hide")]

public class HideBehavior : FilteredFlockBehavior
{
    #region Variables
    public ContextFilter obstacleFilter;
    public float hideBehindObstacleDist = 2f;
    #endregion

    #region Functions
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, FlockLife flock)
    {
        if (context.Count == 0)
        {
            return Vector2.zero;
        }

        //Filter obstacles and enemy AI is hiding from - filter from parent class
        List<Transform> filteredContext = (filter == null) ? context: filter.Filter(agent, context);

        //Hide behind
        List<Transform> obstacleContext = (filter == null) ? context: obstacleFilter.Filter(agent, context);

        if (filteredContext.Count == 0)
        {
            return Vector2.zero;
        }

        //find nearest obstacle to hide behind
        float nearestDistance = float.MaxValue;
        Transform nearestObstacle = null;
        foreach (Transform item in obstacleContext)
        {
            float distance = Vector2.Distance(item.position, agent.transform.position);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestObstacle = item;
            }
        }

        //if no obstacle
        if (nearestObstacle == null)
        {
            return Vector2.zero;
        }

        //find best hiding spot
        Vector2 hidePosition = Vector2.zero;

        //Looking through every enemy and finding opposite direction from the enemy to obstacle
        foreach (Transform item in filteredContext)
        {
            
            Vector2 obstacleDirection = nearestObstacle.position - item.position;

            obstacleDirection = obstacleDirection.normalized * hideBehindObstacleDist; //Give direction magnitude of 1

            hidePosition += ((Vector2)nearestObstacle.position) + obstacleDirection;

            
        }
        hidePosition /= filteredContext.Count;

        //FOR DEBUG ONLY
        Debug.DrawRay(hidePosition, Vector2.up * 1f);

        return hidePosition - (Vector2)agent.transform.position; 

        
    }



    #endregion
}
