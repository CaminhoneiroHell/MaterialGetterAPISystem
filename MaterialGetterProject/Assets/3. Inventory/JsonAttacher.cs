using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonAttacher : MonoBehaviour
{
    public MaterialList materialList = new MaterialList();
   
    private void Start()
    {
        TextAsset targetFile = Resources.Load("data") as TextAsset;
        if (targetFile != null)
        {
            materialList = JsonUtility.FromJson<MaterialList>(targetFile.text);
            foreach(MaterialsDTO matDTO in materialList.materialsDTOs)
            {
                print(matDTO.id);
                print(matDTO.tilingX);
                print(matDTO.materialId);
                print(matDTO.iconUrl);
            }
        }
        else
        {
            print("Is Null");
        }

    }
    
}
