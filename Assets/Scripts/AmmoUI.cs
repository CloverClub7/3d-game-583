using UnityEngine;
using UnityEngine.UI;

public class AmmoUI : MonoBehaviour
{
    public Image[] bulletIcons; // Assign bullet images in Inspector

    public void UpdateAmmoDisplay(int currentAmmo)
    {
        if (bulletIcons == null || bulletIcons.Length == 0)
        {
            Debug.LogWarning("AmmoUI: bulletIcons array is empty or not assigned.");
            return;
        }

        // Clamp ammo to valid range
        currentAmmo = Mathf.Clamp(currentAmmo, 0, bulletIcons.Length);

        for (int i = 0; i < bulletIcons.Length; i++)
        {
            if (bulletIcons[i] == null)
            {
                Debug.LogWarning($"AmmoUI: bulletIcons[{i}] is not assigned.");
                continue;
            }

            // Show bullets that haven't been fired
            bulletIcons[i].enabled = i < currentAmmo;
        }

        // Optional: log once to see the current ammo driving UI
        Debug.Log($"AmmoUI: Updated ammo display to {currentAmmo}");
    }
}
