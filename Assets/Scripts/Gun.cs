using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gun : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GunData gunData;
    [SerializeField] private Transform cam;
    [SerializeField] private Transform rotator;
    public TextMeshProUGUI ammogui;
    float timeSinceLastShot;
    public LayerMask damagables;

    private void Start()
    {
       PlayerFire.Onfire += Shoot;
       PlayerFire.reloadInput += StartReload;
    }

    private void OnDisable() => gunData.reloading = false;

    public void StartReload()
    {
        if (!gunData.reloading && this.gameObject.activeSelf)
            StartCoroutine(Reload());
    }

    private IEnumerator Reload()
    {
        gunData.reloading = true;
        ammogui.text = "Gun type: " + gunData.name + "\n Ammo: " + "Reloading";
        yield return new WaitForSeconds(gunData.reloadTime);

        gunData.currentAmmo = gunData.clipSize;
        

        gunData.reloading = false;
    }

    private bool CanShoot() => !gunData.reloading && timeSinceLastShot > 1f / (gunData.fireRate / 60f);

    private void Shoot()
    {
        if (gunData.currentAmmo > 0)
        {
            if (CanShoot())
            {
                // array of colliders that are hit by the raycast
               RaycastHit[] hits = Physics.RaycastAll(cam.position, cam.forward, gunData.MaxDistance,damagables);
                for (int i = 0; i < hits.Length; i++)
                {
                    IDamageable damageable = hits[i].transform.GetComponent<IDamageable>();
                    damageable?.TakeDamage(gunData.damage);
                    Debug.Log(hits[i].transform.name);
                }
                gunData.currentAmmo--;
                timeSinceLastShot = 0;
                OnGunShot();
            }
        }
        if (gunData.currentAmmo <= 0)
        {
            ammogui.text = "Gun type: " + gunData.name + "\n Ammo: " + "Out of Ammo";
            StartReload();
        }
    }

    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;
        if(!gunData.reloading)
        ammogui.text = "Gun type: "+gunData.name + "\n Ammo: "+ gunData.currentAmmo.ToString() +"/" + gunData.clipSize.ToString();

        Debug.DrawRay(cam.position, cam.forward * gunData.MaxDistance);
    }

    private void OnGunShot() {
    rotator.Rotate(0, 0, 30);
    
    
    
    }
}