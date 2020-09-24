using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ContextFilter : ScriptableObject
{
    //List<Transform> original is the neighbors and will filter the objects we are looking for
    public abstract List<Transform> Filter(FlockAgent agent, List<Transform> original);
}
