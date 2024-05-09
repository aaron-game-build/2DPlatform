using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Default
{
    public class Player : PhysicsObject
    {
        [SerializeField] private float maxSpeed = 3;

        private Animator _animator;

        // Start is called before the first frame update
        void Start()
        {
            _animator = GetComponentInChildren<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            float move = Input.GetAxis("Horizontal");

            if (move < 0 && _animator.transform.localScale.x > 0)
            {
                _animator.transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (move > 0 && _animator.transform.localScale.x < 0)
            {
                _animator.transform.localScale = new Vector3(1, 1, 1);
            }

            targetVelocity = new Vector2(move * maxSpeed, 0);
            _animator.SetFloat("Move", Mathf.Abs(move));

            // If the player presses "Jump" and we're grounded, set the velocity to jump power value
            if (Input.GetButtonDown("Jump") && grounded)
            {
                velocity.y = 10;

                _animator.SetBool("Jump", true);
            }
            
            if (velocity.y < 0.1 && _animator.GetBool("Jump"))
            {
                _animator.SetBool("Jump", false);
                _animator.SetBool("Fall", true);
            }
        }
    }
}