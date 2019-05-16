using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Projectiles
{
    public abstract class Projectile : MonoBehaviour
    {
        public abstract void Fire(Vector3 origin, Vector3 direction);
        public int damage = 5;
    }
}
