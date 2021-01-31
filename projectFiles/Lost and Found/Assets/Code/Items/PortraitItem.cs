using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortraitItem : InteractableItem {
    public override void interact() {
        if(!hasInteracted) {
            Debug.Log("[PortraitItem] - Knocking portrait over!");
            hasInteracted = true;
            BlockingItem hole = GameObject.Find("hole").GetComponent<BlockingItem> ();
            hole.unblock();
        } else {
            Debug.Log("[PortraitItem] - Portrait has already been knocked over");
        }
    }
}