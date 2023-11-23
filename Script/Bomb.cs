using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour, ISeaItem
{
    public void Caught(float rodPower)
    {
        Boat.Instance.Sink = true;
        KillerSpawner.RemoveBomb();
        Destroy(gameObject);
    }
}
