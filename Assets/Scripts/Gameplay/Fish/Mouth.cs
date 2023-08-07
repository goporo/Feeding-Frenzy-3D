using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouth : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.tag == "FishBody" || other.gameObject.tag == "FishMouth") && other.gameObject.GetComponentInParent<Fish>() != this.GetComponentInParent<Fish>())
        {
            this.GetComponentInParent<Fish>().Eat(other.gameObject.GetComponentInParent<Fish>());
        }
    }
}
