using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    public GameObject shootPoint;
    public GameObject prefabProjectile;
    public GameObject projectile;

    private Rigidbody projectileRigid;

    public Vector3 launchPosition;
    
    public bool isAiming;

    public float multiplierVelocity = 10f;

    private void Awake()
    {
        Transform shootPointPos = transform.Find("ShootPoint");
        shootPoint = shootPointPos.gameObject;
        shootPoint.SetActive(false);


        launchPosition = shootPointPos.position;
        
        
    }

    private void Update()
    {
        if (isAiming == true)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = -Camera.main.transform.position.z;
            Vector3 mousePos2 = Camera.main.ScreenToWorldPoint(mousePos);

            Vector3 difference = mousePos2 - launchPosition;

            float maxMagnitude = this.GetComponent<SphereCollider>().radius;


            if(difference.magnitude > maxMagnitude)
            {
                difference.Normalize();
                difference *= maxMagnitude;
            }


            Vector3 projectilePosition = launchPosition + difference;
            projectile.transform.position = projectilePosition;

            if (Input.GetMouseButton(0))
            {
                isAiming = false;
                projectileRigid.isKinematic = false;
                projectileRigid.velocity = -difference * multiplierVelocity;
                Follow.C = projectile;
                projectile = null;
                Game.ShotFired();
            }
        }
    }


    private void OnMouseEnter()
    {
        
        
        shootPoint.SetActive(true);
        
    }

    private void OnMouseExit()
    {
        shootPoint.SetActive(false);
    }

    private void OnMouseDown()
    {
        isAiming = true;
        projectile = Instantiate(prefabProjectile) as GameObject;
        projectile.transform.position = launchPosition;

        projectile.GetComponent<Rigidbody>().isKinematic = true;

        projectileRigid = projectile.GetComponent<Rigidbody>();
        projectileRigid.isKinematic = true;
    }



}
