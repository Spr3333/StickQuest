using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pill : MonoBehaviour
{
    bool hasActivated = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!hasActivated && other.CompareTag("Player"))
        {
            if (gameObject.CompareTag("RedPill"))
            {
                hasActivated = true;
                gameObject.SetActive(false);
                GameObject[] redBlocks = GameObject.FindGameObjectsWithTag("RedBlock");
                foreach (GameObject redBlock in redBlocks)
                {
                    Collider triggerCollider = redBlock.GetComponent<Collider>();
                    if (triggerCollider != null)
                    {
                        triggerCollider.isTrigger = true;
                    }
                }
            }
            else if (gameObject.CompareTag("BluePill"))
            {
                hasActivated = true;
                gameObject.SetActive(false);
                GameObject[] blueBlocks = GameObject.FindGameObjectsWithTag("BlueBlock");
                foreach (GameObject blueBlock in blueBlocks)
                {
                    Collider triggerCollider = blueBlock.GetComponent<Collider>();
                    if (triggerCollider != null)
                    {
                        triggerCollider.isTrigger = true;
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (hasActivated && other.CompareTag("Player"))
        {
            if (gameObject.CompareTag("RedPill"))
            {
                GameObject[] redBlocks = GameObject.FindGameObjectsWithTag("RedBlock");
                foreach (GameObject redBlock in redBlocks)
                {
                    Collider triggerCollider = redBlock.GetComponent<Collider>();
                    if (triggerCollider != null)
                    {
                        triggerCollider.isTrigger = false;
                    }
                }
            }
            else if (gameObject.CompareTag("BluePill"))
            {
                GameObject[] blueBlocks = GameObject.FindGameObjectsWithTag("BlueBlock");
                foreach (GameObject blueBlock in blueBlocks)
                {
                    Collider triggerCollider = blueBlock.GetComponent<Collider>();
                    if (triggerCollider != null)
                    {
                        triggerCollider.isTrigger = false;
                    }
                }
            }
        }
    }
}