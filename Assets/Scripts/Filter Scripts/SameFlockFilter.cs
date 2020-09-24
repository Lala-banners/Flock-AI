using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Filter/Same Flocks")]

public class SameFlockFilter : ContextFilter
{
    public override List<Transform> Filter(FlockAgent agent, List<Transform> original)
    {
        List<Transform> filtered = new List<Transform>();
        //Every item in original will be in filtered list
        foreach(Transform item in original)
        {
            FlockAgent itemAgent = item.GetComponent<FlockAgent>();
            //If itemAgent doesn't exist then it can't be used unless it has been checked first
            if(itemAgent != null) //&& keeps it in the same line (itemAgent.AgentFlock == agent.AgentFlock)
            {
                if(itemAgent.AgentFlock == agent.AgentFlock)
                {
                    //If agent is part of the same flock then add to filtered List
                    filtered.Add(item);
                }
            }
        }
        
        return filtered;
    }
}
