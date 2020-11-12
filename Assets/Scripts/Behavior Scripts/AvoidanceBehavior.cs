using System.Collections.Generic;
using UnityEngine;

//Creating Menu in Project window in Unity
// = Created a ScriptableObject object - container of data that doesn't need to be in the scene
[CreateAssetMenu(menuName = "Flock/Behavior/Avoidance")]

public class AvoidanceBehavior : FilteredFlockBehavior
{
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        //Trying to move away from neighbors
        if (context.Count == 0)
        {
            return Vector2.zero;
        }
        //Getting the average
        Vector2 avoidanceMove = Vector2.zero;
        //Number of things to avoid variable
        int numAvoid = 0;
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
        foreach (Transform item in filteredContext)
        {
            //Agent goes in opposite direction of item inside foreach method
            //Length of direction compared to the direction of SquareAvoidanceRadius
            //OR Distance between item position and agent position
             if(Vector2.SqrMagnitude(item.position - agent.transform.position) < flock.SquareAvoidanceRadius)
            {
                numAvoid++;
                avoidanceMove += (Vector2)(agent.transform.position - item.position);
            }
        }
        //If we have things to avoid then average out
        if (numAvoid > 0)
        {
            avoidanceMove /= numAvoid;
        }

        return avoidanceMove;
       

    }

   
}
