using System.Collections.Generic;
using UnityEngine;

//Creating Menu in Project window in Unity
// = Created a ScriptableObject object - container of data that doesn't need to be in the scene
[CreateAssetMenu(menuName = "Flock/Behavior/Cohesion")]

public class CohesionBehavior : FilteredFlockBehavior //Forces whatever is implemented in FlockBehavior to be abstract class (Ctr+ right click)
{
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, FlockLife flock)
    {
        //Going in the same direction as the agents
        if(context.Count == 0)
        {
            return Vector2.zero;
        }

        //Add all points together and get the average of neighbors
        //Never assume defaults or variables
        Vector2 cohesionMove = Vector2.zero;
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
        int count = 0;
        foreach (Transform item in filteredContext)
        {
            if (Vector2.SqrMagnitude(item.position - agent.transform.position) <= flock.SquareSmallRadius)
            {
                cohesionMove += (Vector2)item.position;
            }
        }
        if (count != 0)
        {
            //Average location of everything in the context around each agent 
            // (/=) equivilent to dividing 
            cohesionMove /= count;
        }

        //Direction from a to b = b - a
        //cohesionMove = cohesionMove - (Vector2)agent.transform.position;
        cohesionMove -= (Vector2)agent.transform.position;
        return cohesionMove;

        //Make the agents go in a circle (around location points)
    }
  
}
