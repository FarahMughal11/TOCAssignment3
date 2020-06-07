using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;

public class CheckMyVision : MonoBehaviour
{
    //How sensitive we are about vision/Line of sight?
    public enum enumSensitivity { HIGH ,LOW };

    //varible to check sensitivity
    public enumSensitivity sensitivity = enumSensitivity.HIGH;

    //are we able to see the target right now?
    public bool targetInsight = false;

    //feild of vision
    public float fieldofVision = 45f;

    //we need a reference to our target here as well
    private Transform target = null;

    //reference to our eyes yet to add
    public Transform myEyes = null;

    //my transform component?
    public Transform npcTransform = null;

    //my sphere collider
    private SphereCollider sphereCollider = null;

    //last know sighting of objects?
    public Vector3 lastKnowsSighting = Vector3.zero;

    private void Awake()
    {
        npcTransform = GetComponent<Transform>();
        sphereCollider = GetComponent<SphereCollider>();
        lastKnowsSighting = npcTransform.position;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    bool InMyFieldOfVision()
    {
        Vector3 dirToTarget = target.position - myEyes.position;
        //Get angle between forward and view direction
        float angle = Vector3.Angle(myEyes.forward, dirToTarget);
        //let us check if within field of view
        if (angle <= fieldofVision)
            return true;
        else
            return false;
    }
    //we need a function to check line of sight
    bool ClearLineOfSight()
    {
        RaycastHit hit;
        if (Physics.Raycast(myEyes.position, (target.position - myEyes.position).normalized, out hit, sphereCollider.radius)) ;
        {
            if(hit.transform.CompareTag("Player"))
            {
                return true;
            }
        }
        return false;
    }
    void UpdateSight()
    {
        switch (sensitivity)
        {
            case enumSensitivity.HIGH:
                targetInsight = InMyFieldOfVision() && ClearLineOfSight();
                break;
            case enumSensitivity.LOW:
                targetInsight = InMyFieldOfVision() || ClearLineOfSight();
                break;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        UpdateSight();
        //update last Known sighting
        if (targetInsight)
            lastKnowsSighting = target.position;
    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;
        targetInsight = false;
    }
        // Start is called before the first frame update
        void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
