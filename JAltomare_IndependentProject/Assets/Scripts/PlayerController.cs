using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity;

public class PlayerController : MonoBehaviour
{
    public bool CanMove { get; private set; } = true;
    private bool IsSprinting => canSprint && Input.GetKey(sprintKey);
    public bool ShouldJump => Input.GetKey(jumpkey) && characterController.isGrounded;



    // Functional Options 
    [SerializeField] private bool canSprint = true;
    [SerializeField] private bool canJump = true;
    [SerializeField] private bool canInteract = true;

    // Controls
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode interactKey = KeyCode.E;
    [SerializeField] private KeyCode jumpkey = KeyCode.Space;

    // Movement Parameters
    [SerializeField] private float walkSpeed = 3.0f;
    [SerializeField] private float sprintSpeed = 6.0f;


    //Look Parameters
    [SerializeField, Range(1, 10)] private float lookSpeedX = 2.0f;
    [SerializeField, Range(1, 10)] private float lookSpeedY = 2.0f;
    [SerializeField, Range(1, 180)] private float upperLookLimit = 80.0f;
    [SerializeField, Range(1, 180)] private float lowerLookLimit = 80.0f;

    // Health Parameters
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float timeBeforeRegenStarts = 4;
    [SerializeField] private float healthValueIncrement = 1;
    [SerializeField] private float healthTimeIncrement = 0.05f;
    private float currentHealth;
    private Coroutine regeneratingHealth;
    public static Action<float> OnTakeDamage;
    public static Action<float> OnDamage;
    public static Action<float> OnHeal;

    // Jump Parameters
    [SerializeField] private float jumpForce = 8.0f;
    [SerializeField] private float gravity = 30.0f;

    private Camera playerCamera;
    public CharacterController characterController;
    public Animator playerAnim;
    private AudioSource asPlayer;
    public GameManager gameManager;
    public Transform shootPoint;

    //Audio Clips
    public AudioClip birdPickup;

    private Vector3 moveDirection;
    private Vector2 currentInput;
    private float rotationX = 0;

    // Shooting
    public GameObject fireProjectile;
    public GameObject iceProjectile;
    public float projectileSpeed = 20f;
    private bool isShootingFire;
    private bool isShootingIce;

    private void OnEnable()
    {
        OnTakeDamage += ApplyDamage;
    }
    private void OnDisable()
    {
        OnTakeDamage -= ApplyDamage;
    }

    void Awake()
    {
        playerCamera = GetComponentInChildren<Camera>();
        characterController = GetComponent<CharacterController>();
        playerAnim = GetComponent<Animator>();
        asPlayer = GetComponent<AudioSource>();
        currentHealth = maxHealth;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (CanMove && !gameManager.gameOver)
        {
            HandleMovementInput();
            HandleMouseLook();

            if (canJump)
            {
                HandleJump();
            }
            ApplyFinalMovements();
        }
        if (GameManager.Instance.canShootFire == true && characterController.isGrounded)
        {
            isShootingFire |= Input.GetKeyDown(KeyCode.Alpha1);
        }
        if (GameManager.Instance.canShootIce == true && characterController.isGrounded)
        {
            isShootingIce |= Input.GetKeyDown(KeyCode.Alpha2);
        }

        if (characterController.isGrounded == true) 
        {
            playerAnim.SetBool("isGrounded", true);
            playerAnim.SetBool("isFalling", false);
            playerAnim.SetBool("isJumping", false);
            Debug.Log("Character is Grounded");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BabyBird"))
        {
            asPlayer.PlayOneShot(birdPickup, 2.0f);
        }

    }
    private void FixedUpdate()
    {

            if (isShootingFire)
            {
                GameObject newProjectile = Instantiate(fireProjectile, shootPoint.position, shootPoint.rotation);
                Rigidbody ProjectileRB = newProjectile.GetComponent<Rigidbody>();
                ProjectileRB.AddForce(this.transform.forward * projectileSpeed * Time.deltaTime, ForceMode.Impulse);
                playerAnim.SetTrigger("CastSpell");
                isShootingFire = false;
            }

            if (isShootingIce)
            {
                GameObject newProjectile = Instantiate(iceProjectile, shootPoint.position, shootPoint.rotation);
                Rigidbody ProjectileRB = newProjectile.GetComponent<Rigidbody>();
                ProjectileRB.AddForce(this.transform.forward * projectileSpeed * Time.deltaTime, ForceMode.Impulse);
                playerAnim.SetTrigger("CastSpell");
                isShootingIce = false;
            }
 
    }

    private void HandleMovementInput()
    {
        currentInput = new Vector2((IsSprinting ? sprintSpeed : walkSpeed) * Input.GetAxisRaw("Vertical"), (IsSprinting ? sprintSpeed : walkSpeed) * Input.GetAxisRaw("Horizontal"));
        float moveDirectionY = moveDirection.y;
        moveDirection = (transform.TransformDirection(Vector3.forward) * currentInput.x + transform.TransformDirection(Vector3.right) * currentInput.y);
        moveDirection.y = moveDirectionY;
    }
    private void HandleMouseLook()
    {
        rotationX -= Input.GetAxis("Mouse Y") * lookSpeedY;
        rotationX = Mathf.Clamp(rotationX, -upperLookLimit, lowerLookLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeedX, 0);
    }
    private void HandleJump()
    {
        if (ShouldJump)
        {
            moveDirection.y = jumpForce;
            playerAnim.SetBool("isJumping", true);
        }

    }
    private void ApplyDamage(float dmg)
    {
        currentHealth -= dmg;
        OnDamage?.Invoke(currentHealth);

        if (currentHealth <= 0)
        {
            KillPlayer();
        }
        else if (regeneratingHealth != null)
        {
            StopCoroutine(regeneratingHealth);
        }
        regeneratingHealth = StartCoroutine(RegenerateHealth());
    }
    private void KillPlayer()
    {
        currentHealth = 0;

        if (regeneratingHealth != null)
        {
            StopCoroutine(regeneratingHealth);
            print("DEAD");
        }
    }
    private void ApplyFinalMovements()
    {
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
            playerAnim.SetBool("isFalling", true);
        }

        characterController.Move(moveDirection * Time.deltaTime);

        if (characterController.velocity.y < -1 && characterController.isGrounded)
        {
            moveDirection.y = 0; 
            //playerAnim.SetBool("isFalling", false);
            //playerAnim.SetBool("isJumping", false);
        }

    }
    private IEnumerator RegenerateHealth()
    {
        yield return new WaitForSeconds(timeBeforeRegenStarts);
        WaitForSeconds timeToWait = new WaitForSeconds(healthTimeIncrement);

        while (currentHealth < maxHealth)
        {
            currentHealth += healthValueIncrement;

            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
            OnHeal?.Invoke(currentHealth);
            yield return timeToWait;
        }
        regeneratingHealth = null;
    }
}

