using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ZombieFollow : MonoBehaviour {

	public GameObject Player;
    public Transform objPlayer;
    public float targetDistance;
    public float allowedRange = 200;
    public GameObject objEnemy;
    public float enemySpeed;
    public int attackTrigger;
    public RaycastHit shot;

    private void Update()
    {
        transform.LookAt(objPlayer);
  
        if (Physics.Raycast(transform.position,transform.TransformDirection(Vector3.forward), out shot))
        {
            targetDistance = shot.distance;

            if (targetDistance < allowedRange)
            {
                enemySpeed = 0.05f;
                if (attackTrigger == 0)
                {
                    objEnemy.GetComponent<Animation>().Play("Walking");
                    transform.position = Vector3.MoveTowards (transform.position, objPlayer.position, enemySpeed);
                }
            }
            else
            {
                enemySpeed = 0;
                objEnemy.GetComponent<Animation>().Play("Idle");
            }
        }

        if(attackTrigger == 1)
        {
            enemySpeed = 0;
            objEnemy.GetComponent<Animation>().Play("Attacking");
            //objEnemy.transform.rotation = new Quaternion(0, 0, 0, 0);
			//player loses health
			SceneManager.LoadScene(0);
        }

        else
        {
            enemySpeed = 5.0f;
            objEnemy.GetComponent<Animation>().Play("Walking");
        }
    } 

    void OnTriggerEnter()
    {
        attackTrigger = 1;
    }

    void OnTriggerExit()
    {
        attackTrigger = 0;
    }
}
