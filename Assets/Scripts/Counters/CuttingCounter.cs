using UnityEngine;
using System;

public class CuttingCounter : BaseCounter ,IHasProgress
{
    public static event EventHandler OnAnyCut;

   new public static void ResetStaticData()
    {
        OnAnyCut = null;
    }
    public event EventHandler OnCut;
    public event EventHandler<IHasProgress.OnProgressChangedEventArges> OnProgressChanged;
    public class OnProgressChangedEventArges : EventArgs
    {
        public float progressNormalized;
    }

    [SerializeField] CuttingRecipeSO[] cuttingRecipeArray;
    [SerializeField] int cuttingProgress;
    public override void Interact(Player player)
    {

        if (!HasKitchenObject())
        {
            //there is no kitchen object
            if (player.HasKitchenObject())
            {
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    cuttingProgress = 0;
                    CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArges
                    {
                        progressNormalized = (float)cuttingProgress/ cuttingRecipeSO.cuttingProgressMax
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
                    }
                }
            }
            else
            {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

    public override void InteractAlternate(Player player)
    { //think

        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
        {

            cuttingProgress++;

            OnCut?.Invoke(this, EventArgs.Empty);
            OnAnyCut?.Invoke(this, EventArgs.Empty);
            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArges
            {
                progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
            });
            if (cuttingProgress >= cuttingRecipeSO.cuttingProgressMax)
            {

                KitchenObjectSO outputKitchenObject = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
                //There is kitchenObject here and it can be cut
                GetKitchenObject().DestroySelf();
                KitchenObject.SpawnKitchenObject(outputKitchenObject, this);
            }
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObject)
    { //think

        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObject);
        return cuttingRecipeSO != null;

    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObject)
    {
        //think
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObject);
        if (cuttingRecipeSO != null)
        {
            return cuttingRecipeSO.output;
        }
        else
        {
            return null;
        }

    }

    private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    { //think
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeArray)
        {
            if (cuttingRecipeSO.input == inputKitchenObjectSO)
            {
                return cuttingRecipeSO;
            }
        }
        return null;


    }






}
