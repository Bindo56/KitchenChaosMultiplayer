using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour , IKitchenObjectParent
{

    public static event EventHandler OnDrop;

    public static void ResetStaticData()
    {
        OnDrop = null;
    }

    [SerializeField] public Transform topPoint;
     KitchenObject kitchenObject;

    public virtual void Interact(Player player)
    {
        Debug.LogError("BaseOnteract");
    }

    public virtual void InteractAlternate(Player player)
    {
       // Debug.LogError("InteractAlternate");
    }
    public Transform GetKitchenObjectFollowTransform()
    {
        return topPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
        if(kitchenObject != null )
        {
            OnDrop?.Invoke(this,EventArgs.Empty);
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
