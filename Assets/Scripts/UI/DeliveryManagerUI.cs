using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] Transform container;
    [SerializeField] Transform recipetemplate;

    private void Awake()
    {
        recipetemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSpawned += DeliveryManager_OnRecipeSpawned;
        DeliveryManager.Instance.OnRecipeCompleted += DeliveryManager_OnRecipeCompleted;
        UpdateVisual();
    }

    private void DeliveryManager_OnRecipeSpawned(object sender, System.EventArgs e)
    {
       UpdateVisual();
    }

    private void DeliveryManager_OnRecipeCompleted(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach(Transform child in container)
        {
            if (child == recipetemplate) continue;
            Destroy(child.gameObject);
        }

        foreach(RecipeSO recipeSO in DeliveryManager.Instance.GetWaitingReciepSOLsit())
        {
            Transform recipetranform = Instantiate(recipetemplate,container);
            recipetranform.gameObject.SetActive(true);
            recipetranform.GetComponent<DeveliveryManagerIconUI>().SetRecipeSO(recipeSO);//
        }
    }


}
