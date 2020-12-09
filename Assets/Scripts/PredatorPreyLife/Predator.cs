using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Predator : Life
{
    [Header("Predator")]
    public FlockAgent agent;
    public Vector2 predatorSpeed;
    public float damage;
    [SerializeField] private FlockBehavior attackBehavior;
    [SerializeField] private ContextFilter otherFlock;
    public Prey prey;
    public GameObject[] wanderPoints;
    public int index = 0;

    // Update is called once per frame
    void Update()
    {
        MovePredator(predatorSpeed);
    }

    private void MovePredator(Vector2 velocity)
    {
        print("Predators are moving");
        transform.up = predatorSpeed;
        transform.position += (Vector3)velocity * Time.deltaTime;
    }


    #region Attack
    private IEnumerator AttackState()
    {
        while (lifeStates == LifeStates.Attack)
        {
            print("Predator are eating prey");
            yield return 0;
        }

    }
    
    private void Attack()
    {

    }
    #endregion

    #region Wander
    private IEnumerator WanderState() //make predator travel path
    {
        while (lifeStates == LifeStates.Wander) //while in wander state
        {
            print("Predators are wandering");
            foreach (FlockAgent agent in flock.agents) //go through list of agents in FlockAgent
            {
                //Filter through the list of agents and find prey (other flock)
                List<Transform> filteredContext = (otherFlock == null) ? flock.context : otherFlock.Filter(agent, flock.context);
            }
        }

        yield return 0;
    }
    public float minDistance = 0.5f;
    private void Wander() //Predator traveling waypoints
    {
        //Getting distance between the predator and the waypoints
        float distance = Vector2.Distance(transform.position, wanderPoints[index].transform.position);

        //if distance between predator and waypoints is less than 0.5 then increase index of waypoints
        if(distance < minDistance)
        {
            index++;
        }
        if(index >= wanderPoints.Length)
        {
            index = 0; 
        }
        MovePredator(wanderPoints[index].transform.position);
    }
    #endregion

    #region Pursuit Offset
    private IEnumerator PursuitState()
    {
        while (lifeStates == LifeStates.Pursuit)
        {
            print("Predator are pursuing prey");
            yield return 0;
        }

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
            yield return 0;
        }

    }

    private void CollisionAvoidance()
    {

    }
    #endregion



}
