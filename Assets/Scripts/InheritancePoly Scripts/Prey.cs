using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prey : Life
{
    #region Prey AI Requirements
    /*
     * Flock, make flock point as goal, make function for completion of prey going to goal (print message)
     * Wander, need prey waypoints for wander until/if predator wanders into prey wander range
     * Evade, go out of predator seek range
     * Hide, hide behind obstacle wall
    */
    #endregion

    #region Variables
    public GameObject[] PreyWaypoints;
    public PreyState state;
    public bool ChangeState = false;
    #endregion

    #region States
    public enum PreyState
    {
        Flock,
        Wander,
        Evade,
        Hide
    }
    IEnumerator FlockState() 
    {
        print("Flock: Enter");
        while (state == PreyState.Flock)
        {
            
            
            yield return 0;
        }
        print("Flock: Exit");
        NextState();
    }

    IEnumerator Wandertate() //Seek prey if in seek range
    {
        print("Wander: Enter");
        while (state == PreyState.Wander)
        {
            
            yield return 0;
        }
        print("Wander: Exit");
        NextState();
    }

    IEnumerator EvadeState() 
    {
        print("Evade: Enter");
        while (state == PreyState.Evade)
        {
            
            
            yield return 0;
        }
        print("Evade: Exit");
        NextState();
    }

    IEnumerator HideState() 
    {
        print("Evade: Enter");
        while (state == PreyState.Hide)
        {
            
            yield return 0;
        }
        print("Hide: Exit");
        //NextState();
    }
    #endregion

    #region Functions
    private void Start()
    {
        NextState();
    }

    void NextState()
    {
        string methodName = state.ToString() + "State";
        System.Reflection.MethodInfo info =
            GetType().GetMethod(methodName,
                                   System.Reflection.BindingFlags.NonPublic |
                                   System.Reflection.BindingFlags.Instance);

        StartCoroutine((IEnumerator)info.Invoke(this, null));
    }
    #endregion
}

