using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Controller : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float jumpPower;

    private float _gravityForce;
    private Vector3 _moveVector;

    private CharacterController _controller;
    private Animator _animator;

    private void Start(){
        _controller = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }

    private void Update(){
        CharacterMove();
        PlayerGravity();
    }

    private void CharacterMove(){
        if(_controller.isGrounded){
            _animator.ResetTrigger("Jump");
            _moveVector = Vector3.zero;
            _moveVector.x = Input.GetAxis("Horizontal")* speed;
            _moveVector.z = Input.GetAxis("Vertical")* speed;


            if(Vector3.Angle(Vector3.forward,_moveVector)>1f || Vector3.Angle(Vector3.forward,_moveVector) == 0){
                Vector3 direct = Vector3.RotateTowards(transform.forward, _moveVector, speed, 0.0f);
                transform.rotation = Quaternion.LookRotation(direct);
            }
        }
            if(_moveVector.x!=0 && _controller.isGrounded || _moveVector.z!=0 && _controller.isGrounded )_animator.SetBool("Move", true);
            else _animator.SetBool("Move", false);
       
        _moveVector.y = _gravityForce;

        _controller.Move(_moveVector * Time.deltaTime);
        
    }

    private void PlayerGravity(){
        if(!_controller.isGrounded) _gravityForce -=60f * Time.deltaTime;
        else _gravityForce = -1f;

        if(Input.GetKeyDown(KeyCode.Space) && _controller.isGrounded){
             _gravityForce = jumpPower;
            _animator.SetTrigger("Jump");
        }
    }
}
