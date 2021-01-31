using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortraitItem : InteractableItem {
    public override void interact() {
        if(!hasInteracted) {
            Debug.Log("[PortraitItem] - Knocking portrait over!");
            BlockingItem hole = GameObject.Find("hole").GetComponent<BlockingItem> ();
            PlayerController dog = GameObject.Find("doggo").GetComponent<PlayerController>();

            // update portrait state because it has already been interacted with
            // update hole state because portrait is now covering it
            hasInteracted = true;
            hole.unblock();

            // "cutscene", move dog to the left and then face right
            StartCoroutine(dog.walkLeftRoutine(10, "right"));
        } else {
            Debug.Log("[PortraitItem] - Portrait has already been knocked over");
        }
    }
}