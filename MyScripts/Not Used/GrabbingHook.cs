using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbingHook : MonoBehaviour
{

    private DistanceJoint2D joint;

    private Vector3 target;

    private RaycastHit2D rayCast;

    [SerializeField]
    private LineRenderer line;

    [SerializeField]
    private float distance = 10f;

    [SerializeField]
    private LayerMask mask;

    void Start()
    {
        joint = GetComponent<DistanceJoint2D>();

        joint.enabled = false;
        line.enabled = false;
    }

  
    void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Began)
            {
                target = Camera.main.ScreenToWorldPoint(touch.position);
                target.z = 0;

                rayCast = Physics2D.Raycast(transform.position, target - transform.position, distance, mask);

                if (rayCast.collider != null)
                {
                    joint.enabled = true;

                    joint.connectedBody = rayCast.collider.gameObject.GetComponent<Rigidbody2D>();

                    joint.connectedAnchor = rayCast.point - new Vector2(rayCast.collider.transform.position.x , rayCast.collider.transform.position.y);

                    joint.distance = Vector2.Distance(transform.position, rayCast.point);

                    line.enabled = true;

                    line.SetPosition(0 , transform.position);
                    line.SetPosition(1, rayCast.point); 


                }
            }

            if (touch.phase == TouchPhase.Stationary)
            {
                line.SetPosition(0, transform.position);
            }

            if (touch.phase == TouchPhase.Ended)
            {
                joint.enabled = false;
                line.enabled = false;
            }
        }
    }
}
