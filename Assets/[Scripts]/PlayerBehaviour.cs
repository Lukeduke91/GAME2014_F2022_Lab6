using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [Header("Player Movement")]
    public float HorizontalForce;
    public float HorizontalSpeed;
    public float VerticalForce;
    public float airFactor;
    public Transform GroundPoint;
    public float GroundRadius;
    public LayerMask GetLayerMask;
    public bool isGrounded;

    private Rigidbody2D rigidbody2D;
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var hit = Physics2D.OverlapCircle(GroundPoint.position, GroundRadius, GetLayerMask);
        isGrounded = hit;

        Move();
        Jump();
    }

    private void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        if (x != 0.0f)
        {


            rigidbody2D.AddForce(Vector2.right * ((x > 0.0) ? 1.0f : -1.0f) * HorizontalForce  * ((isGrounded) ? 1 : airFactor));

            rigidbody2D.velocity = Vector2.ClampMagnitude(rigidbody2D.velocity, HorizontalSpeed);
        }
    }

    private void Jump()
    {
        var y = Input.GetAxis("Jump");

        if((isGrounded) && (y > 0.0f))
        {
            rigidbody2D.AddForce(Vector2.up * VerticalForce, ForceMode2D.Impulse);
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(GroundPoint.position, GroundRadius);
    }
}
