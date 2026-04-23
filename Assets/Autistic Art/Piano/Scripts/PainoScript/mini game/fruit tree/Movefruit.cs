using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movefruit : MonoBehaviour
{
    [Range(0, 5)]
    public float speed;
    public Vector3 move;
    void Update()
    {

        transform.Translate(move*speed*Time.deltaTime);
        if (transform.localPosition.y < -6f|| transform.localPosition.x < -9f)
            gameObject.SetActive(false);
        
    }
}
