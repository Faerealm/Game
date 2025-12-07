using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.AI;
public enum AnimalState
{
    Idle,
    Moving,
}
[RequireComponent(typeof(NavMeshAgent))]


public class animal01 : MonoBehaviour
{
    [Header("Wander")]
    public float wanderDistance = 50.0f;//动物在一个小时内移动的距离
    public float walkSpeed = 5.0f;
    public float maxWalkTime = 6.0f;//移动六秒钟之后变进入闲置状态
    [Header("Idle")]
    public float idleTime = 2.0f;
    protected NavMeshAgent navMeshAgent;
    protected AnimalState currentState = AnimalState.Idle;
    private void Start()
    {
        InitialiseAnimal();
    }
    protected virtual void InitialiseAnimal()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = walkSpeed;
        currentState = AnimalState.Idle;
        UpdateState();
    }
    protected virtual void UpdateState()
    {
        switch (currentState)
        {
            case AnimalState.Idle:
                HandleIdleState();
                break;
            case AnimalState.Moving:
                HandleMovingState();
                break;
        }
    }
    protected Vector3 GetRandomNavMeshPosition(Vector3 origin, float distance)
    {//创建一个动物要移动到的点
        Vector3 randomDirection = Random.insideUnitSphere * distance;
        //指定球面内创建的一个随机点
        randomDirection += origin;
        NavMeshHit navMeshHit;
        if (NavMesh.SamplePosition(randomDirection, out navMeshHit, distance, NavMesh.AllAreas))
        {
            return navMeshHit.position;
        }
        else
        {
            return GetRandomNavMeshPosition(origin, distance);
        }
    }
    protected virtual void HandleIdleState()
    {
        StartCoroutine(WaitToMove());//StartCoroutine例程调用
    }
    private IEnumerator WaitToMove()
    {
        float waitTime = Random.Range(idleTime / 2, idleTime * 2);
        //创建随机等待时间
        yield return new WaitForSeconds(waitTime);

        Vector3 randomDestination = GetRandomNavMeshPosition(transform.position, wanderDistance);
        navMeshAgent.SetDestination(randomDestination);//前进的目的地
        SetState(AnimalState.Moving);
    }
    protected virtual void HandleMovingState()
    {
        StartCoroutine(WaitToReachDestination());
    }
    private IEnumerator WaitToReachDestination()
    {
        float startTime = Time.time;
        while ((navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance))
        {
            if (Time.time - startTime >= maxWalkTime)
            {
                navMeshAgent.ResetPath();
                SetState(AnimalState.Idle);
                yield break;
            }
            yield return null;
        }
        //Destination has been reached
        SetState(AnimalState.Idle);
    }
    protected void SetState(AnimalState newState)
    {
        if (currentState == newState) return;

        currentState = newState;
        OnStateChanged(newState);
    }
    protected virtual void OnStateChanged(AnimalState newState)
    {
        UpdateState();
    }
}

