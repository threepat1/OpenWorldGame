using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;
using NaughtyAttributes;

[RequireComponent(typeof(Player))]
public class Shooting : MonoBehaviour
{
    [BoxGroup("Weapon")]public List<Weapon> weapons = new List<Weapon>();
    [BoxGroup("Weapon")] public int currentWeaponIndex = 0;
    [BoxGroup("Weapon")] public Weapon currentWeapon;

    public Player player;
    private CameraLook cameraLook;
    // Start is called before the first frame update
    void Awake()
    {
        player = GetComponent<Player>();
        cameraLook = GetComponent<CameraLook>();
    }

    // Update is called once per frame
    void Start()
    {
        weapons = GetComponentsInChildren<Weapon>().ToList();
        SelectWeapon(0);
    }
    private void Update()
    {
        if (currentWeapon)
        {
            bool fire1 = Input.GetButton("Fire1");
            if (fire1)
            {
                //Check if weapon can shoot
                if (currentWeapon.canShoot)
                {
                    currentWeapon.Shoot();
                    Vector3 euler = Vector3.up * 2f;
                    euler.x = Random.Range(-1f, 1f);
                    cameraLook.SetTargetOffset(euler * currentWeapon.recoil);
                }
            }
        }
    }
    void DisableAllWeapon()
    {
        foreach(var item in weapons)
        {
            item.gameObject.SetActive(false);
        }
    }

    void SelectWeapon(int index)
    {
        if(index >= 0 && index < weapons.Count)
        {
            DisableAllWeapon();
            currentWeapon = weapons[index];
            currentWeapon.gameObject.SetActive(true);
            currentWeaponIndex = index;
        }
    }
}
