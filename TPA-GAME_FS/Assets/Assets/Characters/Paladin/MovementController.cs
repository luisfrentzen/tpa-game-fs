using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    //public EnemyStats enemyStats;
    public Animator animator;
    public float Speed;
    // Start is called before the first frame update

    void Start()
    {
        //enemyStats = GetComponent<EnemyStats>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
    }

    void PlayerMovement()
    {
        
        if(PauseGame.isPaused == false)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            Vector3 PlayerMove = new Vector3(horizontal, 0f, vertical) * Speed * Time.deltaTime;
            transform.Translate(PlayerMove, Space.Self);
            if (Input.GetKey("w") == true)
            {
                animator.SetBool("isRunning", true);
                animator.SetBool("walkRight", false);
                animator.SetBool("walkLeft", false);
                animator.SetBool("walkBack", false);
            }
            else if (Input.GetMouseButton(0) == true)
            {
                animator.SetBool("Attack1", true);

            }
            else if (Input.GetKey("d") == true)
            {
                animator.SetBool("walkRight", true);
            }
            else if (Input.GetKey("a") == true)
            {
                animator.SetBool("walkLeft", true);
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                animator.SetBool("isJumping", true);
            }
            else if (Input.GetKey("s") == true)
            {
                animator.SetBool("walkBack", true);
            }
            else
            {
                animator.SetBool("isRunning", false);
                animator.SetBool("isJumping", false);
                animator.SetBool("walkRight", false);
                animator.SetBool("walkLeft", false);
                animator.SetBool("walkBack", false);
                animator.SetBool("Attack1", false);
            }
        }
       
    }

}
