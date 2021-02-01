using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class InteractableItem : GenericItem {
    public override bool isInteractable {
        get {
            return true;
        }
    }
    public override bool isBlocking {
        get {
            return false;
        }
    }

    // attribute that indicates whether the item has already been interacted with
    public bool hasInteracted;
    public abstract void interact();
}
