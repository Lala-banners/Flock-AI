using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Filter/Physics Layer")]

public class PhysicsLayerFilter : ContextFilter
{
    public LayerMask mask;

    public override List<Transform> Filter(FlockAgent agent, List<Transform> original)
    {
        List<Transform> filtered = new List<Transform>();
        foreach (Transform item in original)
        {
            //Getting the layer and shifting 1 to the left and ORing to public LayerMask mask, and checking if they are the same.
            //Will not be the same if the mask is false

            //EXAMPLE
            //mask =   0100
            //layer1 = 0001
            //layer2 = 0100
            
            //If the layer of the item exists in the mask, the mask won't change with an OR 
            if (mask == (mask | (1 << item.gameObject.layer)))
            {
                filtered.Add(item);
            }
        }
        return filtered;
    }
}
