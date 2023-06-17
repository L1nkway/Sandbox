using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    [SerializeField] private TargetJoint2D targetJoint2D;

    [SerializeField] private HingeJoint2D hingeJoint2D;

    [SerializeField] private Rigidbody2D rb;

    private Vector2 difference = Vector2.zero;

    GameObject connectionPoint;

    private void OnMouseDown()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        difference = mousePos - rb.position;

        connectionPoint = new GameObject("ConnectionPoint");
        connectionPoint.transform.position = mousePos;
        connectionPoint.AddComponent<Rigidbody2D>();
        connectionPoint.AddComponent<HingeJoint2D>();
        connectionPoint.GetComponent<HingeJoint2D>().connectedBody = rb;
        Debug.Log(connectionPoint.transform.position);
    }

    private void OnMouseDrag()
    {
        targetJoint2D.enabled = true;
        hingeJoint2D.enabled = true;

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //targetJoint.target = targetJoint.transform.InverseTransformPoint(mousePos);


        if (targetJoint2D)
        {
            targetJoint2D.target = mousePos - difference;
            hingeJoint2D.connectedBody = connectionPoint.GetComponent<Rigidbody2D>();

        }
    }

    private void OnMouseUp()
    {
        targetJoint2D.enabled = false;
        hingeJoint2D.enabled = false;
        hingeJoint2D.connectedBody = null;
        Destroy(connectionPoint);
    }
}
