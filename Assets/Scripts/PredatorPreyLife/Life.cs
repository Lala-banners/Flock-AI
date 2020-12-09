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

public class Life : MonoBehaviour
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
}
