using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Projectiles.Effects
{
    public abstract class Projectile : MonoBehaviour
    {
        public int damage = 5;
        public abstract void Fire(Vector3 origin, Vector3 direction);
    }
}
