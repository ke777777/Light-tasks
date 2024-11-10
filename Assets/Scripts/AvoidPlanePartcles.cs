using UnityEngine;

public class AvoidPlaneParticles : MonoBehaviour
{
    public ParticleSystem myParticleSystem; // �Ώۂ�Particle System
    public Transform planeTransform;        // Plane��Transform
    public float frontDistance = 5.0f;      // Plane�̑O��ŕ\�����Ȃ�����
    public float heightTolerance = 0.5f;    // Plane �̍��������͈̔�

    void Update()
    {
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[myParticleSystem.particleCount];
        int numParticlesAlive = myParticleSystem.GetParticles(particles);

        for (int i = 0; i < numParticlesAlive; i++)
        {
            Vector3 particlePosition = particles[i].position;
            float planeZ = planeTransform.position.z;
            float planeY = planeTransform.position.y;

            // Plane�̑O��ƍ����͈͓��ɂ���p�[�e�B�N�����폜
            if (particlePosition.z <= planeZ + frontDistance && 
                particlePosition.z >= planeZ - frontDistance &&
                Mathf.Abs(particlePosition.y - planeY) <= heightTolerance)
            {
                particles[i].remainingLifetime = 0; // �p�[�e�B�N�����\��
            }
        }

        myParticleSystem.SetParticles(particles, numParticlesAlive);
    }
}
