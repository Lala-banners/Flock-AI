using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Predator : Life
{
    #region Predator AI Requirements
    /*
     * Wander around a territory (waypoint)
     * Seek prey - make seek radius/range
     * Attack - make attack range
     * Collision Avoidance - put obsacles for predator to avoid
     * Offset Pursuit
     * Search & Seek SAME THING
    */
    #endregion

    #region Variables
    [Header("Predator Stats")]
    public GameObject[] PredatorWaypoints;
    public PredatorState state;
    public bool ChangeState = false;
    [Space]
    public int index = 0;
    public float speed;
    public RangeInt atkRange; //would be 10
    public RangeInt seekRange; //would be 15
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

    //Function to move the AI
    void MovePredator(Vector2 targetPosition)
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }
    #endregion

    #region States

    public enum PredatorState
    {
        Wander,
        Seek,
        Attack,
        Avoid,
        Offset
    }

    IEnumerator WanderState() //Wander to attack
    {
        print("Wander: Enter");
        while (state == PredatorState.Wander)
        {
            //To Change States(From Wander to Attack, if prey hide state, then seek)
            if (ChangeState == false)
            {
                state = PredatorState.Wander; //changing states
            }
            
            yield return 0;
        }
        MovePredator(PredatorWaypoints[index].transform.position);
        print("Wander: Exit");
        NextState();
    }

    IEnumerator SeekState() //Seek prey if in seek range
    {
        print("Seek: Enter");
        while (state == PredatorState.Seek) 
        {
            
            yield return 0;
        }
        print("Seek: Exit");
        NextState();
    }

    IEnumerator AttackState() //Attack prey if in attack range
    {
        print("Attack: Enter");
        while (state == PredatorState.Attack)
        {
            
            yield return 0;
        }
        print("Atack: Exit");
        NextState();
    }

    IEnumerator AvoidState() //Avoid prey if predator health below half
    {
        print("Avoid: Enter");
        while (state == PredatorState.Avoid)
        {
            
            yield return 0;
        }
        print("Avoid: Exit");
        NextState();
    }

    IEnumerator OffsetState() 
    {
        print("Offset: Enter");
        while (state == PredatorState.Offset)
        {
            
            yield return 0;
        }
        print("Offset: Exit");
        //NextState();
    }


    #endregion





}
