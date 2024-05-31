using System;
using UnityEngine;

public class PlateCounter : BaseCounter
{

    public event EventHandler OnPlateSpwaned;
    public event EventHandler OnPlateRemoved;

    [SerializeField] KitchenObjectSO plateKitchenObjectSO;

    float spawnPlateTimer;
    float spawnPlateTimerMax = 4f;
    int plateSpawnAmount;
    int plateSpawnAmountmax = 4;


    private void Update()
    {
        spawnPlateTimer += Time.deltaTime;
        if (spawnPlateTimer > spawnPlateTimerMax)
        {
            spawnPlateTimer = 0;
            if ( GameManager.Instane.IsGamePlaying() && plateSpawnAmount < plateSpawnAmountmax)
            {
                plateSpawnAmount++;
                OnPlateSpwaned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            if (plateSpawnAmount > 0)
            {
                plateSpawnAmount--;

                KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);

                OnPlateRemoved?.Invoke(this, EventArgs.Empty);
            }

        }
    }



}
