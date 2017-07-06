using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public int moveSpeed;
	public int speedAdder;
	private int moveSpeedStore;

	public float speedIncreaseMilestone;
	private float speedIncreaseMilestoneStore;
	private float speedMilestoneCount;
	private float speedMilestoneCountStore;

	public int jumpForce;

	public float jumpTime;
	private float jumpTimeCounter;

	private bool grounded;
	public LayerMask whatIsGround;
	public Transform groundCheck;
	public float groundCheckRadius;


	private Rigidbody2D rb;

	private Animator myAnimator;

	public GameManager theGameManager;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();

		myAnimator = GetComponent<Animator> ();

		jumpTimeCounter = jumpTime;

		moveSpeedStore = moveSpeed;
		speedMilestoneCount = speedIncreaseMilestone;

		speedMilestoneCountStore = speedMilestoneCount;
		speedIncreaseMilestoneStore = speedIncreaseMilestone;
	}
	
	// Update is called once per frame
	void Update () {

		grounded = Physics2D.OverlapCircle (groundCheck.position, groundCheckRadius, whatIsGround);


		if (transform.position.x > speedMilestoneCount) {
			speedMilestoneCount += speedIncreaseMilestone;

			moveSpeed = moveSpeed + speedAdder;
		}

		rb.velocity = new Vector2 (moveSpeed, rb.velocity.y);

		if ((Input.GetKeyDown(KeyCode.Space))&& grounded) {
			rb.velocity = new Vector2 (rb.velocity.x, jumpForce);
		}

		if(Input.GetKey (KeyCode.Space)) {
			if (jumpTimeCounter > 0) {
				rb.velocity = new Vector2 (rb.velocity.x, jumpForce);
				jumpTimeCounter -= Time.deltaTime;
			} else if (jumpTimeCounter < 0) {
				jumpTimeCounter = 0;
			}
				
		}

		if (Input.GetKeyUp (KeyCode.Space)) {
			jumpTimeCounter = 0;
		}

		if (grounded) {
			jumpTimeCounter = jumpTime;
		}

		myAnimator.SetFloat ("Speed", rb.velocity.x);
		myAnimator.SetBool ("Grounded", grounded);
	}

	void OnCollisionEnter2D (Collision2D other) {
		if (other.gameObject.tag == "Finish") {
			theGameManager.RestartGame ();
			moveSpeed = moveSpeedStore;
			speedMilestoneCount = speedMilestoneCountStore;
			speedIncreaseMilestone = speedIncreaseMilestoneStore;
		}
	}
}
