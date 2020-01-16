using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWork : MonoBehaviour
{
    public Transform playerTransform;

    Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        //방법1
        //offset = new Vector3(0, -10.0f, 3.6f);

        //방법2
        offset = playerTransform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerTransform.position - offset;
    }
}
