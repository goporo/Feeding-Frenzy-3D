using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouth : MonoBehaviour
{
        private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "FishBody")
        {
            this.GetComponentInParent<Fish>().Eat(other.gameObject.GetComponentInParent<Fish>());
        }
    }
}