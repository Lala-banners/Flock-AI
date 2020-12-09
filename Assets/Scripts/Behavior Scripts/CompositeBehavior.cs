using System.Collections.Generic;
using UnityEngine;

//Creating Menu in Project window in Unity
// = Created a ScriptableObject object - container of data that doesn't need to be in the scene
[CreateAssetMenu(menuName = "Flock/Behavior/Composite")]

public class CompositeBehavior : FlockBehavior
{
    //Can be accessed by Unity
    [System.Serializable]
    public struct BehaviorGroup
    {
        public FlockBehavior behaviors;
        public float weights;
    }

    public BehaviorGroup[] behaviors;

    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, FlockLife flock)
    {
        //Outside the class in variable form = CompositeBehavior.BehaviorGroup varName;
        Vector2 move = Vector2.zero;

        //Go through each behavior and find length and combine them
        for(int i = 0; i < behaviors.Length ;i++)
        {
            //Either increase or decrease the effect of the current behavior on the overall 
            Vector2 partialMove = behaviors[i].behaviors.CalculateMove(agent, context, flock) * behaviors[i].weights;

            if (partialMove != Vector2.zero)
            {
                //Makes sure the number we get for moving the agent isn't larger than the weight we gave it
                if (partialMove.sqrMagnitude > behaviors[i].weights * behaviors[i].weights)
                {
                    partialMove.Normalize();
                    partialMove *= behaviors[i].weights;
                }

                //Bring all  the behaviors together as one
                move += partialMove;
            }
        }

        return move;
    }

    
}
