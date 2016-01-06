using UnityEngine;
using System.Collections;

public class Player1Controller : MonoBehaviour {

    public float speed;
    public float jumpStrength;

    private bool grounded;

    private Rigidbody2D rb;

    private float GroundCheckRayLen = 1.2f;

	// Use this for initialization
	void Awake ()
    {
        rb = GetComponent<Rigidbody2D>();
	}
	
    bool GroundCheck()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, -Vector3.up, GroundCheckRayLen);

        foreach (var hit in hits)
        {
            // HACK: check if the collider belongs to the same gameobject as this
            if (hit.collider.gameObject == gameObject)
            {
                continue;
            }

            // we didn't hit ourselves
            return true;
        }

        return false;
    }

	// FixedUpdate is called once per physics update
    void FixedUpdate()
    {
        grounded = GroundCheck();

            rb.velocity = new Vector2(0f, rb.velocity.y);

            float moveHor = Input.GetAxis("Horizontal");
            float jump = Input.GetButtonDown("Jump") ? 1 : 0;


        if (!grounded)
        {
            jump = 0;
        }

        Vector2 movement = new Vector2(moveHor, jump * jumpStrength);

            rb.AddForce(movement * speed, ForceMode2D.Impulse);
        
    }
}
