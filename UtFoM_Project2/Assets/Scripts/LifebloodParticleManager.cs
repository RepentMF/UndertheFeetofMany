using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifebloodParticleManager : MonoBehaviour
{
    public ParticleSystem ParticleSystemScript;
    public EnemyInfo EnemyInformation;
    // Start is called before the first frame update
    void Start()
    {
        EnemyInformation = this.gameObject.GetComponent<EnemyInfo>();
        ParticleSystemScript = this.gameObject.GetComponent<ParticleSystem>();
        ParticleSystem.Particle[] particles = ParticleSystemScript.Particle[ParticleSystemScript.particleCount];
        // ParticleSystem.Particle[] ParticleArray = ParticleSystemScript.Particle[ParticleSystemScript.particleCount];
        // for (int i = 1; i < ParticleSystemScript.particleCount; i++)
        // {
        // }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
