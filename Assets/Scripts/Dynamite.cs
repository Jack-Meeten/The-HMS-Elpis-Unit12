using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dynamite : MonoBehaviour
{
    private GameObject hole;
    public ParticleSystem exp1;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "IceWall")
        {
            hole = other.gameObject;
            Debug.Log("RUN");
            StartCoroutine(Explode());
        }
    }

    IEnumerator Explode()
    {
        yield return new WaitForSecondsRealtime(4);
        hole.SetActive(false);
        Instantiate(exp1, transform.position, transform.rotation);
        Destroy(transform.parent.gameObject);
    }
}
