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
        Health health = GetComponent<Health>();
        health.OnDeath.AddListener(DestroyOnDeath);
    }

    // Update is called once per frame
    public override void Update()
    {
        
    }
    public void DestroyOnDeath()
    {
        Debug.Log(gameObject.name + " is dead!");
        Destroy(gameObject, deathDelay);
    }
}
