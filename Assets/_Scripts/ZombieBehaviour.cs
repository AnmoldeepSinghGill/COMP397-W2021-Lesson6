using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum ZombieState
{
    IDLE,
    RUN,
    JUMP
}

public class ZombieBehaviour : MonoBehaviour
{
    [Header("Line of Sight")]
    public LayerMask collisionLayer;
    public Vector3 LOSoffset = new Vector3(0.0f, 2.0f, -5.0f);
    public bool HasLOS;
    public GameObject player;

    private NavMeshAgent agent;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (HasLOS)
        {
            agent.SetDestination(player.transform.position);
        }

        if (HasLOS && Vector3.Distance(transform.position, player.transform.position) < 2.5)
        {
            animator.SetInteger("AnimState", (int)ZombieState.IDLE);
            transform.LookAt(transform.position - player.transform.forward); 
        }
        else
        {
            animator.SetInteger("AnimState", (int)ZombieState.RUN);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            HasLOS = true;
            player = other.transform.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            HasLOS = false;
        }
    }

    //void OnDrawGizmos()
    //{
    //    var size = new Vector3(4.0f, 2.0f, 10.0f);
    //    Gizmos.color = Color.magenta;
    //    Gizmos.DrawWireCube(transform.position + LOSoffset, size);
    //}
}
