using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class AttachGameObjectsToParticles : MonoBehaviour
{
    public enum AttachType
    {
        light, spawnBlood
    }
    public GameObject m_Prefab;
    public AttachType attachType;
    private ParticleSystem m_ParticleSystem;
    private List<GameObject> m_Instances = new List<GameObject>();
    private ParticleSystem.Particle[] m_Particles;

    // Start is called before the first frame update
    void Start()
    {
        m_ParticleSystem = GetComponent<ParticleSystem>();
        m_Particles = new ParticleSystem.Particle[m_ParticleSystem.main.maxParticles];
    }

    // Update is called once per frame
    void LateUpdate()
    {
        int count = m_ParticleSystem.GetParticles(m_Particles);
        if (attachType == AttachType.light)
        {
            while (m_Instances.Count < count)
                m_Instances.Add(Instantiate(m_Prefab, m_ParticleSystem.transform));
        }

        bool worldSpace = (m_ParticleSystem.main.simulationSpace == ParticleSystemSimulationSpace.World);
        for (int i = 0; i < count; i++)
        {
            if (i < count)
            {
                if (worldSpace)
                {
                    if (attachType == AttachType.light)
                    {
                        m_Instances[i].transform.position = m_Particles[i].position;
                    }
                    else if (attachType == AttachType.spawnBlood)
                    {
                        if (m_Particles[i].remainingLifetime < 0.05f)
                        {
                            if (Random.Range(0, 100) == 80)
                            {
                                Instantiate(PrefabsStatic.BloodFloor, m_Particles[i].position, Quaternion.Euler(0,0,Random.Range(0f,360f)));
                            }
                        }
                    }
                }
                else
                {
                    if (attachType == AttachType.light)
                    {
                        m_Instances[i].transform.localPosition = m_Particles[i].position;
                    }
                }
                if (attachType == AttachType.light)
                {
                    m_Instances[i].SetActive(true);
                }
            }
            else
            {
                m_Instances[i].SetActive(false);
            }
        }
    }
}
