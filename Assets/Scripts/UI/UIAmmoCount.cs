using UnityEngine;
using UnityEngine.UI;

/// <summary>
///     This class hold a reference to the text to update when the ammo count change.
///     It is a singleton so it can be called from anywhere (e.g. PlayerController SetAmmo)
/// </summary>
public class UIAmmoCount : MonoBehaviour
{
    public Text countText;
    public static UIAmmoCount Instance { get; private set; }

    // Use this for initialization
    private void Awake()
    {
        Instance = this;
    }

    public void SetAmmo(int count, int max)
    {
        countText.text = "x" + count + "/" + max;
    }
}