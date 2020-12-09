using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PreyStates
{
    Wander,
    Evade,
    Hide
}

public class Prey : Life
{
    public PreyStates states;
    private FlockBehavior wanderBehavior;

    IEnumerator WanderState()
    {
        flock.behavior = wanderBehavior;
        while (states == PreyStates.Wander)
        {
            yield return 0;
        }
        NextState();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void NextState()
    {
        string methodName = states.ToString() + "State";

        System.Reflection.MethodInfo info =
            GetType().GetMethod(methodName,
                                System.Reflection.BindingFlags.NonPublic |
                                System.Reflection.BindingFlags.Instance);
        //Run our method
        StartCoroutine((IEnumerator)info.Invoke(this, null));
        //Using StartCoroutine() means we can leave and come back to the method that is running
        //All Coroutines must return IEnumerator
    }

    // OnCollisionEnter is called when this collider/rigidbody has begun touching another rigidbody/collider
    private void OnCollisionEnter(Collision collision)
    {
        if(gameObject.tag == "Predator" && collision.gameObject.tag == "Prey")
        {
            Destroy(collision.gameObject);
            print("Prey being eaten");
        }
    }






}
