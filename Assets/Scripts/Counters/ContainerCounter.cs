using System;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    public event EventHandler OnPlayerGrabObject;
    [SerializeField] KitchenObjectSO kitchenObjectPrefab;

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {

            KitchenObject.SpawnKitchenObject(kitchenObjectPrefab, player);
            OnPlayerGrabObject?.Invoke(this, EventArgs.Empty);
            //  KitchenObjectTransform.localPosition = Vector3.zero;


        }

    }

}
