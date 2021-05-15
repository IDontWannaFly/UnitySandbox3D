using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraScript : MonoBehaviour
{

    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
            transform.SetPositionAndRotation(new Vector3(player.position.x, transform.position.y, transform.position.z), transform.rotation);
    }
}
