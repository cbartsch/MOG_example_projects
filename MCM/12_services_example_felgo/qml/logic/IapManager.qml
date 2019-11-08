import Felgo 3.0
import QtQuick 2.0

Item {

  id: item

  readonly property bool hasRemovedAds: noAdsGood.purchased
  readonly property int tokenBalance: tokenGood.balance

  Store {
    id: store

    version: 1
    // secret encrypts store data locally on device
    secret: "local_secret"

    // From Google Play Developer Console, when using Google Play billing service
    androidPublicKey: "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAwEGMu4kGOAfZajtTwhTz4zw8J6f5c7SdIe0xRRH8EmSdI6MTWUf4qJ7LywBOmydfzvGgEeUN4j7PybQgrizbqvg5BU7I1ZCiRPUIZxHcQpHxoqsaUMGoh7fADhH3xIoF/BA+OPFeec14Wm4I2BB6tvoUczCPFI4ZWM8hFflqfe/ZKDtoVK1qAlA83RQS3N4G31yS/S2baFMWBQ1BpZbT7KKXIBHNoZXOL+qPOY50+rgk0i0gVKOrYrpxchI9rF2Qhg+FvicPAyvBHI4gL//yT0AL+7VHqYT0uwyXPkc5ZwX3rCcJ47gd/o1v9kLYQRcFSNKwGns9OX4OmMvixTaThwIDAQAB"

    goods: [
      LifetimeGood {
        id: noAdsGood
        itemId: "remove_ads"

        purchaseType: StorePurchase {
          id: noAdPurchase
          price: 10
          productId: noAdsGood.itemId
        }
      },
      SingleUseGood {
        id: tokenGood
        itemId: "token"

        purchaseType: StorePurchase {
          id: tokenPurchase
          price: 1
          productId: tokenGood.itemId
        }
      }
    ]

    onItemPurchased: {
      console.log("Purchased item:", itemId)
    }
  }

  function buyNoAds() {
    console.log("buy no ads", noAdsGood.itemId)

    store.buyItem(noAdsGood.itemId)
  }

  function restoreAds() {
    console.log("restore ads", noAdsGood.itemId)

    store.takeItem(noAdsGood.itemId)
  }

  function buyTokens() {
    console.log("buy tokens", tokenGood.itemId)

    store.buyItem(tokenGood.itemId)
  }

  function spendTokens(amount) {
    console.log("spend", amount, "tokens", tokenGood.itemId)

    store.takeItem(tokenGood.itemId, 1)
  }
}
