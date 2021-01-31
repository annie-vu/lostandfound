using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericItem : MonoBehaviour {
    // attribute that determines whether the item is interactable
    public abstract bool isInteractable { get; }

    // attribute that determines whether the item should block the dog movement
    public abstract bool isBlocking { get; }
}
