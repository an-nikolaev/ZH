using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroyScript : MonoBehaviour
{
    public int gunDamage;
    private void OnEnable()
    {
        Invoke("Destroy", 111f);
//        Debug.Log(GetComponent<ParticleSystem>().randomSeed);
    }

    private void Destroy()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    void OnParticleCollision(GameObject other)
    {
//        Debug.Log("Hit!");
//        GameObject go = Instantiate(other, transform);
//        go.tag = "Lol";
//        if (other.rigidbody != null)
//        {
//                    other.rigidbody.AddForce(-hit.normal * hitForce);
//        }
        Rigidbody body = other.GetComponent<Rigidbody>();
        if (body)
        {
//            Debug.Log(other);
//            Debug.Log("Body hit!");
            ShootableBox health = other.GetComponent<ShootableBox>();
            if (health != null)
            {
                health.Damage(gunDamage);
            }
//            Vector3 direction = other.transform.position - transform.position;
//            direction = direction.normalized;
//            body.AddForce(direction * 5000);
        }
    }
}