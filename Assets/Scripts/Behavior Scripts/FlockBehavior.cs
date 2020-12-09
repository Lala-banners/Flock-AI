using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FlockBehavior : ScriptableObject
{
    //ScriptableObject does not need to be attached to a game object to work unlike MonoBehaviour 
    //public abstract class is incomplete (now complete!)
    public abstract Vector2 CalculateMove(FlockAgent agent, List<Transform> context, FlockLife flock); 
}


