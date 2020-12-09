using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public FlockAgent agentPrefab;
    List<FlockAgent> agents = new List<FlockAgent>();
    public FlockBehavior behavior;

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

    //keep distance between each bird (agent) so they do not collide with neighbor
    [Range(0f, 1f)]
    public float avoidanceRadiusMultiplier = 0.5f;

    [Range(0f, 1f)]
    public float smallRadiusMultiplier = 0.2f;

    //will need these for calculations and will need to square other numbers 
    float squareMaxSpeed;
    float squareNeighborRadius;
    float squareAvoidanceRadius;
    float squareSmallRadius;
    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }
    public float SquareSmallRadius { get{ return squareSmallRadius; } }

    private void Start()
    {
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighborRadius = neighborRadius * neighborRadius;
        squareAvoidanceRadius = squareNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;
        squareSmallRadius = squareNeighborRadius * smallRadiusMultiplier * smallRadiusMultiplier;


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
            agents.Add(newAgent);
        }
    }

    private void Update()
    {
        //Be careful with loop within loops (Loopception)
        foreach(FlockAgent agent in agents)
        {
            //List of Transforms called context is the area around the AI
            //One agent where the AI flocks around it
            List<Transform> context = GetNearbyObjects(agent);

            //Changing color of Flock Agents FOR TESTING
            //agent.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, Color.red, context.Count /6f);
            //Lerp - Linear Interpolate 

            //Referencing move behaviour in Unity and FlockBehaviour script
            Vector2 move = behavior.CalculateMove(agent, context, this);
            //move will return some value (to increase speed) and depending on how many generate, 
            //multiplied by driveFactor
            move *= driveFactor;
            //Magnitude is speed inside a Vector - 2 uses, one is storing direction, other is magnitude 
            if(move.sqrMagnitude > squareMaxSpeed)
            {
                //normalized returns Vector with mag of 1, normalize changes Vector 
                move = move.normalized * maxSpeed;
            }
            //F12 shows definition of where the method came from
            agent.Move(move); 
        }
    }

    //By default List<Transform> is private
    private List<Transform> GetNearbyObjects(FlockAgent agent)
    {
        //Empty list that we will write to
        List<Transform> context = new List<Transform>();
        //Make an over lap circle around the agents
        //Each agent will do a circle
        //Needs a NeighborRadius 
        Collider2D[] contextColliders = Physics2D.OverlapCircleAll(agent.transform.position, neighborRadius);
        foreach(Collider2D c in contextColliders)
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
