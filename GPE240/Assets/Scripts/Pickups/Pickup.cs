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
        colliderComponent = GetComponent<Collider>();
        colliderComponent.isTrigger = true;
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (colliderComponent != null)
        {
            Destroy(gameObject);
            OnPickup.Invoke();
        }
    }
}
