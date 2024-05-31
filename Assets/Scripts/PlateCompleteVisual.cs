using System;
using UnityEngine;
using System.Collections.Generic;

public class PlateCompleteVisual : MonoBehaviour
{
    [Serializable]
    public struct KitchenObjectSO_GameObject
    {
        public KitchenObjectSO kitchenObjectSO;
        public GameObject gameObject;
    }
    [SerializeField] PlateKitchenObject plateKitchenObject;
    [SerializeField] List<KitchenObjectSO_GameObject> kitchenObjectsSOGameObjectList;
    // Start is called before the first frame update
    private void Start()
    {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
        foreach (KitchenObjectSO_GameObject kitchenObjectSO_GameObject in kitchenObjectsSOGameObjectList)
        {
                  kitchenObjectSO_GameObject.gameObject.SetActive(false);
        }
    }
    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventAgrs e)
    {
        foreach ( KitchenObjectSO_GameObject kitchenObjectSO_GameObject in kitchenObjectsSOGameObjectList)
        {
            if(kitchenObjectSO_GameObject.kitchenObjectSO == e.kitchenObjectSO)
            {
                kitchenObjectSO_GameObject.gameObject.SetActive(true);
            }
        }
    }
}

