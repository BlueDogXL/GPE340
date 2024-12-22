using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent (typeof (Health))]
[RequireComponent (typeof (ParticleSystem))]
public class Death_Particles : GameAction
{
    ParticleSystem particles;
    // Start is called before the first frame update
    public override void Start()
    {
        // get our health component
        Health health = GetComponent<Health>();
        // get our particle system
        particles = GetComponent<ParticleSystem>();
        // add our function to the OnDeath event
        health.OnDeath.AddListener(DeployParticles);
    }

    public void DeployParticles()
    {
        // play our particles
        particles.Play();
    }
}
