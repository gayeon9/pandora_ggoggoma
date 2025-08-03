using UnityEngine;

namespace SuperEasyRPG
{
    public class Player : MonoBehaviour
    {
        public bool canMove = true;
        public float speed = 1.0f;

        [Space]
        public float interactionDistance = 0.7f;
        public int interactionLayer = 3;
        [Space]
        public int level = 0;

        new Rigidbody2D rigidbody;
        Animator animator;

        Vector2 moveVec;
        Vector3 dirVec;

        InteractiveObject scannedObject;
        AudioSource walkSound;

        void Start()
        {
        }

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            walkSound = GetComponent<AudioSource>();
        }

        void Update()
        {
            HandleInput();
        }

        private void HandleInput()
        {
            if (canMove)
            {
                // player movement
                int horizontal = 0;
                int vertical = 0;

                float horizontalAxis = Input.GetAxisRaw("Horizontal");
                float verticalAxis = Input.GetAxisRaw("Vertical");

                if (horizontalAxis > 0)
                {
                    horizontal = 1;
                    dirVec = Vector3.right;
                }
                else if (horizontalAxis < 0)
                {
                    horizontal = -1;
                    dirVec = Vector3.left;
                }
                else if (verticalAxis > 0)
                {
                    vertical = 1;
                    dirVec = Vector3.up;
                }
                else if (verticalAxis < 0)
                {
                    vertical = -1;
                    dirVec = Vector3.down;
                }

                moveVec = new Vector2(horizontal, vertical) * speed;

                // interaction
                if (scannedObject != null && Input.GetKeyDown(KeyCode.Space))
                {
                    scannedObject.Interact();
                }
            }
            else
            {
                moveVec = new Vector2(0, 0);
            }

            UpdateAnimatorState(moveVec);
            HandleWalkingSound(moveVec);
        }

        private void UpdateAnimatorState(Vector2 moveVec)
        {
            int newHorizontal = (int)moveVec.x;
            int newVertical = (int)moveVec.y;
            bool isChanged = animator.GetInteger("horizontal") != newHorizontal || animator.GetInteger("vertical") != newVertical;
            animator.SetBool("isChanged", isChanged);
            animator.SetInteger("horizontal", newHorizontal);
            animator.SetInteger("vertical", newVertical);
        }

        private void HandleWalkingSound(Vector2 moveVec)
        {
            if (moveVec.sqrMagnitude > 0)
            {
                walkSound.enabled = true;
            } else
            {
                walkSound.enabled = false;
            }
        }

        private void FixedUpdate()
        {
            rigidbody.linearVelocity = moveVec;

            ScanInteractiveObject();
        }

        private void ScanInteractiveObject()
        {
            Debug.DrawRay(rigidbody.position, dirVec * interactionDistance, Color.red);
            RaycastHit2D rayHit = Physics2D.Raycast(rigidbody.position, dirVec, interactionDistance, (1 << interactionLayer));

            if (rayHit.collider != null && rayHit.collider.gameObject.GetComponent<InteractiveObject>() != null)
            {
                scannedObject = rayHit.collider.gameObject.GetComponent<InteractiveObject>();
            }
            else
            {
                scannedObject = null;
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, Vector3.down * interactionDistance);
        }
    }
}