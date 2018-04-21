using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CancelCardSelection : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        HandController.Instance.UnselectAll();
        CardSystemController.Instance.ClearSelection();
    }
}
