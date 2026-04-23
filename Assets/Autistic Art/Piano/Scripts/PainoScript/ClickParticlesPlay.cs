using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickParticlesPlay : MonoBehaviour
{
    public Transform playingArea;
    public Transform IntialArea;
    public float activeTime=2;
    ParticleSystem partSys;
    private void Awake()
    {
        partSys= GetComponent<ParticleSystem>();
    }
    private void OnEnable()
    {
        transform.parent = playingArea;
        partSys.Play();
        StartCoroutine(BackToArea());
    }
    IEnumerator BackToArea() {
        yield return new WaitForSeconds(activeTime);
        transform.parent = IntialArea;
        gameObject.SetActive(false);
    }
}
