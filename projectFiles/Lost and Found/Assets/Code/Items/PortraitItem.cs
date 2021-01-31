using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortraitItem : InteractableItem {
    public override void interact() {
        if(!hasInteracted) {
            Debug.Log("[PortraitItem] - Knocking portrait over!");
            BlockingItem hole = GameObject.Find("hole").GetComponent<BlockingItem> ();
            PlayerController dog = GameObject.Find("doggo").GetComponent<PlayerController>();

            hasInteracted = true;
            hole.unblock();
            dog.walkLeft(10);
            dog.changeDirection("right");
        } else {
            Debug.Log("[PortraitItem] - Portrait has already been knocked over");
        }
    }
}