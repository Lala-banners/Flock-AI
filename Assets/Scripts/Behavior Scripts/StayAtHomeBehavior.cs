using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Stay at Home")]

public class StayAtHomeBehavior : FlockBehavior
{
    [SerializeField]
    private Vector2 center;
    [SerializeField]
    public float radius = 15;

    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, FlockLife flock)
    {
        //Direction to the center
        //Magnitude will be the distance to the center
        Vector2 centerOffset = center - (Vector2)agent.transform.position;
        float t = centerOffset.magnitude / radius;

        //If t less than the radius, do not change the agents
        if (t < 0.9f)
        {
            return Vector2.zero;
        }
        //If not, change the offset
        return centerOffset * t * t;
    }
}
