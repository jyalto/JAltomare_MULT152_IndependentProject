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
    [SerializeField] private float healthValueIncrement = 1.0f;
    [SerializeField] private float healthTimeIncrement = 0.05f;
    [SerializeField] private float currentHealth;
    private Coroutine regeneratingHealth;
    public static Action<float> OnTakeDamage;
    public static Action<float> OnDamage;
    public static Action<float> OnHeal;

    public Transform spawnPoint;


    // Jump Parameters
    [SerializeField] private float jumpForce = 8.0f;
    [SerializeField] private float gravity = 30.0f;

    private Camera playerCamera;
    public CharacterController characterController;
    private PlayerController playerController;
    private Animator playerAnim;
    private AudioSource asPlayer;
    public GameManager gameManager;
    public Transform shootPoint;

    //Audio Clips
    public AudioClip birdPickup;
    public AudioClip healingIncoming;
    public AudioClip fireShoot;
    public AudioClip iceShoot;

    public ParticleSystem healSystem;
    public ParticleSystem fireImpact;
    public ParticleSystem iceImpact;

    private Vector3 moveDirection;
    private Vector2 currentInput;
    private float rotationX = 0;

    // Shooting
    public GameObject fireProjectile;
    public GameObject iceProjectile;
    public float projectileSpeed = 1000.0f;
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
        playerController = GetComponent<PlayerController>();
        playerAnim = GetComponent<Animator>();
        asPlayer = GetComponent<AudioSource>();
        currentHealth = maxHealth;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Physics.autoSyncTransforms = true;
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
        if (other.CompareTag("HealingCircle"))
        {
            regeneratingHealth = StartCoroutine(RegenHealthOverTime());
            asPlayer.PlayOneShot(healingIncoming, .25f);
            healSystem.Play();
        }

    }
    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("HealingCircle") && regeneratingHealth != null)
        {
            StopCoroutine(regeneratingHealth);
        }
        if (other.CompareTag("HealingCircle"))
        {
            healSystem.Stop();
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
            asPlayer.PlayOneShot(fireShoot, .25f);
            isShootingFire = false;
        }

        if (isShootingIce)
        {
            GameObject newProjectile = Instantiate(iceProjectile, shootPoint.position, shootPoint.rotation);
            Rigidbody ProjectileRB = newProjectile.GetComponent<Rigidbody>();
            ProjectileRB.AddForce(this.transform.forward * projectileSpeed * Time.deltaTime, ForceMode.Impulse);
            playerAnim.SetTrigger("CastSpell");
            asPlayer.PlayOneShot(iceShoot, .25f);
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
        fireImpact.Play();
        iceImpact.Play();

        if (currentHealth <= 0)
        {
            KillPlayer();
        }
    }
    private void KillPlayer()
    {
        currentHealth = 0;
        playerAnim.SetBool("isDead", true);
        playerController.enabled = false;
        GameManager.Instance.playerDead = true;

        Invoke("Respawn", 6);
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

    IEnumerator RegenHealthOverTime()
    {
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
    }
    public void Respawn()
    { 
        currentHealth = maxHealth;
        OnHeal?.Invoke(currentHealth);
        transform.position = spawnPoint.position;
        transform.rotation = spawnPoint.rotation;
        playerController.enabled = true;
        playerAnim.SetBool("isDead", false);
        playerAnim.SetTrigger("isRevived");
        GameManager.Instance.playerDead = false;
        GameManager.Instance.deleteElementals = true;
        GameManager.Instance.fireCore = 0;
        GameManager.Instance.iceCore = 0;
    }
}

