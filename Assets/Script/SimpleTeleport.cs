using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTeleport : MonoBehaviour
{
    public Transform player;
    public LineRenderer line;
    public LayerMask layerTeleport;
    private Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // TELEPORT TOUCH
        if (OVRInput.Get(OVRInput.Touch.Three))
        {
            line.enabled = true;
            //Debug.Log("Touch");
            RaycastHit hit;
            line.SetPosition(0, transform.position);
            if (Physics.Raycast(transform.position, transform.forward, out hit, 100f, layerTeleport))
            {
                pos = hit.point;
                line.SetPosition(1, hit.point);
            }
            else
            {
                pos = Vector3.zero;
                line.SetPosition(1, transform.position);
            }
        } else {
            line.enabled = false;
        }

        //TELEPORT 
        if (OVRInput.GetDown(OVRInput.Button.Three) && pos != Vector3.zero)
        {
            //player.GetComponent<CharacterController>().enabled = false;
            player.position = new Vector3(pos.x, player.position.y, pos.z);
            //player.GetComponent<CharacterController>().enabled = true;
        }
    }
}
