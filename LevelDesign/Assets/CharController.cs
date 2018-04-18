using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour {

    public Animator animator;
    float vertical;
    float horizontal;
    bool attack;

    public float Speed = 5f;
    public float JumpHeight = 2f;
    public float GroundDistance = 0.2f;
    public LayerMask Ground;

    private Rigidbody _body;
    private Vector3 _inputs = Vector3.zero;
    private bool _isGrounded = true;
    private Transform _groundChecker;

    void Start() {
        animator = GetComponent<Animator>();
        _body = GetComponent<Rigidbody>();
        _groundChecker = transform.GetChild(0);
    }

    void Update() {

        animator.SetBool("walk", false);
        _isGrounded = Physics.CheckSphere(_groundChecker.position, GroundDistance, Ground, QueryTriggerInteraction.Ignore);

        _inputs = Vector3.zero;
        _inputs.x = Input.GetAxis("Horizontal");
        attack = Input.GetKeyDown("l");
        animator.SetBool("Attack", attack);

        if (_inputs != Vector3.zero) {
            transform.forward = _inputs;
            animator.SetBool("walk", true);
        }

        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _body.AddForce(Vector3.up * Mathf.Sqrt(JumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
        }
    }


    void FixedUpdate() {
        _body.MovePosition(_body.position + _inputs * Speed * Time.fixedDeltaTime);
    }
}



//Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
//_controller.Move(move * Time.deltaTime * _controller.velocity.magnitude);

//if (move != Vector3.zero)
//transform.forward = move;

//var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
//var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

//transform.Rotate(0, x, 0);
//transform.Translate(0, 0, z);