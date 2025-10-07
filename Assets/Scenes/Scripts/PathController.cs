using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathController : MonoBehaviour
{
    [SerializeField]
    public PathManager pathManager;

    List<wayPoint> thePath;
    wayPoint target;
    public float MoveSpeed;
    public float RotateSpeed;

    public Animator animator;
    bool isWalking;

    void Start()
    {
        isWalking = false;
        animator.SetBool("isWalking", isWalking);

        thePath = pathManager.GetPath();
        if(thePath != null && thePath.Count > 0)
        {
            Debug.Log(thePath.Count);
            target = thePath[0];
        }    

    }

    void rotateTowardsTarget()
    {
        float stepSize = RotateSpeed * Time.deltaTime;

        Vector3 targetDir = target.pos - transform.position;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, stepSize, 0.0f);
        //Debug.Log(newDir);
        transform.rotation = Quaternion.LookRotation(newDir);
    }
    void moveForward()
    {
        float stepSize = Time.deltaTime * MoveSpeed;
        float distanceToTarget = Vector3.Distance(transform.position, target.pos);
        if (distanceToTarget < stepSize)
        {
            return;

        }
        Vector3 moveDir = Vector3.forward;
        //Debug.Log(moveDir);
        transform.Translate(moveDir * stepSize);
        
    }

    void Update()
    {
        if(Input.anyKeyDown)
        {
            isWalking = !isWalking;
            animator.SetBool("isWalking", isWalking );

        }
        if (isWalking)
        {
            rotateTowardsTarget();
            moveForward();
        }

       

    }

    private void OnTriggerEnter(Collider other)
    {
        // switch to next target
        target = pathManager.GetNextTarget();

        if (other.CompareTag("Wall"))
        {
            animator.SetBool("isWalking", false);
            isWalking = false;
        }
    }

    


}
