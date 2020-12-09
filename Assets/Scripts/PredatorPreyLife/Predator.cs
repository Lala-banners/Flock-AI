using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Predator : Life
{
    [Header("Predator")]
    public FlockAgent agent;
    //public Vector2 predatorSpeed;
    public float damage;
    [SerializeField] private FlockBehavior attack;
    [SerializeField] private ContextFilter otherFlock;

    // Update is called once per frame
    void Update()
    {

    }

    private void MovePredator(Vector2 velocity)
    {
        transform.up = velocity;
        transform.position += (Vector3)velocity * Time.deltaTime;
    }


    #region Attack
    private IEnumerator AttackState()
    {


        yield return 0;
    }
    
    private void Attack()
    {

    }
    #endregion

    #region Wander
    private IEnumerator WanderState()
    {
        while (lifeStates == LifeStates.Wander) //while in wander state
        {
            foreach (FlockAgent agent in flock.agents) //go through list of agents in FlockAgent
            {
                //Filter through the list of agents and find prey (other flock)
                List<Transform> filteredContext = (otherFlock == null) ? flock.context : otherFlock.Filter(agent, flock.context);
            }
        }

        yield return 0;
    }
    private void Wander()
    {

    }
    #endregion

    #region Pursuit Offset
    private IEnumerator PursuitState()
    {


        yield return 0;
    }

    private void Pursuit()
    {

    }
    #endregion

    #region Collision Avoidance
    private IEnumerator CollisionAvoidState()
    {


        yield return 0;
    }

    private void CollisionAvoidance()
    {

    }
    #endregion



}
