using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//we have to be attached to a game object that has a Collider2D
[RequireComponent(typeof(Collider2D))] 
public class FlockAgent : MonoBehaviour
{
    //FlockAgent is CharacterController of AI

    Flock agentFlock;
    //another class can access value of AgentCollider but cannot change it (would need a set)
    public Flock AgentFlock { get { return agentFlock; } }

    private Collider2D agentCollider;
    //another class can access value of AgentCollider but cannot change it (would need a set)
    public Collider2D AgentCollider { get { return agentCollider; } }
    void Start()
    {
        agentCollider = GetComponent<Collider2D>();
    }

    public void Initialize(Flock flock)
    {
        agentFlock = flock;
    }

    //Changes the up direction to where ever velocity is facing
    public void Move(Vector2 velocity)
    {
        //velocity gives direction and speed
        transform.up = velocity;
        transform.position += (Vector3) velocity * Time.deltaTime;
    }
}
