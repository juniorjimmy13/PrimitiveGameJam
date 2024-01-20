using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gun", menuName = "Guns")]
public class GunData : ScriptableObject
{
    // the name of the gun
    public string gunType;

    // damage, fire rate, max distance, current ammo, max ammo, clip size, reload time, reloading will expand upon this later
    public float damage;
    public float fireRate;
    public float MaxDistance;
    
    public int currentAmmo;
    public int maxAmmo;
    public int clipSize;

    public float reloadTime;
    public bool reloading;



}
