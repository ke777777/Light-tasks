using UnityEngine;
using System.Collections.Generic;

public class AvoidPlaneParticles : MonoBehaviour
{
    public ParticleSystem myParticleSystem; // 対象のParticle System
    public Transform planeTransform;        // PlaneのTransform
    public List<Transform> buttonPositions; // Buttonの位置リスト
    public float frontDistance = 5.0f;      // Planeの前後で表示しない距離
    public float heightTolerance = 0.5f;    // Plane の高さ方向の範囲
    public float buttonAvoidRadius = 0.5f;  // Button周辺を避ける半径

    void Update()
    {
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[myParticleSystem.particleCount];
        int numParticlesAlive = myParticleSystem.GetParticles(particles);

        for (int i = 0; i < numParticlesAlive; i++)
        {
            Vector3 particlePosition = particles[i].position;
            float planeZ = planeTransform.position.z;
            float planeY = planeTransform.position.y;

            // Planeの前後と高さ範囲内にあるかチェック
            bool withinPlaneBounds = 
                particlePosition.z <= planeZ + frontDistance && 
                particlePosition.z >= planeZ - frontDistance &&
                Mathf.Abs(particlePosition.y - planeY) <= heightTolerance;

            // Button周辺の範囲を避ける
            bool nearButton = IsNearAnyButton(particlePosition);

            // 条件に当てはまる場合、パーティクルを非表示に設定
            if (withinPlaneBounds || nearButton)
            {
                particles[i].remainingLifetime = 0; // パーティクルを非表示
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
                return true; // Button周辺にパーティクルがある
            }
        }
        return false; // Buttonから離れている
    }
}
