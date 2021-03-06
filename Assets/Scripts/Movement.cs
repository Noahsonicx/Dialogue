using UnityEngine;
namespace Debugging.Player
{
    [AddComponentMenu("RPG/Player/Movement")]
    [RequireComponent(typeof (CharacterController))]
    public class Movement : MonoBehaviour
    {
        [Header("Speed Vars")]
        public float moveSpeed;
        public float walkSpeed, runSpeed, crouchSpeed, jumpSpeed;
        private float _gravity = 20.0f;
        private Vector3 _moveDir;
        private CharacterController _charC;
        private Animator characterAnimator;
        private void Start()
        {
            _charC = GetComponent<CharacterController>();
            characterAnimator = GetComponentInChildren<Animator>();
        }
        private void Update()
        {
            Move();
        }
        private void Move()
        {
            
           
            if (_charC.isGrounded)
            {
                Vector2 controlVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

                if (controlVector.magnitude >= .05f)
                {
                    characterAnimator.SetBool("moving", true);
                }
                else
                {
                    characterAnimator.SetBool("moving", false);
                }
                if (Input.GetButton("Sprint"))
                {
                    moveSpeed = runSpeed;
                    characterAnimator.SetFloat("speed", 5f);
                }
                else if (Input.GetButton("Crouch"))
                {
                    moveSpeed = crouchSpeed;
                }
                else
                {
                    moveSpeed = walkSpeed;
                    characterAnimator.SetFloat("speed", 1);
                }
                _moveDir = transform.TransformDirection(new Vector3(controlVector.x, 0, controlVector.y) * moveSpeed); 
                if (Input.GetButton("Jump"))
                {
                    _moveDir.y = jumpSpeed;
                    characterAnimator.SetBool("moving", true);
                }
            }
            _moveDir.y -= _gravity * Time.deltaTime;
            _charC.Move(_moveDir * Time.deltaTime);
        }
        
    }
}
