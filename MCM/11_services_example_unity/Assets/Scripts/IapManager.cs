using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class IapManager : MonoBehaviour {

    public void OnPurchaseComplete(Product product) {
        Debug.Log("purchased product: " + 
                  (product?.definition?.id ?? "(none)"));
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason reason) {
        Debug.Log("failed to purchase: " + 
                  (product?.definition?.id ?? "(none)") + 
                  ", reason: " + reason);
    }
}
