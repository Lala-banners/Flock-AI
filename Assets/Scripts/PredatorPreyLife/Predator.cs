using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Predator : Life
{
    [Header("Predator")]
    public TMP_Text stateText;
    public FlockAgent agent;
    protected float predatorSpeed;
    public float damage;
    [SerializeField] private FlockBehavior pursuitBehavior; //if prey is in range, change to attack
    [SerializeField] private FlockBehavior attackBehavior; //while prey are in attacking range or if all prey is gone, change to wander
    [SerializeField] private FlockBehavior wanderBehavior; //wander
    [SerializeField] private ContextFilter otherFlock; //for distinguishing between predator and prey
    [SerializeField] private FlockBehavior obstacleAvoid; //wander
    public Prey prey;
    public Transform[] wanderPoints;
    public int index = 0;

    protected override float GetRadius()
    {
        switch (lifeStates)
        {
            case LifeStates.Pursuit:
                return chaseRadius;
        }
        return chaseRadius;
    }

    // Update is called once per frame
    private void Update()
    {
        #region (Old)
        /*switch (lifeStates)
        {
            case LifeStates.Pursuit:
                foreach (FlockAgent preyAgent in prey.GetFlock().agents) //loop through flock agents
                {
                    
                }
                break;
            case LifeStates.Attack:
                foreach (FlockAgent preyAgent in prey.GetFlock().agents) //loop through list of prey
                {
                    
                }
                break;
            case LifeStates.CollisionAvoidance:
                foreach (FlockAgent agent in flock.agents)
                {
                    print(stateText + LifeStates.CollisionAvoidance.ToString());
                    Vector2 velocity = obstacleAvoid.CalculateMove(agent, GetNearbyObjects(agent), flock);
                    agent.Move(velocity);
                }
                break;
        }*/
        #endregion
    }

    #region Attack
    private IEnumerator AttackState() //Doesnt work 
    {
        while (lifeStates == LifeStates.Attack)
        {
            stateText.text = "Predator State: " + LifeStates.Attack.ToString();
            
            foreach (FlockAgent predatorAgent in flock.agents)
            {
                Vector2 velocity = attackBehavior.CalculateMove(predatorAgent, GetNearbyObjects(predatorAgent), flock);
                predatorAgent.Move(velocity);

                foreach (FlockAgent preyAgent in prey.GetFlock().agents) //go through list of agents in prey
                {
                    // check if prey is in range of attack range (1f)
                    if (Vector3.Distance(preyAgent.transform.position, predatorAgent.transform.position) < attackRadius)
                    {
                        Destroy(prey.gameObject);
                        print("Prey are being eaten");
                  
                        if (prey.GetFlock().agents.Count <= 0) //if predators have eaten all prey
                        {
                            print("Prey are gone");
                            lifeStates = LifeStates.Wander; //go back to wander state
                            
                        }
                    }
                }
            }
            print("Predator are eating prey");
        }
        NextState();
        yield return null;
       
    }
    #endregion

    #region Wander
    private IEnumerator WanderState() //does work 
    {
        while (lifeStates == LifeStates.Wander) //while in wander state
        {
            print("Predators are wandering");
            stateText.text = "Predator State: " + LifeStates.Wander.ToString();

            foreach (FlockAgent predatorAgent in flock.agents)
            {
                Vector2 velocity = wanderBehavior.CalculateMove(predatorAgent, GetNearbyObjects(predatorAgent), flock);
                predatorAgent.Move(velocity);

                foreach (FlockAgent preyAgent in prey.GetFlock().agents) //go through list of agents in FlockAgent
                {
                    if (Vector2.Distance(preyAgent.transform.position, predatorAgent.transform.position) < chaseRadius)
                    {
                        //Change state to attack state and eat prey
                        lifeStates = LifeStates.Pursuit;
                        break;
                    }
                }

                if (lifeStates != LifeStates.Wander)
                    break;
            }
            yield return null;
        }
        NextState();
        yield return null;
    }
    /*private void Wander() 
    {
        foreach (FlockAgent agent in flock.agents)
        {
            Vector2 velocity = wanderBehavior.CalculateMove(agent, GetNearbyObjects(agent), flock);
            agent.Move(velocity);
            //yield return null;
        }
        //Getting distance between the predator and the waypoints
        float distance = Vector2.Distance(transform.position, wanderPoints[index].transform.position);

        //if distance between predator and waypoints is less than 0.5 then increase index of waypoints
        if (distance < minDistance)
        {
            index++;
        }
        if (index >= wanderPoints.Length)
        {
            index = 0;
        }
        //flock.agentPrefab.Move(wanderPoints[index].position);
    }*/
    #endregion

    #region Pursuit 
    private IEnumerator PursuitState() //doesnt work
    {
        while (lifeStates == LifeStates.Pursuit)
        {
            stateText.text = "Predator State: " + LifeStates.Pursuit.ToString();
            foreach (FlockAgent predatorAgent in flock.agents)
            {
                Vector2 velocity = pursuitBehavior.CalculateMove(predatorAgent, GetNearbyObjects(predatorAgent), flock);
                predatorAgent.Move(velocity);

                foreach (FlockAgent preyAgent in prey.GetFlock().agents) //go through list of agents in FlockAgent
                {
                    // check if prey is too far away
                    if (Vector3.Distance(preyAgent.transform.position, predatorAgent.transform.position) > chaseRadius)
                    {
                        lifeStates = LifeStates.Wander;
                        continue;
                    }
                    else
                    {
                        lifeStates = LifeStates.Attack;
                        
                    }
                }
            }
            print("Predator are pursuing prey");
            yield return null;
        }
        NextState();
        yield return null;
        
    }
    #endregion

    #region Collision Avoidance 
    private IEnumerator CollisionAvoidState() //does work 
    {
        while (lifeStates == LifeStates.CollisionAvoidance)
        {
            foreach (FlockAgent agent in flock.agents)
            {
                Vector2 velocity = obstacleAvoid.CalculateMove(agent, GetNearbyObjects(agent), flock);
                agent.Move(velocity);
                yield return null;
            }
            print("Predator are avoiding obstacles");

        }
        yield return null;
    }
    #endregion



}
