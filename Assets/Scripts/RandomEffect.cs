using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEffect : MonoBehaviour
{
    public ParticleSystem[] EffectsParticles;
    // Start is called before the first frame update
    void Start()
    {
        int randomEffct =Random.Range(0, EffectsParticles.Length);
        EffectsParticles[randomEffct].gameObject.SetActive(true);
        EffectsParticles[randomEffct].Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
