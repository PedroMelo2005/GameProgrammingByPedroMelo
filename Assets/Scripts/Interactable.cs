using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Interactable : MonoBehaviour {
    public UnityEvent onInteraction;

    public string message;

    // Start is called before the first frame update
    void Start() {

    }

    public void Interact() {
        onInteraction.Invoke();
    }

}