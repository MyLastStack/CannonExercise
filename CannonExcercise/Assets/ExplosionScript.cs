using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    [SerializeField] ParticleSystem _particleSystem;

    public float cTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cTime += Time.deltaTime;
        if (cTime >= _particleSystem.startLifetime)
        {
            Destroy(gameObject);
        }
    }
}
