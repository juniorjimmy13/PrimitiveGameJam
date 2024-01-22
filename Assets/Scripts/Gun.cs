using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gun : MonoBehaviour
{
    [Header("References")]
    public GunData gunData;
    [SerializeField] private Transform cam;
    [SerializeField] private Transform rotator;
    public TextMeshProUGUI ammogui;
    float timeSinceLastShot;
    public LayerMask damagables;
    private void Awake()
    {
        PlayerFire.Onfire += Shoot;
        PlayerFire.reloadInput += StartReload;
    }

    private void Start()
    {
        if (gunData == null)
        {
            Debug.Log("Missing gun data");
        }
    }

    private void OnEnable()
    {
         PlayerFire.Onfire += Shoot;
        PlayerFire.reloadInput += StartReload;
    }

    private void OnDisable() => gunData.reloading = false;

    public void StartReload()
    {
        if (gunData != null)
        {
            if (!gunData.reloading && this.gameObject.activeInHierarchy)
                StartCoroutine(Reload());
        }
        
    }

    private IEnumerator Reload()
    {

        if (gunData == null)
        {
            Debug.LogError("Gun data is null in Reload coroutine");
            yield break;
        } 
        else
        {
            gunData.reloading = true;
            ammogui.text = "Gun type: " + gunData.name + "\n Ammo: " + "Reloading";
            yield return new WaitForSeconds(gunData.reloadTime);

            gunData.currentAmmo = gunData.clipSize;


            gunData.reloading = false;
        }
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
                Debug.Log(hits.Length);
                if (hits != null)
                {
                    for (int i = 0; i < hits.Length; i++)
                    {
                        IDamageable damageable = hits[i].transform.GetComponent<IDamageable>();
                        damageable?.TakeDamage(gunData.damage);
                        Debug.Log(hits[i].transform.name);
                    }
                }
                AudioInstance.AudioInstance1.PlaySpecificAudioClip(AudioInstance.AudioInstance1.fire);
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