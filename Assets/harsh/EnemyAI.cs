using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    public Transform Target;
    public float AtRange;
    public float Speed;
    public float RotationSpeed;
    public float RunSpeed;
    public float SprintDistance;
    public Animator anim;
    // Update is called once per frame
    void Update()
    {
        var _distance = Mathf.Abs(transform.position.z - Target.position.z);
        float _speed;
        if (_distance < AtRange )
        {

                anim.SetBool("IsRunning", false);
                anim.SetBool("IsWalking", false);
                anim.SetBool("IsIdle", false);
                anim.SetBool("IsAttacking", true);


        }

        else
        {
            if (_distance > SprintDistance)
            {
                _speed = RunSpeed;
                //run animation
                anim.SetBool("IsIdle", false);
                anim.SetBool("IsRunning", true);
                anim.SetBool("IsWalking", false);
                anim.SetBool("IsAttacking", false);
            }
            else
            {
                _speed = Speed;
                //walk animation
                anim.SetBool("IsIdle", false);
                anim.SetBool("IsRunning", false);
                anim.SetBool("IsWalking", true);
                anim.SetBool("IsAttacking", false);
            }
            // Chase
            transform.position = Vector3.MoveTowards(transform.position, Target.position, _speed * Time.deltaTime);
            Vector3 direction = (Target.position - transform.position).normalized;
            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                targetRotation.x = 0f;
                targetRotation.z = 0f;
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, RotationSpeed * Time.deltaTime);
            }
        }
    }
}

