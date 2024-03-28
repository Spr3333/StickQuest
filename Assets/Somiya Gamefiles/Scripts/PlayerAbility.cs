using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbility : MonoBehaviour
{
    bool hasRedAbility = false;
    bool hasBlueAbility = false;
    public float pillTimer = 5;

    public GameObject redPill;
    public GameObject bluePill;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RedPill"))
        {
            hasRedAbility = true;
            DeactivateBlockTriggers("RedBlock");

            hasBlueAbility = false;
            ReactivateBlockTriggers("BlueBlock");
            // disable blue ability when red is active

            other.gameObject.SetActive(false);
            StartCoroutine(Pill(other.gameObject));
        }
        else if (other.CompareTag("BluePill"))
        {
            hasBlueAbility = true;
            DeactivateBlockTriggers("BlueBlock");

            hasRedAbility = false;
            ReactivateBlockTriggers("RedBlock");
            // disable red ability when blue is active

            other.gameObject.SetActive(false);
            StartCoroutine(Pill(other.gameObject));
        }
    }

    void DeactivateBlockTriggers(string blockTag)
    {
        GameObject[] blocks = GameObject.FindGameObjectsWithTag(blockTag);
        foreach (GameObject block in blocks)
        {
            Collider triggerCollider = block.GetComponent<Collider>();
            if (triggerCollider != null)
            {
                triggerCollider.isTrigger = true; // Deactivate triggers for blocks
            }
        }
    }

    void ReactivateBlockTriggers(string blockTag)
    {
        GameObject[] blocks = GameObject.FindGameObjectsWithTag(blockTag);
        foreach (GameObject block in blocks)
        {
            Collider triggerCollider = block.GetComponent<Collider>();
            if (triggerCollider != null)
            {
                triggerCollider.isTrigger = false; // Reactivate triggers for blocks
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("RedBlock") && hasRedAbility)
        {
            hasRedAbility = false;
            ReactivateBlockTriggers("RedBlock");
            // other.isTrigger = false; // Deactivate trigger for the block
        }
        if (other.CompareTag("BlueBlock") && hasBlueAbility)
        {
            hasBlueAbility = false;
            ReactivateBlockTriggers("BlueBlock");
        }
    }

    IEnumerator Pill(GameObject pill)
    {
        yield return new WaitForSeconds(pillTimer);
        pill.SetActive(true);    
           
    }
}
