using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prey : Life
{
    protected float preySpeed;
    //[SerializeField] private CompositeBehavior[] preyBehaviors;
    [SerializeField] private FlockBehavior wanderBehavior;
    [SerializeField] private FlockBehavior flockBehavior; //stay at home behavior
    [SerializeField] private FlockBehavior hideBehavior; //hide behind obstacles
    [SerializeField] private FlockBehavior evadeBehavior; //other flock avoidance - make weight very high
    private ContextFilter otherFlock; //for distinguishing between predator and prey
    public Transform[] preyWanderPoint;
    public Transform hidePoint;
    [Tooltip("Prey Waypoint Index")] [SerializeField] private int i; //waypoint index
    [SerializeField] private float minDistance = 0.5f;

    [Header("Prey Radius\'")]
    [SerializeField]
    private float flockRadius = 5f;
    [SerializeField]
    private float wanderRadius = 5f;
    [SerializeField]
    private float evadeRadius = 5f;

    protected override float GetRadius()
    {
        switch(lifeStates)
        {
            case LifeStates.Flock:
                return flockRadius;
            case LifeStates.Wander:
                return wanderRadius;
            case LifeStates.Evade:
                return evadeRadius;
        }
        return fleeRadius;
    }

    #region Wander
    private IEnumerator WanderState()
    {
        while (lifeStates == LifeStates.Wander)
        {
            print("Prey are wandering");

            foreach (FlockAgent agent in flock.agents)
            {
                Vector2 velocity = wanderBehavior.CalculateMove(agent, GetNearbyObjects(agent), flock);
                agent.Move(velocity);
            }

            yield return null;

            //Getting distance between the predator and the waypoints
            float distance = Vector2.Distance(transform.position, preyWanderPoint[i].transform.position);

            //if distance between predator and waypoints is less than 0.5 then increase index of waypoints and go to next waypoint
            if (distance < minDistance)
            {
                i++;
            }
            if (i >= preyWanderPoint.Length)
            {
                i = 0;
            }
            flock.agentPrefab.Move(preyWanderPoint[i].position);
            yield return null;
        }
        yield return null;
        NextState();
    }

    private void Wander()
    {

    }
    #endregion

    #region Evade
    private IEnumerator EvadeState()
    {
        while (lifeStates == LifeStates.Evade)
        {
            print("Prey are evading predator");
            foreach (FlockAgent agent in flock.agents)
            {
                List<Transform> filteredContext = (otherFlock == null) ? flock.context : otherFlock.Filter(agent, flock.context);
                if (filteredContext.Count <= 0) //if count of other flock (predators) is less than 0 then go to wander behavior
                {
                    lifeStates = LifeStates.Wander;
                }
                else
                {
                    //if prey cant evade then they will be eaten
                }

                Vector2 velocity = evadeBehavior.CalculateMove(agent, GetNearbyObjects(agent), flock);
                agent.Move(velocity);
            }
            yield return null;
        }
        yield return null;
        NextState();
    }
    #endregion

    #region Hide
    private IEnumerator HideState() //Hide = go out of chase range
    {
        while (lifeStates == LifeStates.Hide)
        {
            print("Prey are hiding");
            //Transform hideLocation = Vector2.MoveTowards(transform.position, target, preySpeed); 
            //flock.agentPrefab.Move(hideLocation);

            //Move prey flock to hide point (set outside predator range, only hide if there are less than 5 prey still alive)
            flock.agentPrefab.Move(hidePoint.position);
            yield return null;
        }

        yield return null;
        NextState();
    }

    private void Hide()
    {

    }
    #endregion

    #region Flock
    private IEnumerator FlockState()
    {
        while (lifeStates == LifeStates.Flock)
        {
            print("Prey are flocking");
            foreach (FlockAgent agent in flock.agents)
            {
                Vector2 velocity = flockBehavior.CalculateMove(agent, GetNearbyObjects(agent), flock);
                agent.Move(velocity);
            }
            
            yield return null;
        }
        yield return null;
        NextState();
    }

    private void Flock()
    {

    }
    #endregion







}
