using System;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{

    [SerializeField] RecipeListSO recipeListSO;

    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnSuccess;
    public event EventHandler OnFailed;
    public static DeliveryManager Instance { get; private set; }

    private List<RecipeSO> waitingRecipeSOList;
    float spawnRecipeTimer;
    float spawnRecipeTimerMax = 4f;
    int watingRecipeList = 4;
     int successfulRecipeAmount;
    private void Awake()
    {
        Instance = this;
        waitingRecipeSOList = new List<RecipeSO>();
    }
    private void Start()
    {
        spawnRecipeTimer = spawnRecipeTimerMax;
    }

    private void Update()
    {
        spawnRecipeTimer = -Time.deltaTime;

        if (spawnRecipeTimer <= 0f)
        {
            spawnRecipeTimer = spawnRecipeTimerMax;

            if ( GameManager.Instane.IsGamePlaying() && waitingRecipeSOList.Count < watingRecipeList)
            {

                RecipeSO waitingRecipe = recipeListSO.Recipes[UnityEngine.Random.Range(0, recipeListSO.Recipes.Count)];
               // Debug.Log(waitingRecipe);
                waitingRecipeSOList.Add(waitingRecipe);
                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }
        }


    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {

        for (int i = 0; i < waitingRecipeSOList.Count; i++)
        {
            RecipeSO waitingRecipeSO = waitingRecipeSOList[i];
            if (waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
            {
                //Has Same NUmber of ingrident
                bool platerContentMatchesRecipe = true;
                foreach (KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList)
                {
                    //cycling through all indgrident in the recipe
                    bool ingredientFound = false;
                    foreach (KitchenObjectSO platekitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
                    {
                        //cycling through all the recipe in the plate 
                        if (platekitchenObjectSO == recipeKitchenObjectSO)
                        {
                            //ingredientFound
                            ingredientFound = true;
                            break;

                        }
                    }
                    if (!ingredientFound)
                    {
                        platerContentMatchesRecipe = false;
                    }
                }
                if (platerContentMatchesRecipe)
                {
                    successfulRecipeAmount++;
                    Debug.Log("Player deliver the correct recipe");
                    waitingRecipeSOList.RemoveAt(i);
                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    OnSuccess?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
        }
        //idgredient not found
        Debug.Log("Player did not deliver the correct recipe");
        OnFailed?.Invoke(this, EventArgs.Empty);
    }


    public List<RecipeSO> GetWaitingReciepSOLsit()
    {
        return waitingRecipeSOList;
    }

    public int GetSucessfulRecipeDelivered()
    {
        return successfulRecipeAmount;
    }
}
