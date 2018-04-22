using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour
{
    public CardData Card;

    void OnTriggerEnter(Collider other)
    {
        var player = other.transform.GetComponents<PlayerStats>();

        if (player != null)
        {
            if (HandController.Instance.CanAddCards())
            {
                HandController.Instance.AddCard(Card);
                gameObject.SetActive(false);
            }
        }
    }

}
