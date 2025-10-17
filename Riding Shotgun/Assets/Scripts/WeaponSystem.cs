using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public UnityEvent OnWeaponShoot;
    public float ShootCooldown;

    public bool Automatic;

    private float CurrentCooldown;

    private void Start()
    {
        CurrentCooldown = ShootCooldown;
    }

    private void Update()
    {
        if (Automatic)
        {
            if (Input.GetMouseButton(0))
            {
                if (CurrentCooldown <= 0f)
                {
                    OnWeaponShoot?.Invoke();
                    CurrentCooldown = ShootCooldown;
                }
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (CurrentCooldown <= 0f)
                {
                    OnWeaponShoot?.Invoke();
                    CurrentCooldown = ShootCooldown;
                }
            }
        }

        CurrentCooldown -= Time.deltaTime;
    }
}