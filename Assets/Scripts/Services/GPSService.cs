using UnityEngine;
using System.Collections;

public class GPSService: MonoBehaviour
{
  IEnumerator Start()
  {
    for (int wait = 20; !Input.location.isEnabledByUser && wait > 0; wait -= 1) {
      yield return new WaitForSeconds(1);
    }

    if (!Input.location.isEnabledByUser) {
      Debug.LogError("[GPS Service] GPS is disabled by user");
      yield break;
    }
    
    Input.location.Start();

    for (
      int wait = 20;
      (Input.location.status == LocationServiceStatus.Stopped || Input.location.status == LocationServiceStatus.Initializing) && wait > 0;
      wait -= 1
    ) {
      yield return new WaitForSeconds(1);
    }

    if (Input.location.status == LocationServiceStatus.Initializing) {
      Debug.LogError("[GPS Serivce] TIME OUT");
      yield break;
    }
    if (Input.location.status == LocationServiceStatus.Failed) {
      Debug.LogError("[GPS Serivce] Unable to determine device location");
      yield break;
    }

    while (true) {
      Debug.Log("[GPS Service] Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude);
      yield return new WaitForSeconds(1);
    }
  }
}