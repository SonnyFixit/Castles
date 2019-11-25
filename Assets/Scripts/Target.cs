using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{

    public static bool hitConfirm = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Projectile")
        {
            hitConfirm = true;
            Destroy(this.gameObject);
        }
    }
}
