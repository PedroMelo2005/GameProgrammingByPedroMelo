using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonUI : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler {
    public void OnPointerEnter(PointerEventData eventData) {
        /*
        SoundManager.Instance.PlaySound(SoundManager.Instance.hoverSound);
        */
    }

    public void OnPointerClick(PointerEventData eventData) {
        /*
        SoundManager.Instance.PlaySound(SoundManager.Instance.clickSound);
        */
    }

}