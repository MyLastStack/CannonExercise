using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class CannonScript : MonoBehaviour
{
    [SerializeField] InputAction fireAction;

    [SerializeField] Animator anim;
    [SerializeField] ParticleSystem[] particleSystems;

    [SerializeField] AudioSource[] audioSources;

    // Cannonball
    [SerializeField] GameObject cannonBall, barrelEnd;
    float projectileSpd = 2000.0f;
    float fireRate = 1.0f;
    float timeStamp = -1.0f;
    GameObject[] cannonBallPool;
    public int cbpIndex;

    bool particleTrigger = false;

    // Start is called before the first frame update
    void Start()
    {
        cannonBallPool = new GameObject[5];
        for (int i = 0; i < cannonBallPool.Length; i++)
        {
            cannonBallPool[i] = Instantiate(cannonBall, Vector3.zero, Quaternion.identity);
            cannonBallPool[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (fireAction.WasPressedThisFrame() && Time.time > timeStamp + fireRate)
        {
            anim.SetBool("Firing", true);
            particleTrigger = true;

            cannonBallPool[cbpIndex].SetActive(true);
            cannonBallPool[cbpIndex].transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
            cannonBallPool[cbpIndex].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            cannonBallPool[cbpIndex].transform.position = barrelEnd.transform.position;
            cannonBallPool[cbpIndex].transform.rotation = barrelEnd.transform.rotation;

            cannonBallPool[cbpIndex].GetComponent<Rigidbody>().velocity = Vector3.zero;
            cannonBallPool[cbpIndex].GetComponent<Rigidbody>().AddForce(cannonBallPool[cbpIndex].transform.forward * projectileSpd, ForceMode.Acceleration);

            cbpIndex++;
            if (cbpIndex >= cannonBallPool.Length)
            {
                cbpIndex = 0;
            }

            timeStamp = Time.time;
        }

        if (particleTrigger && AnimatorIsPlaying("CannonAnim"))
        {
            particleSystems[0].Play();
            audioSources[0].Play();
            particleTrigger = false;
        }
    }
    bool AnimatorIsPlaying()
    {
        return anim.GetCurrentAnimatorStateInfo(0).length >
               anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }

    bool AnimatorIsPlaying(string stateName)
    {
        return AnimatorIsPlaying() && anim.GetCurrentAnimatorStateInfo(0).IsName(stateName);
    }

    private void OnEnable()
    {
        fireAction.Enable();
    }

    private void OnDisable()
    {
        fireAction.Disable();
    }
}
