using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Death_Destroy : GameAction 
{ 
    [SerializeField]
    private float deathDelay;

    // Start is called before the first frame update
    public override void Start()
    {
        // get the health component
        Health health = GetComponent<Health>();
        // add our function to the OnDeath event
        health.OnDeath.AddListener(DestroyOnDeath);
    }

    // Update is called once per frame
    public override void Update()
    {
        
    }
    public void DestroyOnDeath()
    {
        // print this object's obituary in the papers
        Debug.Log(gameObject.name + " is dead!");
        // destroy this object
        Destroy(gameObject, deathDelay);
    }
}
