using UnityEngine;
using System.Collections.Generic;

public class AvoidPlaneParticles : MonoBehaviour
{
    public ParticleSystem myParticleSystem; // �Ώۂ�Particle System
    public Transform planeTransform;        // Plane��Transform
    public List<Transform> buttonPositions; // Button�̈ʒu���X�g
    public float frontDistance = 5.0f;      // Plane�̑O��ŕ\�����Ȃ�����
    public float heightTolerance = 0.5f;    // Plane �̍��������͈̔�
    public float buttonAvoidRadius = 0.5f;  // Button���ӂ�����锼�a

    void Update()
    {
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[myParticleSystem.particleCount];
        int numParticlesAlive = myParticleSystem.GetParticles(particles);

        for (int i = 0; i < numParticlesAlive; i++)
        {
            Vector3 particlePosition = particles[i].position;
            float planeZ = planeTransform.position.z;
            float planeY = planeTransform.position.y;

            // Plane�̑O��ƍ����͈͓��ɂ��邩�`�F�b�N
            bool withinPlaneBounds = 
                particlePosition.z <= planeZ + frontDistance && 
                particlePosition.z >= planeZ - frontDistance &&
                Mathf.Abs(particlePosition.y - planeY) <= heightTolerance;

            // Button���ӂ͈̔͂������
            bool nearButton = IsNearAnyButton(particlePosition);

            // �����ɓ��Ă͂܂�ꍇ�A�p�[�e�B�N�����\���ɐݒ�
            if (withinPlaneBounds || nearButton)
            {
                particles[i].remainingLifetime = 0; // �p�[�e�B�N�����\��
            }
        }

        myParticleSystem.SetParticles(particles, numParticlesAlive);
    }

    private bool IsNearAnyButton(Vector3 particlePosition)
    {
        foreach (Transform button in buttonPositions)
        {
            if (Vector3.Distance(particlePosition, button.position) < buttonAvoidRadius)
            {
                return true; // Button���ӂɃp�[�e�B�N��������
            }
        }
        return false; // Button���痣��Ă���
    }
}
