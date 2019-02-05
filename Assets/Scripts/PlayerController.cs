using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    
    [SerializeField] private AnimationCurve jumpFallOff;

    private float moveSpeed;

    private List<Vector3> movementHistory;

    [Tooltip("Radius which to make animals look for the player and flee")]
    [SerializeField] private float animalAlertRadius;

    [SerializeField] private float walkNoise, runNoise;
    [SerializeField] private float walkSpeed, runSpeed;
    [SerializeField] private float runBuildUp;
    [SerializeField] private KeyCode runKey;

    [SerializeField] private float jumpMultiplier;
    [SerializeField] private KeyCode jumpKey;


    [SerializeField] private float slopeForce;
    [SerializeField] private float slopeRayLength;

    private bool isJumping;
    private CharacterController charController;
    

    private void Awake()
    {
        charController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        movementHistory = new List<Vector3>();
        AnimalController.CriticalAnimalAiUpdate += AlertAnimals;
    }


    private void AlertAnimals()
    {
        Collider[] colls = Physics.OverlapSphere(transform.position, animalAlertRadius);
        colls.ToList().ForEach(Coll => {
            AnimalScript animal = Coll.gameObject.GetComponent<AnimalScript>();
            if (animal != null)
            {
                //animal.Alert();
            }
        });
    }

    private void FixedUpdate()
    {

    }

    private void Update()
    {
        PlayerMovement();


        if (movementHistory.Count > 10)
        {
            movementHistory.RemoveAt(0);
        }
        movementHistory.Add(new Vector3(transform.position.x, 0, transform.position.z));

        float deltaMovement = 0;

        for (int i = 0; i < movementHistory.Count - 1; i++)
        {
            deltaMovement += Vector3.Distance(movementHistory[i], movementHistory[i + 1]);
        }

        NoiseManager.SendNoise(transform.position, deltaMovement * 10);

        Jump();
        
        
        
    }

    private void PlayerMovement()
    {
        float vertInput = Input.GetAxis("Vertical");
        float horiInput = Input.GetAxis("Horizontal");



        if (vertInput != 0 || horiInput != 0) {

            Vector3 forwardMovement = transform.forward * vertInput;
            Vector3 sideMovement = transform.right * horiInput;

            SetMovementSpeed();

            charController.SimpleMove(Vector3.ClampMagnitude(forwardMovement + sideMovement, 1.0f) * moveSpeed);

            if ((vertInput != 0 || horiInput != 0) && OnSlope())
                charController.Move(Vector3.down * charController.height / 2 * slopeForce);
        } else
        {
            charController.SimpleMove(Vector3.ClampMagnitude(Vector3.zero, 1.0f) * moveSpeed);
        }
    }


    private void SetMovementSpeed()
    {
        if (Input.GetKey(runKey))
        {
            moveSpeed = Mathf.Lerp(moveSpeed, runSpeed, runBuildUp);
        } else
        {
            moveSpeed = Mathf.Lerp(moveSpeed, walkSpeed, runBuildUp);
        }
    }

    private void Jump()
    {
         if(Input.GetKeyDown(jumpKey) && isJumping == false)
        {
            isJumping = true;
            StartCoroutine(JumpEvent());
        }
    }

    private IEnumerator JumpEvent()
    {
        charController.slopeLimit = 90;
        float time = 0f;

        do
        {
            float jumpforce = jumpFallOff.Evaluate(time);
            charController.Move(Vector3.up * jumpforce * jumpMultiplier * Time.deltaTime);
            time += Time.deltaTime;
            yield return null;
        } while (charController.isGrounded == false && charController.collisionFlags != CollisionFlags.Above);

        charController.slopeLimit = 45;
        isJumping = false;
    }

    private bool OnSlope()
    {
        if (isJumping)
            return false;


        RaycastHit hit;
        if(Physics.Raycast(transform.position, Vector3.down, out hit, charController.height / 2 * slopeRayLength))
        {
            if(hit.normal != Vector3.up)
                return true;
        }

        return false;
    }

}
