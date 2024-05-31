using UnityEngine;

public class ClearCounter : BaseCounter
{

    [SerializeField] KitchenObjectSO kitchenObjectPrefab;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            //there is no kitchen object
            if (player.HasKitchenObject())
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);
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
                //player is carring plate
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {

                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {

                        GetKitchenObject().DestroySelf();
                    }

                }
                else
                {
                    if (GetKitchenObject().TryGetPlate(out plateKitchenObject))
                    {
                        if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
                        {
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }

                //player is carring object
            }
            else
            {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }

    }


}
