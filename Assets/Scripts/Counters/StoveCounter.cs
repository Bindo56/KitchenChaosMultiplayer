using System;
using UnityEngine;

public class StoveCounter : BaseCounter, IHasProgress
{
    public event EventHandler<IHasProgress.OnProgressChangedEventArges> OnProgressChanged;
    public event EventHandler<OnStateChangeEventArgs> OnStateChanged;
    public class OnStateChangeEventArgs
    {
        public State state;
    }

    public enum State
    {
        Idle,
        Frying,
        Fried,
        Burned
    }

    public State state;

    [SerializeField] FryingRecipeSO[] fryingRecipeArray;
    [SerializeField] BurningRecipeSO[] burningRecipeArray;
    float fryingTimer;
    float burnedTimer;
    FryingRecipeSO fryingRecipeSO;
    BurningRecipeSO burningRecipeSO;


    private void Update()
    {
        if (HasKitchenObject())
        {

            switch (state)
            {
                case State.Idle:

                    break;
                case State.Frying:
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArges
                    {
                        progressNormalized = fryingTimer / fryingRecipeSO.fryingTimerMax
                    });
                    fryingTimer += Time.deltaTime;
                    fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                    if (fryingTimer > fryingRecipeSO.fryingTimerMax)
                    {
                        //fried
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this);

                        OnStateChanged?.Invoke(this, new OnStateChangeEventArgs
                        {
                            state = State.Frying
                        });


                        burnedTimer = 0f;
                        burningRecipeSO = GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                        state = State.Fried;
                    }
                    break;
                case State.Fried:
                    burnedTimer += Time.deltaTime;
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArges
                    {
                        progressNormalized = burnedTimer / burningRecipeSO.burningTimerMax
                    });
                    burningRecipeSO = GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                    if (burnedTimer > burningRecipeSO.burningTimerMax)
                    {
                        //fried
                        GetKitchenObject().DestroySelf();

                        KitchenObject.SpawnKitchenObject(burningRecipeSO.output, this);
                        state = State.Burned;

                        OnStateChanged?.Invoke(this, new OnStateChangeEventArgs
                        {
                            state = state
                        });

                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArges
                        {
                            progressNormalized = 0f
                        }); 



                    }
                    break;
                case State.Burned:
                    OnStateChanged?.Invoke(this, new OnStateChangeEventArgs
                    {
                        state = state,
                    });
                    break;
            }

        }

    }

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            //there is no kitchen object
            if (player.HasKitchenObject())
            {
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    //Player is carringSomething that can be fry
                    player.GetKitchenObject().SetKitchenObjectParent(this);


                    fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                    state = State.Frying;

                    fryingTimer = 0;

                    OnStateChanged?.Invoke(this, new OnStateChangeEventArgs { state = state, });

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArges
                    {
                        progressNormalized = fryingTimer / fryingRecipeSO.fryingTimerMax
                    });
                }
                //player is carring object
            }
            else
            {
                //player not carring object 
            }
        }
        else
        {
            //there is kitchen object in counter
            if (player.HasKitchenObject())
            {
                //player is carring object
                //player is carring plate
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {

                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {

                        GetKitchenObject().DestroySelf();

                        state = State.Idle;
                        OnStateChanged?.Invoke(this, new OnStateChangeEventArgs
                        {
                            state = state,
                        });

                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArges
                        {
                            progressNormalized = 0f
                        });
                    }
                }
            }
            else
            {
                GetKitchenObject().SetKitchenObjectParent(player);
                state = State.Idle;
                OnStateChanged?.Invoke(this, new OnStateChangeEventArgs
                {
                    state = state,
                });

                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArges
                {
                    progressNormalized = 0f
                });
            }
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObject)
    { //think

        FryingRecipeSO FryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObject);
        return FryingRecipeSO != null;

    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObject)
    {
        //think
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObject);
        if (fryingRecipeSO != null)
        {
            return fryingRecipeSO.output;
        }
        else
        {
            return null;
        }

    }

    private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    { //think
        foreach (FryingRecipeSO FryingRecipeSO in fryingRecipeArray)
        {
            if (FryingRecipeSO.input == inputKitchenObjectSO)
            {
                return FryingRecipeSO;
            }
        }
        return null;


    }
    private BurningRecipeSO GetBurningRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    { //think
        foreach (BurningRecipeSO BurningRecipeSO in burningRecipeArray)
        {
            if (BurningRecipeSO.input == inputKitchenObjectSO)
            {
                return BurningRecipeSO;
            }
        }
        return null;
    }

    public bool IsFired()
    {
        return state == State.Fried;
    }


}

