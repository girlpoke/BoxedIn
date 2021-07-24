using System;
using BoxedIn.testing;
using Unity.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Index))]
[RequireComponent(typeof(NavMeshAgent))]
public class AgentManager : MonoBehaviour
{
    public Vector3 PathTarget => agent.steeringTarget;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float lineDist; 
    [SerializeField] private float lineOffsetDist; 
    
    private float speed;
    private float searchProgress = 5;
    [ReadOnly] public float _angle;
    
    [NonSerialized] public SearchZone searchZone;
    [NonSerialized] public bool searchArea = false;
    [ReadOnly] public bool targetSpotted;
    [NonSerialized] public Transform waypoint;
    public Transform searchTarget;
    [NonSerialized] public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<PlayerController>().transform;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;

        speed = 3.5f;
    }

    private void Update()
    {
        AgentSight();
        
    }

    public void Search()
    {
        if (searchArea)
        {
            if (searchZone.currentSearchType == SearchType.QuickSearch)
            {
                searchTarget = searchZone.searchObjects[searchZone.count];
                LookAtTarget(searchTarget.position);
                searchProgress -= 2 * Time.deltaTime;
                if (searchProgress <= 0)
                {
                    searchZone.count++;
                    
                    if (searchZone.count >= searchZone.searchObjects.Length)
                        searchZone.count = 0;
                    
                    searchProgress = 5;
                }
            }
        }
    }
    private void AgentSight()
    {
        var dir = target.position - transform.position;
        var angle = Vector3.Angle(dir, transform.forward);
        _angle = angle;
        
        //13.63 50.12032
        // if the angle is less than or equal to 50 then see if there is line of sight
        if (angle <= 50.12032f)
        {
            RaycastHit hit;
            if (Physics.Linecast(transform.position, target.position, out hit))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    Debug.DrawLine(transform.position, target.position, Color.green);
                    targetSpotted = true;
                }
                else
                {
                    Debug.DrawLine(transform.position, target.position, Color.red);
                    targetSpotted = false;
                }
            }
        }
        else targetSpotted = false;
        Debugging();
    }

    private void Debugging()
    {
        float distance = lineDist;                         // used to make it go further out
        float offsetDistance = lineOffsetDist;             // used to spread out the lines
        Vector3 offset = transform.right * offsetDistance; // sets the offset
        Vector3 forward = transform.forward * distance;    // sets the direction

        Debug.DrawLine(transform.position, transform.position + forward - offset, Color.blue); // left
        Debug.DrawLine(transform.position, transform.position + forward, Color.blue);          // middle
        Debug.DrawLine(transform.position, transform.position + forward + offset, Color.blue); // right
    }
    
    /// <summary>
    /// Makes the agent look towards the path they are taking
    /// </summary>
    public void LookAtTarget(Vector3 _target)
    {
        var position = transform.position;
        var position1 = _target;
     
        Vector3 direction = (position1 - position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 5 * Time.deltaTime);
    }
    
    /// <summary>
    /// Moves the agent towards a waypoint or a target
    /// </summary>
    public void SetAgentDestination(Transform _move)
    {
        var position = _move.position;
        agent.SetDestination(position);
        agent.speed = speed;
    } 
}
