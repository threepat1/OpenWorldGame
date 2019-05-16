using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Projectiles.Effects
{
    public class Effect : MonoBehaviour
    {
        public float effectRate = 1f;
        public int damage = 2;
        private float effectTimer = 0f;
        [Tooltip("What visual effect to spawn as a child to the thing we hit")]
        public GameObject visualEffectPrefab;
        [HideInInspector] public Transform hitObject;

        // Update is called once per frame
        protected virtual void Start()
        {
            GameObject clone = Instantiate(visualEffectPrefab, hitObject.transform);
            clone.transform.position = transform.position;
            clone.transform.rotation = transform.rotation;
        }
        protected virtual void Update()
        {
            effectTimer += Time.deltaTime;
            if (effectTimer >= 1f / effectRate)
            {
                RunEffect();
            }
        }
        public abstract void RunEffect();
       
    }

}
