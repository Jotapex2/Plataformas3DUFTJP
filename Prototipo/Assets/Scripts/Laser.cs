using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public GameObject baseLine;
    public GameObject endPoint;
    public GameObject startPoint;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(startPoint.transform.position, startPoint.transform.forward, out hit, 100))
        {
            endPoint.transform.position = hit.point;

            baseLine.transform.localScale = new Vector3(1, 1, hit.distance);
        }
    }
}
