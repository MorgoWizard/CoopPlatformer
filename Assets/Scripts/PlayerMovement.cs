using Unity.Netcode;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
        [SerializeField] private float speed = 5f;
        [SerializeField] private float jumpForce = 10f;
        
        private SpriteRenderer _renderer;

        private BoxCollider2D _boxCollider2D;
        [SerializeField] private LayerMask platformLayerMask;
        
        private Rigidbody2D _rb2d;
        private Vector2 _moveDirection = Vector2.zero;

        [SerializeField] private new Camera camera;

        public override void OnNetworkSpawn()
        {
            if (!IsLocalPlayer && camera)
            {
                camera.gameObject.SetActive(false);
            }
            _rb2d = GetComponent<Rigidbody2D>();
            _boxCollider2D = GetComponent<BoxCollider2D>();
            _renderer = GetComponent<SpriteRenderer>();
        }
        
        private void FixedUpdate()
        {
            if (!IsOwner) return;
            _moveDirection = new Vector2(Input.GetAxis("Horizontal"), 0);

            MovementLogic();
            RightFacing();
        }

        private void Update()
        {
            if (!IsOwner) return;
            if(Input.GetKeyDown("space") || Input.GetKeyDown("w")) DoJump();
        }

        private void DoJump()
        {
            if (!IsGrounded()) return;
            Vector2 jump = new Vector2(0, jumpForce);
            _rb2d.AddForce(jump, ForceMode2D.Impulse);
        }

        private void MovementLogic()
        {
            if (!IsGrounded()) return;
            Vector2 movement = new Vector2(_moveDirection.x * speed, _rb2d.velocity.y);
            _rb2d.velocity = movement;
        }
        private void RightFacing()
        {
            if (!IsGrounded()) return;
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                _renderer.flipX = true;
            }
            else if (Input.GetAxisRaw("Horizontal") < 0)
            {
                _renderer.flipX = false;
            }
        }

        private bool IsGrounded()
        {
            const float extraHeightText = 0.03f;
            var bounds = _boxCollider2D.bounds;
            RaycastHit2D raycastHit2D = Physics2D.Raycast(bounds.center, Vector2.down,
                bounds.extents.y + extraHeightText, platformLayerMask);
            var rayColor = raycastHit2D.collider != null ? Color.green : Color.red;
            Debug.DrawRay(bounds.center, Vector2.down * (bounds.extents.y + extraHeightText), rayColor);
            return raycastHit2D.collider != null;
        }
}
