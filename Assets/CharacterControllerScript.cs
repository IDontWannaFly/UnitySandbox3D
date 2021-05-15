using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerScript : MonoBehaviour
{

    public float speed = 5f;
    public GroundDetector groundDetector;
    public float jumpForce = 5f;

    private Animator animator;
    private Rigidbody body;
    private float m_RunCycleLegOffset = 0.2f;
    private float comboCounter = 0f;
    private bool isReload = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frames
    void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var jump = Input.GetButton("Jump");
        var hit = Input.GetButtonDown("Fire1");
        if(hit){
            Hit();
        } else {
            var direction = new Vector3(horizontal * speed, body.velocity.y, 0f);
            if(direction.magnitude >= 0.1f){
                var targetAngle = Mathf.Atan2(direction.x, 0f) * Mathf.Rad2Deg;
            
                transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
                body.velocity = direction;
            }
            if(jump && groundDetector.IsGrounded()){
                body.velocity = new Vector3(body.velocity.x, jumpForce, body.velocity.z);
            }
        }
        if(comboCounter > 0f) {
            comboCounter -= Time.deltaTime;
        } else if(comboCounter <= 0f) {
            comboCounter = 0;
            isReload = false;
        }
        UpdateAnimator(horizontal);
        //animator.SetFloat("Turn", Mathf.Atan2(direction.x, direction.y), 0.1f, Time.deltaTime);
    }

    private void UpdateAnimator(float horizontal){
        animator.SetFloat("Combo", comboCounter);
        animator.SetFloat("Jump", body.velocity.y);
        animator.SetFloat("Forward", Mathf.Abs(horizontal), 0.1f, Time.deltaTime);
        animator.SetBool("OnGround", groundDetector.IsGrounded());
        float runCycle =
				Mathf.Repeat(
					animator.GetCurrentAnimatorStateInfo(0).normalizedTime + m_RunCycleLegOffset, 1);
        float jumpLeg = (runCycle < 0.5f ? 1 : -1) * horizontal;
        if (groundDetector.IsGrounded())
        {
            animator.SetFloat("JumpLeg", jumpLeg);
        }
    }

    private void Hit(){
        if(comboCounter < 4f && !isReload)
            comboCounter += 1.5f;
        else if(comboCounter >= 4f)
            isReload = true;

        Debug.Log($"Hit: {comboCounter}");
    }
}
