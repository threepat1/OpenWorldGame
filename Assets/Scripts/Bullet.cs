using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NaughtyAttributes;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    public float speed = 50f;
    [BoxGroup("Reference")] public GameObject bulletHolePrefab;
    [BoxGroup("Reference")] public Transform line;

    private Rigidbody rigid;
    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rigid.velocity.magnitude > 0f)
        {
            line.transform.rotation = Quaternion.LookRotation(rigid.velocity);
        }
    }
    private void OnCollisionEnter(Collision col)
    {
        //Get contact point from collision
        ContactPoint contact = col.contacts[0];
        //Spawn a bullethole on that contact point
        Instantiate(bulletHolePrefab, contact.point, Quaternion.LookRotation(contact.normal) * Quaternion.AngleAxis(90, Vector3.right));
        // Destroy self
        Destroy(gameObject);
    }
    public void Fire(Vector3 lineOrigin, Vector3 direction)
    {
        line.transform.position = lineOrigin;

        //set bullet flying in direction with spped
        rigid.AddForce(direction * speed, ForceMode.Impulse);
    }
}
