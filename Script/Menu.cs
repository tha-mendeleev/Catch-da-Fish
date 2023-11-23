using TMPro;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] Boat boat;
    [SerializeField] FishingRod fishingRod;

    [SerializeField] TextMeshProUGUI boatSpeedText;
    [SerializeField] TextMeshProUGUI boatSpeedCostText;

    [SerializeField] TextMeshProUGUI rodSpeedText;
    [SerializeField] TextMeshProUGUI rodSpeedCostText;

    [SerializeField] TextMeshProUGUI rodPowerText;
    [SerializeField] TextMeshProUGUI rodPowerCostText;

    [SerializeField] TextMeshProUGUI rodDeptText;
    [SerializeField] TextMeshProUGUI rodDeptCostText;

    [SerializeField] TextMeshProUGUI capacityText;
    [SerializeField] TextMeshProUGUI capacityCostText;

    [SerializeField] TextMeshProUGUI fuelText;
    [SerializeField] TextMeshProUGUI fuelCostText;

    private float boatSpeed = 1f;
    private float boatSpeedCost = 8f;

    private float rodSpeed = .5f;
    private float rodSpeedCost = 8f;

    private float rodPower = .25f;
    private float rodPowerCost = 7f;

    private float rodDept = 1f;
    private float rodDeptCost = 7f;

    private float fuel = 20f;
    private float fuelCost = 8f;

    private float boatCapacity = 5;
    private float boatCapacityCost = 8;

    private float curDept = 0;

    private void Start()
    {
        boatSpeedText.text = $"+{boatSpeed} Km/h";
        boatSpeedCostText.text = $"{boatSpeedCost} Kg";

        rodSpeedText.text = $"+{rodSpeed} Km/h";
        rodSpeedCostText.text = $"{rodSpeedCost} Kg";

        rodPowerText.text = $"+{rodPower} Kg";
        rodPowerCostText.text = $"{rodPowerCost} Kg";

        rodDeptText.text = $"+{rodDept} Km";
        rodDeptCostText.text = $"{rodDeptCost} Kg";

        capacityText.text = $"+{boatCapacity} Kg";
        capacityCostText.text = $"{boatCapacityCost} Kg";

        fuelText.text = $"+{fuel} L";
        fuelCostText.text = $"{fuelCost} Kg";
    }

    public void UpgradeBoatSpeed()
    {
        if (IfGoodAmount(boatSpeedCost))
        {
            boat.UpgradeBoatSpeed(boatSpeed);
            boat.CutWeight(boatSpeedCost);
            boatSpeedCost *= 1.75f;
            boatSpeed++;
            boatSpeedText.text = $"+{boatSpeed} Km/h";
            boatSpeedCostText.text = $"{boatSpeedCost} Kg";
        }
    }

    public void UpgradeBoatCapacity()
    {
        if (IfGoodAmount(boatCapacityCost))
        {
            boat.UpgradeBoatCapacity(boatCapacity);
            boat.CutWeight(boatCapacityCost);
            boatCapacityCost = boat.Capacity() - 5;
            boatCapacity = Random.Range(4, 9);
            capacityText.text = $"+{boatCapacity} Kg";
            capacityCostText.text = $"{boatCapacityCost} Kg";
        }
    }

    public void BuyFuel()
    {
        if (IfGoodAmount(fuelCost))
        {
            boat.AddFuel(fuel);
            boat.CutWeight(fuelCost);
            fuelCost += 1;
            fuelCostText.text = $"{fuelCost} Kg";
        }
    }

    public void UpgradeRodSpeed()
    {
        if (IfGoodAmount(rodSpeedCost))
        {
            fishingRod.UpSpeed(rodSpeed);
            boat.CutWeight(rodSpeedCost);
            rodSpeedCost += 5f;
            rodSpeedText.text = $"{rodSpeed} Km/h";
            rodSpeedCostText.text = $"{rodSpeedCost} Kg";
        }
    }
    public void UpgradeRodPower()
    {
        if (IfGoodAmount(rodPowerCost))
        {
            fishingRod.UpPower(rodPower);
            boat.CutWeight(rodPowerCost);
            rodPowerCost += 5f;
            rodPowerCostText.text = $"{rodPowerCost} Kg";
        }
    }
    public void UpgradeRodDept()
    {
        if (IfGoodAmount(rodDeptCost))
        {
            if (curDept > 5) return;
            fishingRod.UpDept(rodDept);
            boat.CutWeight(rodDeptCost);
            rodDeptCost *= 1.5f;
            rodDeptCostText.text = $"{rodDeptCost} Kg";
            curDept++;
        }
    }

    private bool IfGoodAmount(float cost)
    {
        return boat.CurrentWeight() >= cost;
    }
}
