using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using NaughtyAttributes;

public class Enemy : MonoBehaviour, IHealth
{
    public Transform target;
    [ProgressBar("Health", 100, ProgressBarColor.Red)]
    public int health = 100;
    NavMeshAgent agent;

    public float targetDistance;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.position);
        targetDistance = Vector3.Distance(target.position, transform.position);
        if (targetDistance < 1.8f)
        {
            attack();
        }
    }
    public void Heal(int heal)
    {
        health += heal;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void attack()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                print("yeahhhh");
            }
        }
    }
}
