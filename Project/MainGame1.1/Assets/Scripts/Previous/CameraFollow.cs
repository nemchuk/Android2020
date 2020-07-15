using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    public GameObject followObject;// character
    [SerializeField]
    public Vector2 followOffset;//how much distance the character will be able to move freely before camera starts moving

    private Rigidbody2D rb; 

    private Vector2 threshold; // Screen dimension - followOffset define boundary box 

    [SerializeField]
    public float speed = 3f;

    // Start is called before the first frame update
    void Start()
    {

        rb = followObject.GetComponent<Rigidbody2D>();

        threshold = calculateBoundaries(); // define threshold
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 follow = followObject.transform.position;

        float xDifference = Vector2.Distance(Vector2.right * transform.position.x , Vector2.right * follow.x);
        float yDifference = Vector2.Distance(Vector2.up * transform.position.y, Vector2.up * follow.y);

        Vector3 newPosition = transform.position;
        if(Mathf.Abs(xDifference) >= threshold.x)
        {
            newPosition.x = follow.x;
        }
        if (Mathf.Abs(yDifference) >= threshold.y)
        {
            newPosition.y = follow.y;
        }

        float moveSpeed = rb.velocity.magnitude > speed ? rb.velocity.magnitude : speed;

        transform.position = Vector3.MoveTowards(transform.position , newPosition , moveSpeed * Time.deltaTime); 
    }
    // calculates threshold
    private Vector3 calculateBoundaries()
    {
        Rect aspect = Camera.main.pixelRect; // aspect ratio of our camera

        Vector2 vec = new Vector2(Camera.main.orthographicSize * aspect.width / aspect.height , Camera.main.orthographicSize);//boundaries

        vec.x -= followOffset.x;
        vec.y -= followOffset.y;

        return vec;
    }

    // visualise boundaries in editor
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red; // define color

        Vector2 border = calculateBoundaries(); // calculate boundaries

        Gizmos.DrawWireCube(transform.position, new Vector3(border.x * 2, border.y * 2, 1)); //visualise boundaries in the editor
    }
}
