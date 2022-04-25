using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] private bool inDialogue;
    [SerializeField] private float speed;
    private Rigidbody2D playerRb;
    private Vector2 moveInput;
    private Animator playerAnimator;

    private void Start() {
        playerRb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    private void Update() {

        if (inDialogue) {
            return;
        }

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveInput = new Vector2(moveX, moveY).normalized;

        playerAnimator.SetFloat("Horizontal", moveX);
        playerAnimator.SetFloat("Vertical", moveY);
        playerAnimator.SetFloat("Speed", moveInput.sqrMagnitude);
    }

    private void FixedUpdate() {
        playerRb.MovePosition(playerRb.position + (speed * Time.fixedDeltaTime * moveInput));
    }

    public bool GetInDialogue() {
        return inDialogue;
    }

    public void SetInDialogue(bool d) {
        inDialogue = d;
    }
}
