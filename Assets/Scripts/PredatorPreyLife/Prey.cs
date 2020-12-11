using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prey : Life
{
    protected float preySpeed;
    [SerializeField] private FlockBehavior wanderBehavior;
    [SerializeField] private FlockBehavior flockBehavior; //
    [SerializeField] private FlockBehavior hideBehavior; //
    [SerializeField] private FlockBehavior evadeBehavior; //other flock avoidance - make weight very high
    [SerializeField] private ContextFilter otherFlock; //for distinguishing between predator and prey
    public Transform[] preyWanderPoint;
    public int i; //waypoint index
    public float minDistance = 0.5f;

    #region Wander
    IEnumerator WanderState()
    {
        flock.behavior = wanderBehavior;
        while (lifeStates == LifeStates.Wander)
        {
            print("Prey are wandering");
            //Getting distance between the predator and the waypoints
            float distance = Vector2.Distance(transform.position, preyWanderPoint[i].transform.position);

            //if distance between predator and waypoints is less than 0.5 then increase index of waypoints
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
    IEnumerator EvadeState()
    {
        flock.behavior = evadeBehavior;
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
            }
            yield return null;
        }
        yield return null;
        NextState();
    }
    #endregion

    #region Hide
    IEnumerator HideState()
    {
        flock.behavior = hideBehavior;
        while (lifeStates == LifeStates.Hide)
        {
            print("Prey are hiding");
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
    IEnumerator FlockState()
    {
        flock.behavior = flockBehavior;
        while (lifeStates == LifeStates.Flock)
        {
            print("Prey are flocking");
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
