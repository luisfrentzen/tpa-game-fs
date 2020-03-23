using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update

    public Animator animator;
    public float lookRadius = 10f;
    public float atkCooldown = 3f;
    public float attackAnimation = 0f;
    public CharacterStats playerStats;

    Transform target;
    NavMeshAgent agent;

    void Start()
    {
        target = PlayerManager.instance.player.transform;
        playerStats = PlayerManager.instance.player.GetComponent<CharacterStats>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        
        if(attackAnimation > 0)
        {
            attackAnimation -= Time.deltaTime;
        }

        if (distance <= lookRadius && attackAnimation <= 0)
        {
            atkCooldown -= Time.deltaTime;
            agent.SetDestination(target.position);
            animator.SetBool("isMoving", true);

            if( distance <= agent.stoppingDistance)
            {
                FaceTarget();
                if (atkCooldown <= 0f && animator.GetBool("Attack") == false)
                {
                    animator.SetBool("Attack", true);
                    atkCooldown = 3f;
                    attackAnimation = 1.5f;
                    playerStats.TakeDamage(5);
                }
                else
                {
                    animator.SetBool("Attack", false);
                 
                }

                
            }

        }
        else
        {
            atkCooldown = 3f;
            animator.SetBool("Attack", false);
            animator.SetBool("isMoving", false);
        }
    }

    void FaceTarget()
    {
        animator.SetBool("isMoving", false);
        Vector3 facingDirection = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(facingDirection.x, 0, facingDirection.z));
        transform.rotation = Quaternion.Slerp(transform.rotation,lookRotation, Time.deltaTime * 5f);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
