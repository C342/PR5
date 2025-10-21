using System.Collections;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class WeaponSystem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI reloadText;

    public UnityEvent OnWeaponShoot;
    public float ShootCooldown;

    public bool Automatic;

    private float CurrentCooldown;

    private void Start()
    {
        CurrentCooldown = 0f;
        if (reloadText != null)
        {
            reloadText.text = "1 / 1";
        }
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
                    ReloadTimer();
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
                    ReloadTimer();
                }
            }
        }

        CurrentCooldown -= Time.deltaTime;
    }

    public void ReloadTimer()
    {
        StartCoroutine(ReloadRoutine());
    }

    private IEnumerator ReloadRoutine()
    {
        if (reloadText != null)
        {
            reloadText.text = "0 / 1";
        }

        yield return new WaitForSeconds(3f);

        if (reloadText != null)
        {
            reloadText.text = "1 / 1";
        }
    }
}