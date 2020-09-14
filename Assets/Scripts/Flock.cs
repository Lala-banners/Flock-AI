using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public FlockAgent agentPrefab;
    List<FlockAgent> agent = new List<FlockAgent>();
    public FlockBehaviour behaviour;

    [Range(10,500)] //[Range()] inside unity editor sets min and max birds as a slider
    public int startingCount = 250;
    //const = constant number that does not change unless in script
    const float AgentDensity = 0.08f;

    //driveFactor gives us back direction number and if its too small, makes flock go faster by multiplying that number
    [Range(1f, 100f)]
    public float driveFactor = 10f;

    [Range(1f,100f)]
    public float maxSpeed = 5f;

    //each agent is based off their neighbor so they stay together
    [Range(1f, 10f)]
    public float neighborRadius = 1.5f;

    //keep distance between ecah bird so they do not collide with neighbor
    [Range(0f, 1f)]
    public float avoidanceRadiusMultiplier = 0.5f;

    //will need these for calculations and will need to square other numbers --> bad for computer
    float squareMaxSpeed;
    float squareNeighborRadius;
    float squareAvoidanceRadius;
    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }

    private void Start()
    {
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighborRadius = neighborRadius * neighborRadius;
        squareAvoidanceRadius = squareNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;

        //Loops for startingCount times of agents
        for (int i = 0; i < startingCount; i++)
        {
            //Instantiate = makes a duplicate of an object or a prefab 
            //Random.insideUnitCircle returns random point inside a circle, number from startingCount * AgentDensity
            //Quaternion.Euler() converts x/y/z to 2D random rotation
            //Quaternion.Euler(Vector3.forward * Random.Range(0,360f)) 
            FlockAgent newAgent = Instantiate(
                agentPrefab,
                Random.insideUnitCircle * startingCount * AgentDensity, 
                Quaternion.Euler(Vector3.forward * Random.Range(0,360f)),
                transform //child of transform game object 
                );
            newAgent.name = "Agent" + i; //change name of agent
            newAgent.Initialize(this); //this is the flock newAgent is a part of
            agent.Add(newAgent);
        }
    }
}
