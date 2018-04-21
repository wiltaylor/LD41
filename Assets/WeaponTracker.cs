using System;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class WeaponTracker : MonoBehaviour
{
    public Sprite BeamIcon;
    public Sprite BoltIcon;
    public Text AmmountText;
    public Image IconImage;
    public GameObject CountObject;


    void Update()
    {
        if (PlayerCombatController.Instance.CurrentWeapon == WeaponType.None)
        {
            CountObject.SetActive(false);
            IconImage.enabled = false;
        }
        else
        {
            CountObject.SetActive(true);
            IconImage.enabled = true;
        }

        AmmountText.text = Mathf.RoundToInt(PlayerCombatController.Instance.Ammount).ToString();

        switch (PlayerCombatController.Instance.CurrentWeapon)
        {
            case WeaponType.Bolt:
                IconImage.sprite = BoltIcon;
                break;
            case WeaponType.Beam:
                IconImage.sprite = BeamIcon;
                break;
            default:
                break;
        }
    }
}
