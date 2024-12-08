using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float damage;
    public float speed;
    public float lifespan;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, lifespan);
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.forward * speed;
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Rocket hit " + other.gameObject.name);
        Health otherHealth = other.GetComponent<Health>();
        if (otherHealth != null )
        {
            otherHealth.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
