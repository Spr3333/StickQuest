using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbility : MonoBehaviour
{
    bool hasRedAbility = false;
    bool hasBlueAbility = false;
    public float pillTimer = 5;

    public GameObject redBlock;
    public GameObject blueBlock;
    private Material orignalRed;
    private Material orignalBlue;
    public Material redReplacer;
    public Material blueReplacer;


    private void Start()
    {
        orignalRed = redBlock.GetComponent<MeshRenderer>().sharedMaterial;
        orignalBlue = blueBlock.GetComponent<MeshRenderer>().sharedMaterial;
    }
    private void Update()
    {
        if (hasRedAbility)
        {
            MaterialBlocks("RedBlock" , redReplacer);
        }
        else
        {
            MaterialBlocks("RedBlock" , orignalRed);
        }
        if (hasBlueAbility)
        {
            MaterialBlocks("BlueBlock" , blueReplacer);
        }
        else
        {
            MaterialBlocks("BlueBlock" , orignalBlue);
        }
    }

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
    void MaterialBlocks(string blockTag , Material blockmaterial)
    {
        GameObject[] blocks = GameObject.FindGameObjectsWithTag(blockTag);
        foreach (GameObject block in blocks)
        {
            block.GetComponent<MeshRenderer>().material = blockmaterial;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("RedBlock") && hasRedAbility)
        {
            hasRedAbility = false;
            ReactivateBlockTriggers("RedBlock");


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
