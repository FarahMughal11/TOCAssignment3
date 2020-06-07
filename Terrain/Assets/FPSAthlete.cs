using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class FPSAthlete : MonoBehaviour
{
    public bool collide = false;
    public float Energy;
    public bool runer;
    public bool position;
    public bool stuck = true;
    public Vector3 pos;

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Hurdel")
        {
            collide = true;
            Energy = Energy - 5;
            position = true;
        }
    }
    IEnumerator SuperTimeout()
    {
        //Change state
        if (GameObject.FindObjectOfType<FirstPersonController>().m_CharacterController.velocity == Vector3.zero)
        {
            runer = false;
            stuck = true;
        }
        else if (GetComponent<FirstPersonController>().m_IsWalking)
        {
            runer = true;
            stuck = false;
        }
        yield return null;
    }

    
    // Start is called before the first frame update
    void Start()
    {
        Energy = 100;
        runer = false;
        position = false;
        collide = false;
        stuck = false;
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(SuperTimeout());
        if (position== true)
        {
           position = false;
        }
    }
}
