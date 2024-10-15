using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity;

public class PlayerController : MonoBehaviour
{
    public bool CanMove { get; private set; } = true;
    private bool IsSprinting => canSprint && Input.GetKey(sprintKey);
    private bool ShouldJump => Input.GetKey(jumpKey) && characterController.isGrounded;

    //private GameController gc;

    // Functional Options 
    [SerializeField] private bool canSprint = true;
    [SerializeField] private bool canJump = true;
    [SerializeField] private bool canInteract = true;

    // Controls
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode interactKey = KeyCode.E;

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

    // Interaction
    [SerializeField] private Vector3 interactionRayPoint = default;
    [SerializeField] private float interactionDistance = default;
    [SerializeField] private LayerMask interactionLayer = default;
    private Interactable currentInteractable;


    private Camera playerCamera;
    public CharacterController characterController;

    private Vector3 moveDirection;
    private Vector2 currentInput;

    private float rotationX = 0;

    // Shooting
    public GameObject projectile;
    public float projectileSpeed = 100f;
    private bool isShooting;

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
        //gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        playerCamera = GetComponentInChildren<Camera>();
        characterController = GetComponent<CharacterController>();
        currentHealth = maxHealth;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (CanMove)
        {
            HandleMovementInput();
            HandleMouseLook();

            if (canJump)
            {
                HandleJump();
            }
            if (canInteract)
            {
                HandleInteractionCheck();
                HandleInteractionInput();
            }
            ApplyFinalMovements();
        }
        isShooting |= Input.GetKeyDown(KeyCode.Alpha1);
    }
    private void FixedUpdate()
    {
        if (isShooting)
        {
            GameObject newProjectile = Instantiate(projectile, this.transform.position + new Vector3(0, 2, 0), this.transform.rotation);
            Rigidbody ProjectileRB = newProjectile.GetComponent<Rigidbody>();
            ProjectileRB.velocity = this.transform.forward * projectileSpeed;
            isShooting = false;
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
        }
    }
    private void HandleInteractionCheck()
    {
        if (Physics.Raycast(playerCamera.ViewportPointToRay(interactionRayPoint), out RaycastHit hit, interactionDistance))
        {
            if (hit.collider.gameObject.layer == 8 && (currentInteractable == null || hit.collider.gameObject.GetInstanceID() != currentInteractable.gameObject.GetInstanceID()))
            {
                hit.collider.TryGetComponent(out currentInteractable);

                if (currentInteractable)
                {
                    currentInteractable.OnFocus();
                }
            }
        }
        else if (currentInteractable)
        {
            currentInteractable.OnLoseFocus();
            currentInteractable = null;
        }

    }
    private void HandleInteractionInput()
    {
        if (Input.GetKeyDown(interactKey) && currentInteractable != null && Physics.Raycast(playerCamera.ViewportPointToRay(interactionRayPoint), out RaycastHit hit, interactionDistance, interactionLayer))
        {
            currentInteractable.OnInteract();
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
        }
        characterController.Move(moveDirection * Time.deltaTime);

        if (characterController.velocity.y < -1 && characterController.isGrounded)
            moveDirection.y = 0;
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

