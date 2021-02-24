using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public Transform[] transformPositions;
    [SerializeField] GameObject camReference;
    [SerializeField]float currentDistance;
    [SerializeField]Transform destiny;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            destiny = transformPositions[0];
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            destiny = transformPositions[1];
        }
        if (destiny != null)
        {
            currentDistance += Time.deltaTime;
            camReference.transform.position = Vector3.MoveTowards(camReference.transform.position, destiny.position, currentDistance);
            if (currentDistance >= 1)
            {
                currentDistance = 0;
                destiny = null;
            }
        }

    }
}
