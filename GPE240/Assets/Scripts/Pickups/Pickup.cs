using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class Pickup : MonoBehaviour
{
    private Collider colliderComponent;
    public UnityEvent OnPickup;

    public void Awake()
    {
        // get our collider
        colliderComponent = GetComponent<Collider>();
        // make sure it's a trigger
        colliderComponent.isTrigger = true;
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        // if we have a collider
        if (colliderComponent != null)
        {
            // destroy ourselves
            Destroy(gameObject);
            // invoke the pickup event
            OnPickup.Invoke();
        }
    }
}
