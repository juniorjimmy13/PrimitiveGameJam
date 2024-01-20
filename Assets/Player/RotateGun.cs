using UnityEngine;

public class RotateGun : MonoBehaviour {

    
    // the desired rotation of the gun
    private Quaternion desiredRotation;
    // the speed at which the gun rotates
    private float rotationSpeed = 5f;

    void Update() {
       
        desiredRotation = transform.parent.rotation;
        transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, Time.deltaTime * rotationSpeed);
    }

}
