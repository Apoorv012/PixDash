using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
	[SerializeField] private float moveSpeed = 5f;
	[SerializeField] private float jumpPower = 10f;
	[SerializeField] private Rigidbody2D rb;
	[SerializeField] private Animator animator;

	float horizontalInput;
	bool isFacingRight = true;
	bool isGrounded = false;

	// Update is called once per frame
	void Update()
	{
		horizontalInput = Input.GetAxis("Horizontal");

		FlipSprite();

		if (Input.GetButtonDown("Jump") && isGrounded)
		{
			rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
			isGrounded = false;
			animator.SetBool("isJumping", !isGrounded);
		}
	}

	private void FixedUpdate()
	{
		rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);
		animator.SetFloat("xVelocity", Mathf.Abs(rb.linearVelocity.x));
		animator.SetFloat("yVelocity", rb.linearVelocity.y);
	}

	void FlipSprite()
	{
		if (isFacingRight && horizontalInput < 0f || !isFacingRight && horizontalInput > 0f)
		{
			isFacingRight = !isFacingRight;
			Vector3 ls = transform.localScale;
			ls.x *= -1f;
			transform.localScale = ls;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		isGrounded = true;
		animator.SetBool("isJumping", !isGrounded);
	}
}
