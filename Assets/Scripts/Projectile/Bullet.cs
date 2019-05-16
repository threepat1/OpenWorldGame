using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NaughtyAttributes;

using Projectiles.Effects;

namespace Projectiles
{
    [RequireComponent(typeof(Rigidbody))]
    public class Bullet : Projectile
    {
        public float speed = 50f;
        [BoxGroup("Reference")] public GameObject effectPrefab;
        [BoxGroup("Reference")] public Transform line;

        private Rigidbody rigid;
        private Vector3 start, end;
        // Start is called before the first frame update
        void Awake()
        {
            rigid = GetComponent<Rigidbody>();
        }
        private void Start()
        {
            start = transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            if (rigid.velocity.magnitude > 0f)
            {
                line.rotation = Quaternion.LookRotation(rigid.velocity);
            }
        }
        private void OnCollisionEnter(Collision col)
        {
            end = transform.position;
            //Get contact point from collision
            ContactPoint contact = col.contacts[0];

            // Get Bullet Direction
            Vector3 bulletDir = end - start;

            //Jordan
            Quaternion lookRotation = Quaternion.LookRotation(bulletDir);
            Quaternion rotation = lookRotation * Quaternion.AngleAxis(-90, Vector3.right);

            //Spawn a bullethole on that contact point
            GameObject clone = Instantiate(effectPrefab, contact.point, rotation);

            //Get angle between normal and bullet dir
            float impactAngle = 180 - Vector3.Angle(bulletDir, contact.normal);
            clone.transform.localScale = clone.transform.localScale / (1 + impactAngle / 45);
            // Destroy self
            Effect effect = clone.GetComponent<Effect>();
            effect.damage += damage;
            effect.hitObject = col.transform;

            Destroy(gameObject);
        }
        public override void Fire(Vector3 lineOrigin, Vector3 direction)
        {
            // Set line position to origin
            line.position = lineOrigin;

            //set bullet flying in direction with spped
            rigid.AddForce(direction * speed, ForceMode.Impulse);
        }
    }
}
