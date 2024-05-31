using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour , IKitchenObjectParent
{
    [SerializeField] public Transform KitchenObjectHoldPoint;
    [SerializeField] KitchenObject kitchenObject;
    public static Player Instance { get; private set; }


    public event EventHandler OnPickup;
    public event EventHandler<OnSelectedCounterChangeEventArgs> OnSelectedCounterChanged;


    public class OnSelectedCounterChangeEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }


    [SerializeField] float moveSpeed;
    [SerializeField] GameInput gameInput;
    [SerializeField] LayerMask counterLayerMask;
    bool isWalking;
    Vector3 lastInteractDir;

    BaseCounter selectedCounter;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("THere is more then one player script");
        }

        Instance = this;
    }
    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
        gameInput.OnInteractAlternativeAction += GameInput_OnInteractAlternativeAction;
    }

    private void GameInput_OnInteractAlternativeAction(object sender, EventArgs e)
    {
        if (!GameManager.Instane.IsGamePlaying()) return;

        if (selectedCounter != null)
        {
            selectedCounter.InteractAlternate(this);
        }
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if (!GameManager.Instane.IsGamePlaying()) return;

        if (selectedCounter != null)
        {
            selectedCounter.Interact(this);
        }

    }

    private void Update()
    {
        HandleInput();
        HandleInteraction();

    }


    private void HandleInteraction()
    {
        Vector2 inputVector = gameInput.GetMovementVector();


        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);


        if(moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;
        }

        float interactDistance = 2f;
        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit hit, interactDistance,counterLayerMask))
        {
            if (hit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                // clearCounter.Interact();
                if (baseCounter != selectedCounter)
                {
                    SetSelected(baseCounter);


                }
            }
            else
                SetSelected(null);
        }else 
            SetSelected(null);


    }

    private void SetSelected(BaseCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangeEventArgs
        {
            selectedCounter = selectedCounter
        });
    }

    private void HandleInput()
    {
        Vector2 inputVector = gameInput.GetMovementVector();


        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        float playerHeight = 2f;
        float moveDis = moveSpeed * Time.deltaTime;
        float playerRadius = .8f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDis);

        if (!canMove)
        {
            //move in x if collider is in fornt

            Vector3 moveDirX = new Vector3(moveDir.x, 0f, 0f).normalized;

            canMove = (moveDir.x < -.5f || moveDir.x > +.5f) && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDis);

            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                Vector3 moveDirZ = new Vector3(moveDir.z, 0f, 0f).normalized;
                canMove = (moveDir.z < -.5f || moveDir.z > +.5f) && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDis);

                if (canMove)
                {
                    moveDir = moveDirZ;
                }
                else
                {
                    //canot move in anyDir
                }
            }
        }

        if (canMove)
        {
            transform.position += moveDir * moveDis;
        }



        isWalking = moveDir != Vector3.zero;

        float rotationSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotationSpeed);
    }

    public bool IsWalking()
    {
        return isWalking;
    }



    public Transform GetKitchenObjectFollowTransform()
    {
        return KitchenObjectHoldPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
        if(kitchenObject != null)
        {
            OnPickup?.Invoke(this, EventArgs.Empty);
        }
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }

}
