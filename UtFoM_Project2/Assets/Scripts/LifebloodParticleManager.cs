using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class LifebloodParticleManager : MonoBehaviour
{
    public ParticleSystem ParticleSystemScript;
    public ParticleSystem.Particle[] ParticleArray;
    public EnemyInfo EnemyInformation;
    public LifebloodParticle particle;
    public int particlesCount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (EnemyInformation == null)
        {
            EnemyInformation = this.gameObject.GetComponent<EnemyInfo>();
            ParticleSystemScript = this.gameObject.GetComponent<ParticleSystem>();
            ParticleArray = new ParticleSystem.Particle[8];
            particlesCount = ParticleSystemScript.GetParticles(ParticleArray);
            //Debug.Log(particlesCount);
        }

        for (int i = 0; i < particlesCount; i++)
        {
            if (ParticleArray[i].remainingLifetime < 0.02f && ParticleArray[i].remainingLifetime > 0.0f)
            {
                Debug.Log(i + ", " + ParticleArray[i].remainingLifetime);
                Vector3 tempPos = transform.TransformPoint(ParticleArray[i].position);
                Instantiate(particle, tempPos, Quaternion.identity);
            }
        }
    }
}
