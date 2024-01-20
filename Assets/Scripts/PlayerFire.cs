using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public static Action Onfire;
    public static Action reloadInput;

    [SerializeField] private KeyCode reloadKey = KeyCode.R;
    // Update is called once per frame
    void Update()
    { 
        if (Input.GetMouseButtonDown(0))
        {
            Onfire?.Invoke();
        }
        if (Input.GetKeyDown(reloadKey))
            reloadInput?.Invoke();
    }
}
