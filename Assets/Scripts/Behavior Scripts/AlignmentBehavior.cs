using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Creating Menu in Project window in Unity
// = Created a ScriptableObject object - container of data that doesn't need to be in the scene
[CreateAssetMenu(menuName = "Flock/Behavior/Alignment")]

public class AlignmentBehavior : FlockBehavior
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
        foreach(Transform item in context)
        {
            //Up gives up direction in world space, otherwise in local space then is 0
            alignmentMove += (Vector2)item.transform.up;
        }
        alignmentMove /= context.Count;

        //Always put returns OUTSIDE of loop
        return alignmentMove; 
    }
}
