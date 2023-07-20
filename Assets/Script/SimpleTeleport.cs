using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTeleport : MonoBehaviour
{
    // Player gameobject required to be moved
    public Transform player;
    // LineRenderer gameobject (to be added on custom hand left) to draw line
    public LineRenderer line;
    // Ground layer
    // Do not forget to set correct layer on floor
    public LayerMask layerTeleport;
    private Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        // Init target line to zero
        line.positionCount = 2;
        Vector3[] startPos = { Vector3.zero, Vector3.zero };
        line.SetPositions(startPos);
        line.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Detect X button touch (finger hover, not press)
        if (OVRInput.Get(OVRInput.Touch.Three))
        {
            // Enable line renderer component
            line.enabled = true;
            RaycastHit hit;
            // Origin of the ray/line is the hand
            line.SetPosition(0, transform.position);
            // Raycast draws virtual lines, or rays, and detects when it intersects
            // Ray direction is when hand points
            if (Physics.Raycast(transform.position, transform.forward, out hit, 100f, layerTeleport))
            {
                // Ray intersects with floor, remember the intersection
                pos = hit.point;
                line.SetPosition(1, hit.point);
            }
            else
            {
                // Ray does not intersect with floor
                pos = Vector3.zero;
                line.SetPosition(1, transform.position);
            }
        }
        else
        {
            // X button not touched yet
            // Enable line renderer component
            line.enabled = false;
        }

        
        // Detect X button pressed and an intesection position is saved
        if (OVRInput.GetDown(OVRInput.Button.Three) && pos != Vector3.zero)
        {
            // Move, or rather teleport player to the target position
            player.position = new Vector3(pos.x, player.position.y, pos.z);
        }
        
    }
}
