using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    static public GameObject C;
    public float camZ;
    public float ease = 0.05f;

    private void Awake()
    {
        camZ = this.transform.position.z;
    }

  
    void FixedUpdate()
    {

     



        Vector3 direction;
        if (C == null)
        {
            direction = Vector3.zero;
        }
         else
        {
            direction = C.transform.position;

            if (C.tag == "Projectile")
            {
                if (C.GetComponent<Rigidbody>().IsSleeping())
                {
                    C = null;
                    return;
                }
            }
        }

        direction = Vector3.Lerp(transform.position, direction, ease);
        direction.z = camZ;
        transform.position = direction;

    }
}
