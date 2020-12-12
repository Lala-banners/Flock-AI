using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LifeStates //contains both predator and prey states
{
    #region Prey
    Wander,
    Evade,
    Hide,
    Flock,
    #endregion

    #region Predator
    Attack,
    Pursuit,
    CollisionAvoidance,
    #endregion

}

public abstract class Life : MonoBehaviour
{
    [Header("Life Stats")] //Stats that both Predator and Prey have
    public LifeStates lifeStates;
    public bool changeState = false;
    protected float speed = 10; //prey speed
    protected float attackStrength; //for predator to attack prey
    protected float chaseRadius = 5f; //for predator to chase prey
    protected float fleeRadius = 10f; //for prey to flee predator

    //Connection to Flock script
    [SerializeField] protected Flock flock;

    protected abstract float GetRadius();

    virtual protected void Start()
    {
        flock = GetComponent<Flock>(); //Getting reference to Flock

        if(flock == null)
        {
            Debug.LogError("No Flock in scene");
            return;
        }
        NextState();
    }

    //Function for calling the next state
    protected void NextState()
    {
        string methodName = lifeStates.ToString() + "State";

        System.Reflection.MethodInfo info =
            GetType().GetMethod(methodName,
                                System.Reflection.BindingFlags.NonPublic |
                                System.Reflection.BindingFlags.Instance);
        //Run our method
        StartCoroutine((IEnumerator)info.Invoke(this, null));
        //Using StartCoroutine() means we can leave and come back to the method that is running
        //All Coroutines must return IEnumerator
    }

    protected List<Transform> GetNearbyObjects(FlockAgent agent)
    {
        //Empty list that we will write to
        List<Transform> context = new List<Transform>();
        //Make an over lap circle around the agents
        //Each agent will do a circle
        //Needs a NeighborRadius 
        Collider2D[] contextColliders = Physics2D.OverlapCircleAll(agent.transform.position, GetRadius());
        foreach (Collider2D c in contextColliders)
        {
            //If the circle that collides with everything around it is NOT Agent collider
            //Add to the context
            if (c != agent.AgentCollider)
            {
                context.Add(c.transform);
            }
        }
        return context;
    }
}
