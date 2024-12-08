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
        Health health = GetComponent<Health>();
        particles = GetComponent<ParticleSystem>();
        health.OnDeath.AddListener(DeployParticles);
    }

    // Update is called once per frame
    public void DeployParticles()
    {
        particles.Play();
    }
}
