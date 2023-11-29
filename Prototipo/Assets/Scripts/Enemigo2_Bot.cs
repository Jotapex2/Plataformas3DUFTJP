using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo2_Bot : MonoBehaviour
{
    private Transform target;

    public SphereCollider colider;
    public float radius = 10f;
    public float speed = 5f;

    public Transform pivot;
    public float shootSpeed = 5f;
    private float shootCurrent = 0f;

    [Header("Prefs")]
    public GameObject bulletPref;


    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPref, pivot.position, pivot.rotation);
        shootCurrent = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        colider.radius = radius / (float)this.transform.localScale.y;

        if (target != null)
        {
            // obtengo vector direccion
            var direction = target.position - transform.position;
            direction.y = 0;

            //shoot timing
            if (shootCurrent >= shootSpeed)
            {
                Shoot();
            }
            shootCurrent += Time.deltaTime;

            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);


        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var damageble = other.GetComponent<IDamageable>();
        if (damageble != null)
        {
            target = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var damageble = other.GetComponent<IDamageable>();
        if (damageble != null)
        {
            target = null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = (target != null) ? Color.green : Color.red;

        Gizmos.DrawWireSphere(this.transform.position, radius);
    }

}
