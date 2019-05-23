using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Projectiles.Effects
{
  public abstract class Effect : MonoBehaviour
  {
    public float eventRate = 5f;
    public int damage = 2;
    [Tooltip("What Visual Effect to Spawn as a child to the thing we hit.")]
    public GameObject visualEffectPrefab;
    [HideInInspector] public Transform hitObject;

    private float eventTimer = 0f;

    protected virtual void Start()
    {
      GameObject clone = Instantiate(visualEffectPrefab, hitObject.transform);
      clone.transform.position = transform.position;
      clone.transform.rotation = transform.rotation;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
      eventTimer += Time.deltaTime;
      if (eventTimer >= 1f / eventRate)
      {
        RunEvent();
      }
    }

    public abstract void RunEvent();
  }
}
