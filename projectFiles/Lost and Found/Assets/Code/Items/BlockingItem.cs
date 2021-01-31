using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BlockingItem : GenericItem {
    private bool _isBlocking = true;
    public override bool isBlocking {
        get {
            return _isBlocking;
        }
    }
    public override bool isInteractable {
        get {
            return false;
        }
    }

    public void unblock() {
        Debug.Log($"[BlockingItem] - Unblocking state for {this.name}");
        this._isBlocking = false;
    }
}
