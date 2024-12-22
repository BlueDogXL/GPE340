using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Death_Explode : GameAction
{
    // my main goal is to blow up, and act like i don't know nobodayyyyyy

    // anyways

    public float explosionRadius;
    public float explosionDamage;
    public float explosionPushForce;
 

    public override void Start()
    {
        // figured i'd make it auto-add like the other one
        // was gonna be like 'oh you can just add it to the list manually in case you don't want the enemy to explode'
        // but like. you make that decision by either including or not including this component anyways so might as well make it easy

        // get our health component
        Health health = GetComponent<Health>();
        // add our function to the OnDeath event
        health.OnDeath.AddListener(Explode);
    }
    public void Explode()
    {
        // grab all colliders in sphere
        Collider[] explodedColliders = Physics.OverlapSphere(transform.position, explosionRadius); 
        // iterate through them
        Debug.Log("Number of hits: " + explodedColliders.Length);
        for (int i = 0; i < explodedColliders.Length; i++)
        {
            // if it's not us (yeah i caused a stack overflow because i didn't check this)
            if (explodedColliders[i].gameObject != gameObject)
            {
                Debug.Log("Collider of " + explodedColliders[i].gameObject.name);
                // if it's got health
                if (explodedColliders[i].gameObject.GetComponent<Health>() != null)
                {
                    Debug.Log("Explosion damaging" + explodedColliders[i].gameObject.name + "!");
                    // damage it
                    explodedColliders[i].gameObject.GetComponent<Health>().TakeDamage(explosionDamage);
                }
                // if it's got a rigidbody
                if (explodedColliders[i].gameObject.GetComponent<Rigidbody>() != null)
                {
                    Debug.Log("Explosion pushing" + explodedColliders[i].gameObject.name + "!");
                    // get the direction of 'away'
                    Vector3 pushDirection = explodedColliders[i].transform.position - transform.position;
                    // and push
                    explodedColliders[i].gameObject.GetComponent<Rigidbody>().AddForce(pushDirection * explosionPushForce);
                }
            }
        }
    }
}
