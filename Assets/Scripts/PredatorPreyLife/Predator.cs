using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Predator : Life
{
    [Header("Predator")]
    public FlockAgent agent;
    protected float predatorSpeed;
    public float damage;
    [SerializeField] private CompositeBehavior[] predatorBehaviors;
    [SerializeField] private FlockBehavior seekBehavior; //if prey is in range, change to attack
    [SerializeField] private FlockBehavior attackBehavior; //while prey are in attacking range or if all prey is gone, change to wander
    [SerializeField] private ContextFilter otherFlock; //for distinguishing between predator and prey
    [SerializeField] private ContextFilter obstacleAvoidance; //for collision avoidance
    public Prey prey;
    public Transform[] wanderPoints;
    public int index = 0;

    // Update is called once per frame
    private void Update()
    {
        switch (lifeStates)
        {
            case LifeStates.Pursuit:
                foreach (FlockAgent prey in flock.agents) //loop through flock agents
                {
                    if (flock.agents.Count <= 0) //if predators have eaten all prey
                    {
                        lifeStates = LifeStates.Wander; //go to wander state
                    }
                }
                break;
            case LifeStates.Attack:
                foreach (FlockAgent prey in flock.agents) //loop through list of prey
                {
                    List<Transform> filteredContext = (otherFlock != null) ? flock.context : otherFlock.Filter(agent, flock.context); //Filter through other flock (prey)

                    if (filteredContext.Count > minDistance) //if count of filtered flock (prey) is in range, then attack
                    {
                        lifeStates = LifeStates.Attack;
                    }
                    else //if prey not in range, predator wander
                    {
                        lifeStates = LifeStates.Wander;
                    }
                }
                
                break;
            case LifeStates.CollisionAvoidance:
                //Debug.Log("collision avoidance");
                break;
        }

    }


    #region Attack
    private IEnumerator AttackState()
    {
        while (lifeStates == LifeStates.Attack)
        {

            print("Predator are eating prey");
            yield return null;
        }
        yield return null;
    }

    private void Attack()
    {

    }
    #endregion


    #region Wander
    private IEnumerator WanderState() //make predator travel path
    {
        Wander();

        while (lifeStates == LifeStates.Wander) //while in wander state
        {
            print("Predators are wandering");
            foreach (FlockAgent agent in flock.agents) //go through list of agents in FlockAgent
            {
                //Filter through the list of agents and find prey (other flock)
                List<Transform> filteredContext = (otherFlock == null) ? flock.context : otherFlock.Filter(agent, flock.context);
                flock.agentPrefab.Move(wanderPoints[index].position); //move on waypoints
                yield return null;
                //If the distance between the prey and predator is greater than 5(chase prey range) then AI patrols
                if (Vector2.Distance(prey.transform.position, transform.position) > chaseRadius)
                {
                    //Change state to wander state
                    lifeStates = LifeStates.Wander;
                }
                //If the distance between the prey and predator is less than 5(attack prey range)  
                else if (Vector2.Distance(prey.transform.position, transform.position) < chaseRadius)
                {
                    //Change state to attack state and eat prey
                    lifeStates = LifeStates.Attack;
                }
            }
            yield return null;
        }
        NextState();
        yield return null;
    }
    public float minDistance = 0.5f;
    private void Wander() //Predator traveling waypoints
    {
        //Getting distance between the predator and the waypoints
        float distance = Vector2.Distance(transform.position, wanderPoints[index].transform.position);

        //if distance between predator and waypoints is less than 0.5 then increase index of waypoints
        if (distance < minDistance)
        {
            index++;
        }
        if (index >= wanderPoints.Length)
        {
            index = 0;
        }
        flock.agentPrefab.Move(wanderPoints[index].position);
    }
    #endregion

    #region Pursuit Offset
    private IEnumerator PursuitState()
    {
        while (lifeStates == LifeStates.Pursuit)
        {
            print("Predator are pursuing prey");
            yield return null;
        }
        yield return null;
    }

    private void Pursuit()
    {

    }
    #endregion

    #region Collision Avoidance
    private IEnumerator CollisionAvoidState()
    {
        while (lifeStates == LifeStates.CollisionAvoidance)
        {
            print("Predator are avoiding obstacles");
            yield return null;
        }
        yield return null;
    }

    private void CollisionAvoidance()
    {

    }
    #endregion



}
