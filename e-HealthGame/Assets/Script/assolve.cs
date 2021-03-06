using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class assolve : MonoBehaviour
{
    public Material[] dissolveMat;
    public float t = 0.0f;
    int x = 0;
    public GameObject player;
    public Shader shaderDissolve;
    public List<Material> allMaterials;

    public List<Shader> oldShadersList;
    /*public GameObject risposta1;
    public GameObject risposta2;
    public GameObject fuoco1;
    public GameObject fuoco2;*/
    private Color colMat;
    int fatto = 0;
    public GameObject GrassBlade;
    public GameObject GrassBlade2;

    ProceduralGrassRenderer prod;


    public void FixedUpdate()
    {
        if (x > 0 && fatto == 0)
        {
            for (int i = 0; i < allMaterials.Count; i++)
            {
                //dissolveMat[i].SetFloat("Dissolve_Value", Mathf.Lerp(0f, 1f, t));
                allMaterials[i].SetFloat("Dissolve_Value", Mathf.Lerp(1f, 0f, t));
                t += 0.011f * Time.fixedDeltaTime;
                //Debug.Log("Nel fixed update");
            }
            if (GrassBlade != null)
            {
                int n_child = GrassBlade.transform.childCount;
                for (int i = 0; i < n_child; i++)
                {
                    GameObject child = GrassBlade.transform.GetChild(i).gameObject;
                    child.GetComponent<ProceduralGrassRenderer>().instantiatedGrassComputeShader.SetFloat("_BladeWidth", Mathf.Lerp(0f, 0.5f, t));
                    child.GetComponent<ProceduralGrassRenderer>().instantiatedGrassComputeShader.SetFloat("_BladeHeight", Mathf.Lerp(0f, 2f, t));
                }
            }

            if (GrassBlade2 != null)
            {
                int n_child = GrassBlade2.transform.childCount;
                for (int i = 0; i < n_child; i++)
                {
                    GameObject child = GrassBlade2.transform.GetChild(i).gameObject;
                    child.GetComponent<ProceduralGrassRenderer>().instantiatedGrassComputeShader.SetFloat("_BladeWidth", Mathf.Lerp(0f, 0.5f, t));
                    child.GetComponent<ProceduralGrassRenderer>().instantiatedGrassComputeShader.SetFloat("_BladeHeight", Mathf.Lerp(0f, 2f, t));
                }
            }
        }

        if (t >= 1f && fatto == 0)
        {
            fatto = 1;
            reChange();
        }


    }

    private void Start()
    {
        GrassBlade = GameObject.Find("Grassblade");
        GrassBlade2 = GameObject.Find("Grassblade Giostre");

        if (PlayerPrefs.GetInt("Level", 0) > 0)
        {
            changeMaterial();
        }
        else
        {
            if (GrassBlade != null)
            {
                int n_child = GrassBlade.transform.childCount;
                for (int i = 0; i < n_child; i++)
                {
                    GameObject child = GrassBlade.transform.GetChild(i).gameObject;
                    child.GetComponent<ProceduralGrassRenderer>().instantiatedGrassComputeShader.SetFloat("_BladeWidth", 0.5f);
                    child.GetComponent<ProceduralGrassRenderer>().instantiatedGrassComputeShader.SetFloat("_BladeHeight", 2f);
                }
            }
            if (GrassBlade2 != null)
            {
                int n_child = GrassBlade2.transform.childCount;
                for (int i = 0; i < n_child; i++)
                {
                    GameObject child = GrassBlade2.transform.GetChild(i).gameObject;
                    child.GetComponent<ProceduralGrassRenderer>().instantiatedGrassComputeShader.SetFloat("_BladeWidth", 0.5f);
                    child.GetComponent<ProceduralGrassRenderer>().instantiatedGrassComputeShader.SetFloat("_BladeHeight", 2f);
                }
            }
        }


        
    }


    public void changeMaterial()
    {

        GameObject[] allObjects = Object.FindObjectsOfType<GameObject>();
        foreach (GameObject go in allObjects)
        {
            if (go.transform.name != "DOMANDA" && go.name != "Effetto" && go.name != "Effetto (1)" && go.name != "Parola" && go.name != "Immagine" && go.name != "textR1" && go.name != "textR2" && go.name != "AtomBallLines" && go.name != "AtomBallSpheres")
            {
                if (go.GetComponent<MeshRenderer>() != null)
                {
                    if (go.GetComponent<MeshRenderer>().materials != null)
                    {
                        Material[] materials = go.GetComponent<MeshRenderer>().materials;
                        foreach (Material mat in materials)
                        {
                            allMaterials.Add(mat);
                            if (mat.HasProperty("_Color"))
                            {
                                oldShadersList.Add(mat.shader);
                                colMat = mat.color;
                                mat.shader = shaderDissolve;
                                mat.SetColor("_Albedo", colMat);
                            }
                            else
                            {
                                //Debug.LogWarning("NO COLOR" + mat.name);
                                oldShadersList.Add(mat.shader);
                                mat.shader = shaderDissolve;
                                mat.SetColor("_Albedo", Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f));
                                if (go.name == "Plane")
                                {
                                    mat.shader = shaderDissolve;
                                    mat.SetColor("_Albedo", new Color(1f, 1f, 1f));
                                }
                                if (go.name == "Plane_sabbia")
                                {
                                    mat.shader = shaderDissolve;
                                    mat.SetColor("_Albedo", new Color(0.6415094f, 0.5206518f, 0.3056248f));
                                }

                            }

                        }
                    }
                }
            }
        }
        x++;
    }

    public void reChange()
    {
        GameObject[] allObjects = Object.FindObjectsOfType<GameObject>();
        int i = 0;
        foreach (GameObject go in allObjects)
        {
            if (go.transform.name != "DOMANDA" && go.name != "Effetto" && go.name != "Effetto (1)" && go.name != "Parola" && go.name != "Immagine" && go.name != "textR1" && go.name != "textR2" && go.name != "AtomBallLines" && go.name != "AtomBallSpheres")
            {
                if (go.GetComponent<MeshRenderer>() != null)
                {
                    if (go.GetComponent<MeshRenderer>().materials != null)
                    {
                        Material[] materials = go.GetComponent<MeshRenderer>().materials;
                        foreach (Material mat in materials)
                        {
                            if (i<= oldShadersList.Count)
                            {
                                mat.shader = oldShadersList[i];
                                i++;
                            }
                        }
                    }
                }
            }
        }
        x++;



    }




}
