using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F18Pilot : MonoBehaviour
{

    public float speed = 90.0f;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("plane pilot script added to: " + gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveCam = transform.position - transform.forward * 16.0f + Vector3.up * 5.0f;
        float bias = 0.150f;
        Camera.main.transform.position = Camera.main.transform.position * bias + moveCam * (1.0f-bias);
        Camera.main.transform.LookAt(transform.position + transform.forward * 30.0f);

        transform.position += transform.forward * Time.deltaTime * speed;

        speed -= transform.forward.y * Time.deltaTime * 40.0f;

        if(speed < 35.0f)
        {
            speed = 35.0f;
        }

        transform.Rotate( Input.GetAxis("Vertical"), 0.0f, -Input.GetAxis("Horizontal") );

        float terrainToPlanePos = Terrain.activeTerrain.SampleHeight(transform.position);

        if (terrainToPlanePos > transform.position.y) {
            transform.position = new Vector3(transform.position.x, terrainToPlanePos, transform.position.z);
        }

     
    }
}
