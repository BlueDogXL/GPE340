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
        // get our rigidbody
        rb = GetComponent<Rigidbody>();
        // live fast and die young
        Destroy(gameObject, lifespan);
    }

    // Update is called once per frame
    void Update()
    {
        // if the game is paused don't do anything
        if (GameManager.instance.isPaused) return;
        // apply force to our rigidbody
        rb.velocity = transform.forward * speed;
    }

    public void OnTriggerEnter(Collider other)
    {
        // admit to our crime in a court of law
        Debug.Log("Rocket hit " + other.gameObject.name);
        // get the other thing's health component
        Health otherHealth = other.GetComponent<Health>();
        // if the other thing actually has one of those
        if (otherHealth != null )
        {
            // bring the pain
            otherHealth.TakeDamage(damage);
        }
        // our life's mission is complete so we can die peacefully
        Destroy(gameObject);
    }
}
