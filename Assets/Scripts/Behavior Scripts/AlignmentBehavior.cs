using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Creating Menu in Project window in Unity
// = Created a ScriptableObject object - container of data that doesn't need to be in the scene
[CreateAssetMenu(menuName = "Flock/Behavior/Alignment")]

public class AlignmentBehavior : FilteredFlockBehavior
{
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        if(context.Count == 0)
        {
            //Aligning with neighbors in the same direction
            return agent.transform.up;
        }

        //Add all points together and average
        Vector2 alignmentMove = Vector2.zero;
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
        int count = 0;
        foreach (Transform item in filteredContext)
        {
            if (Vector2.SqrMagnitude(item.position - agent.transform.position) <= flock.SquareSmallRadius)
            {
                //Up gives up direction in world space, otherwise in local space then is 0
                alignmentMove += (Vector2)item.transform.up;
                count++;
            }
            
        }
        if (count != 0)
        {
            alignmentMove /= count;
        }
        //Always put returns OUTSIDE of loop
        return alignmentMove; 
    }
}
