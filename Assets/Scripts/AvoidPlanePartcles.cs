using UnityEngine;

public class AvoidPlaneParticles : MonoBehaviour
{
    public ParticleSystem myParticleSystem; // 対象のParticle System
    public Transform planeTransform;        // PlaneのTransform
    public float frontDistance = 5.0f;      // Planeの前後で表示しない距離
    public float heightTolerance = 0.5f;    // Plane の高さ方向の範囲

    void Update()
    {
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[myParticleSystem.particleCount];
        int numParticlesAlive = myParticleSystem.GetParticles(particles);

        for (int i = 0; i < numParticlesAlive; i++)
        {
            Vector3 particlePosition = particles[i].position;
            float planeZ = planeTransform.position.z;
            float planeY = planeTransform.position.y;

            // Planeの前後と高さ範囲内にあるパーティクルを削除
            if (particlePosition.z <= planeZ + frontDistance && 
                particlePosition.z >= planeZ - frontDistance &&
                Mathf.Abs(particlePosition.y - planeY) <= heightTolerance)
            {
                particles[i].remainingLifetime = 0; // パーティクルを非表示
            }
        }

        myParticleSystem.SetParticles(particles, numParticlesAlive);
    }
}
