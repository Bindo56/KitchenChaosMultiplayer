using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    public event EventHandler<OnIngredientAddedEventAgrs> OnIngredientAdded;
    public class OnIngredientAddedEventAgrs : EventArgs
    {
        public KitchenObjectSO kitchenObjectSO;
    }

    List<KitchenObjectSO> kitchenObjectSOList;

    [SerializeField] List<KitchenObjectSO> validKitchenObjectSO;

    private void Awake()
    {
        kitchenObjectSOList = new List<KitchenObjectSO>();
    }

    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
    {
        if (!validKitchenObjectSO.Contains(kitchenObjectSO))
        {
            return false;
        }

        if (kitchenObjectSOList.Contains(kitchenObjectSO))
        {//alreayhas the object in desk
            return false;
        }
        else
        {
            kitchenObjectSOList.Add(kitchenObjectSO);
            OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventAgrs
            {
                kitchenObjectSO = kitchenObjectSO
            });

            return true;
        }
    }

    public List<KitchenObjectSO> GetKitchenObjectSOList()
    {
        return kitchenObjectSOList;
    }

}
