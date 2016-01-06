using UnityEngine;
using System.Collections.Generic;

public class PowersController : MonoBehaviour {

    private Rigidbody2D rb;
    public GameObject player;

    public float pushForce;
    public float pullForce;

    Vector3 MousePos;

    private List<Collider2D> hulls;

    private RaycastHit2D dbgHIT;
    private Vector3 HITposition;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();

        hulls = new List<Collider2D>();

        // HACK: properly note your hulls ;(
        hulls.Add(GetComponent<BoxCollider2D>());
        hulls.Add(GetComponent<CircleCollider2D>());
	}

    void FixedUpdate()
    {
        //if (Input.GetMouseButtonDown(0))
        {

            MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, (MousePos - transform.position).normalized, Mathf.Infinity);

            bool hitSuccess = false;
            Vector3 hitDirection = Vector3.zero;

            RaycastHit2D closestHit = new RaycastHit2D();
            float shortestDistanceRecorded = Mathf.Infinity;

            foreach (var hit in hits)
            {
                if (hulls[0] == hit.collider ||
                    hulls[1] == hit.collider)
                {
                    // we only hit ourselves ;(
                    continue;
                }

                if (hit.distance < shortestDistanceRecorded)
                {
                    shortestDistanceRecorded = hit.distance;
                    closestHit = hit;
                }

                //Debug.Log("WE SHOT " + hit.collider.name);
                hitSuccess = true;
                //Debug.Log(hit.distance);

                hitDirection = (MousePos - transform.position).normalized * hit.distance;
            }

            

            Vector2 rawr = (MousePos - transform.position).normalized;
            //if hit works and is metal player can interact
            if (hitSuccess)
            {
                dbgHIT = closestHit;
                HITposition = dbgHIT.point;

                
                if (closestHit.collider.tag == "Metal")
                {
                    

                    float pull = Input.GetAxis("Fire1");         // should be positive
                    float push = Input.GetAxis("Fire2") * -1;    // should be negative

                    Vector2 magnetismForce = (closestHit.point - (Vector2)transform.position).normalized;   // direction
                    magnetismForce *= ((push * pushForce) + (pull * pullForce));

                    Debug.Log(magnetismForce);

                    rb.AddForce(magnetismForce, ForceMode2D.Impulse);
                }
            }
                    if (Input.GetAxisRaw("Fire1") != 0.0f || Input.GetAxisRaw("Fire2") != 0.0f)
                        rb.gravityScale = 0.0f;
                    else
                        rb.gravityScale = 1.0f;
        }
    }

	// Update is called once per frame
	void Update () {
	
	}

    void OnGizmosDraw()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(dbgHIT.point, 0.5f);
    }
}
