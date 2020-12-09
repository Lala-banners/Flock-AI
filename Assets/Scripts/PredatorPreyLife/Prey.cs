using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prey : Life
{
    private FlockBehavior wanderBehavior;
    private FlockBehavior flockBehavior;
    private FlockBehavior hideBehavior;
    private FlockBehavior evadeBehavior;
    [SerializeField] private ContextFilter otherFlock;

    #region Wander
    IEnumerator WanderState()
    {
        flock.behavior = wanderBehavior;
        while (lifeStates == LifeStates.Wander)
        {
            print("Prey are wandering");
            yield return 0;
        }
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
                if (filteredContext.Count <= 0)
                {
                    lifeStates = LifeStates.Wander;
                }
                else
                {

                }
            }
            yield return 0;
        }
        NextState();
    }

    private void Evade()
    {

    }
    #endregion

    #region Hide
    IEnumerator HideState()
    {
        flock.behavior = hideBehavior;
        while (lifeStates == LifeStates.Hide)
        {
            print("Prey are hiding");
            yield return 0;
        }
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
            yield return 0;
        }
        NextState();
    }

    private void Flock()
    {

    }
    #endregion

    // OnCollisionEnter is called when this collider/rigidbody has begun touching another rigidbody/collider
    private void OnCollisionEnter(Collision collision)
    {
        if(gameObject.tag == "Predator" && collision.gameObject.tag == "Prey")
        {
            Destroy(collision.gameObject);
            print("Prey are being eaten");
        }
    }






}
