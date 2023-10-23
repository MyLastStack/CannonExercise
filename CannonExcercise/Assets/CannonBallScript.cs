using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallScript : MonoBehaviour
{
    [SerializeField] AudioSource explode;
    [SerializeField] GameObject explosive;

    public bool isPlayed = false;
    float countdown = 4.0f;
    public bool gone = false;

    // Start is called before the first frame update
    void Start()
    {
        isPlayed = false;
        gone = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gone)
        {
            gone = false;
            StartCoroutine(Disappear(countdown, gameObject));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isPlayed)
        {
            explode.Play();
            Instantiate(explosive, gameObject.transform.position, Quaternion.identity);
            gameObject.transform.localScale = Vector3.zero;
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
            gameObject.transform.GetComponent<Rigidbody>().velocity = Vector3.zero;

            gone = true;
        }

        isPlayed = false;
    }

    IEnumerator Disappear(float time, GameObject gameObject)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
}
