using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Pursuit")]

public class PursuitBehavior : FilteredFlockBehavior
{
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, FlockLife flock)
    {
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);

        if (filteredContext.Count == 0)
        {
            return Vector2.zero;
        }

        Vector2 move = Vector2.zero;

        foreach (Transform item in filteredContext)
        {
            //Need distance between item and the agents
            float distance = Vector2.Distance(item.position, agent.transform.position);
            //Make two context filters - run two searches around AI
            float distancePercent = distance / flock.neighborRadius;
            float inverseDistancePercent = 1 - distancePercent; //dividing distance of enemy by the max radius
            float weight = inverseDistancePercent / filteredContext.Count;

            //Direction from agent to enemy. Magnitude becomes less
            Vector2 direction = (item.position - agent.transform.position) * weight;
            move += direction;


        }
        return move;
    }

   
}
