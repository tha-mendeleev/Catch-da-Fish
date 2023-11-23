using System;
using UnityEngine;
public class BoatController
    : MonoBehaviour
{
    public float Direction = 0f;

    public bool IsMoving = false;

    public EventHandler Fishing;

    private void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SetDirection(-1);
            IsMoving = true;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            SetDirection(1);
            IsMoving = true;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            SetDirection(0);
            IsMoving = false;
        }

        if (Input.GetKey(KeyCode.F))
        {
            Fishing?.Invoke(this, EventArgs.Empty);
        }
    }

    private void SetDirection(float newDirection)
    {
        Direction = newDirection;
    }

}