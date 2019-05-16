using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;
using NaughtyAttributes;

[RequireComponent(typeof(Player),typeof(CameraLook))]
public class Combat : MonoBehaviour, IHealth
{
    [ProgressBar("Health", 100, ProgressBarColor.Green)]
    public int health = 100;
    [BoxGroup("Weapon")] public Weapon currentWeapon;
    [BoxGroup("Weapon")]public List<Weapon> weapons = new List<Weapon>();
    [BoxGroup("Weapon")] public int currentWeaponIndex = 0;

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
        // Get all weapons attached to Player
        weapons = GetComponentsInChildren<Weapon>().ToList();
        // Select first one
        SelectWeapon(0);
    }
    void FixedUpdate()
    {
        // If there is a weapon
        if (currentWeapon)
        {
            bool fire1 = Input.GetButton("Fire1");
            if (fire1)
            {
                //Check if weapon can shoot
                if (currentWeapon.canShoot)
                {
                    //Shoot the weapon
                    currentWeapon.Attack();
                    //// Apply Weapon Recoil
                    //Vector3 euler = Vector3.up * 2f;
                    //// Randomize the pitch
                    //euler.x = Random.Range(-1f, 1f);
                    //// Apply offset to camera using weapon recoil
                    //cameraLook.SetTargetOffset(euler * currentWeapon.recoil);
                }
            }
        }
    }
    void DisableAllWeapons()
    {
        //Loop through all weapons
        foreach(var item in weapons)
        {
            //Disable each Gameobject
            item.gameObject.SetActive(false);
        }
    }

    void SelectWeapon(int index)
    {
        // Check if index is within bounds
        if (index >= 0 && index < weapons.Count)
        {
            // Disable all weapons
            DisableAllWeapons();
            // Select currentWeapon
            currentWeapon = weapons[index];
            // Enable currentWeapon
            currentWeapon.gameObject.SetActive(true);
            // Update currentWeaponIndex
            currentWeaponIndex = index;
        }
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            print("DEAD!!!");
        }
    }
    public void Heal(int heal)
    {
        health += heal;
    }
}
