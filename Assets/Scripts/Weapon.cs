using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class Weapon : MonoBehaviour
{
    [BoxGroup("Stats")] public int damage = 10, maxReserve = 500, maxClip = 20;
    [BoxGroup("Stats")] public float spread = 2f, recoil = 1f, range = 10f, shootRate = .2f;
    [BoxGroup("Reference")] public Transform shotOrigin;
    [BoxGroup("Reference")] public GameObject bulletPrefab;

    [HideInInspector] public bool canShoot = false;

    private float shootTimer = 0f;
    private int currentReserve = 0, currentClip = 0;
    // Start is called before the first frame update

    void Start()
    {
        currentReserve = maxReserve;
        currentClip = maxClip;
    }

    // Update is called once per frame
    void Update()
    {
        shootTimer += Time.deltaTime;
        if(shootTimer >= shootRate)
        {
            canShoot = true;
        }
    }
    public void Reload()
    {
        if (currentReserve > 0)
        {
            if (currentClip >= 0)
            {
                currentReserve += currentClip;
                currentClip = 0;
                if(currentReserve >= maxClip)
                {
                    currentReserve -= maxClip - currentClip;
                    currentClip = maxClip;
                }
                else if(currentReserve < maxClip){
                    int tempMag = currentReserve;
                    currentClip = tempMag;
                    currentReserve -= tempMag;

                }
            }
        }
    }
    public void Shoot()
    { 
        // Reset timer & canShoot to false
        currentClip--;
        //Auto-reload
        if(currentClip == 0)
        {
            Reload();
        }
        shootTimer = 0f;
        canShoot = false;

        // Get some values
        Camera attachedCamera = Camera.main; // Note (Manny): Pass the reference into weapon somehow
        Transform camTransform = attachedCamera.transform; // Shortening Camera's Transform to 'camTransform'
        Vector3 bulletOrigin = camTransform.position; // Starting point of the bullet (Centre of Camera)
        Quaternion bulletRotation = camTransform.rotation; // Rotation of the bullet
        Vector3 lineOrigin = shotOrigin.position; // Where the bullet line starts
        Vector3 direction = camTransform.forward; // Forward direction of camera

        //Spawn Bullet
        GameObject clone = Instantiate(bulletPrefab, bulletOrigin, bulletRotation);
        Bullet bullet = clone.GetComponent<Bullet>();
        bullet.Fire(lineOrigin, direction);
    }
}
