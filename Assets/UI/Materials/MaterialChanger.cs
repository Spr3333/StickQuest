using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialChanger : MonoBehaviour
{
    public GameObject redCube;
    public GameObject blueCube;
    public Material redReplacer;
    public Material BlueReplacer;
    private Material redOrignal;
    private Material BlueOriginal;
    public bool Redability = false;
    public bool Blueability = false;
    // Start is called before the first frame update
    void Start()
    {
        redOrignal= redCube.GetComponent<MeshRenderer>().material;
        BlueOriginal=blueCube.GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if(Redability)
        {
            MaterialChange(redCube, redReplacer);
        }
        else
        {
            MaterialChange(redCube, redOrignal);
        }
        if (Blueability)
        {
            MaterialChange(blueCube, BlueReplacer);
        }
        else
        {
            MaterialChange(blueCube, BlueOriginal);
        }

    }
    public void MaterialChange(GameObject cube,Material blockmaterial)
    {
        cube.GetComponent<MeshRenderer>().material = blockmaterial;
    }
}
