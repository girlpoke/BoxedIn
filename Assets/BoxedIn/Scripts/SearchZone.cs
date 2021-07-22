using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public enum SearchType
{
    QuickSearch,
    LongSearch,
    MoveItem
}

[RequireComponent(typeof(SphereCollider)), HideMonoScript]
public class SearchZone : SerializedMonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private AgentManager agent;

    private SphereCollider sphereCollider;
    private Color color = Color.green;
    
    public SearchType currentSearchType = SearchType.QuickSearch;
    public Transform[] searchObjects;
    public int count;

    private void Start()
    {
        agent = FindObjectOfType<AgentManager>();
    }

    private IEnumerator EndSearch()
    {
        yield return new WaitForSeconds(10);
        var stateMachine = FindObjectOfType<WorkerStateMachine>();
        agent.searchArea = false;
        stateMachine.ChangeStates(States.Patrol);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {   
            agent.searchZone = GetComponent<SearchZone>();
            
            color = Color.red;
            var stateMachine = other.GetComponent<WorkerStateMachine>();
            stateMachine.ChangeStates(States.Search);
            agent.searchArea = true;

            StartCoroutine(EndSearch());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {   
            color = Color.green;
        }
    }
    private void OnDrawGizmos()
    {
        sphereCollider = GetComponent<SphereCollider>();
        Gizmos.color = color;
        Gizmos.DrawWireSphere(transform.position, (sphereCollider.radius = radius));
    }
}
