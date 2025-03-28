using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class MaterialInstanceController : MonoBehaviour
{
    //TODO:
    //Ver como posso melhor e otimizar essa classe, possivelmente encurtando ela também
    
    #region Inspector
    [Header("References"), Space(6)]
    [SerializeField] private Material material;

    [Header("Variables"), Space(6)]
    [SerializeField] private MaterialRendererEnum materialRenderer;
    [SerializeField] private MaterialWhereToGetEnum materialWhereToGet;
    #endregion
    
    #region Public Variables
    [HideInInspector] public Material materialInstance;
    [HideInInspector] public bool hasCreatedInstance;
    #endregion

    #region Default Functions
    private void Awake()
    {
        hasCreatedInstance = false;

        materialInstance = new Material(material);

        hasCreatedInstance = true;

        switch (materialRenderer)
        {
            case MaterialRendererEnum.meshRenderer:
                CreateInstance<MeshRenderer>();
                break;
            case MaterialRendererEnum.skinnedMeshRenderer:
                CreateInstance<SkinnedMeshRenderer>();
                break;
            case MaterialRendererEnum.decalProjector:
                CreateInstance<DecalProjector>();
                break;
            case MaterialRendererEnum.spriteRenderer:
                CreateInstance<SpriteRenderer>();
                break;
        }
    }
    #endregion

    #region Custom Functions
    private void CreateInstance<T>()
    {
        T[] allGenerics = null;

        switch (materialWhereToGet)
        {
            case MaterialWhereToGetEnum.parent:
                allGenerics = GetComponentsInParent<T>();
                break;
            case MaterialWhereToGetEnum.self:
                allGenerics = GetComponents<T>();
                break;
            case MaterialWhereToGetEnum.children:
                allGenerics = GetComponentsInChildren<T>();
                break;
        }

        Type compare = typeof(T);

        if (compare == typeof(MeshRenderer))
        {
            MeshRenderer[] allMeshes = (MeshRenderer[])Convert.ChangeType(allGenerics, typeof(MeshRenderer[]));
            List<MeshRenderer> specificMeshes = new List<MeshRenderer>();

            foreach (MeshRenderer mesh in allMeshes)
            {
                if (mesh.sharedMaterial == material)
                {
                    specificMeshes.Add(mesh);
                }
            }

            foreach (MeshRenderer mesh in specificMeshes)
            {
                mesh.material = materialInstance;
            }
        }
        else if (compare == typeof(SkinnedMeshRenderer))
        {
            SkinnedMeshRenderer[] allSkinnedMeshes = (SkinnedMeshRenderer[])Convert.ChangeType(allGenerics, typeof(SkinnedMeshRenderer[]));
            List<SkinnedMeshRenderer> specificSkinnedMeshes = new List<SkinnedMeshRenderer>();

            foreach (SkinnedMeshRenderer skinnedMesh in allSkinnedMeshes)
            {
                if (skinnedMesh.sharedMaterial == material)
                {
                    specificSkinnedMeshes.Add(skinnedMesh);
                }
            }

            foreach (SkinnedMeshRenderer skinnedMesh in specificSkinnedMeshes)
            {
                skinnedMesh.material = materialInstance;
            }
        }
        else if (compare == typeof(DecalProjector))
        {
            DecalProjector[] allDecalProjectors = (DecalProjector[])Convert.ChangeType(allGenerics, typeof(DecalProjector[]));
            List<DecalProjector> specificDecalProjectors = new List<DecalProjector>();

            foreach (DecalProjector decalProjector in allDecalProjectors)
            {
                if (decalProjector.material == material)
                {
                    specificDecalProjectors.Add(decalProjector);
                }
            }

            foreach (DecalProjector decalProjector in specificDecalProjectors)
            {
                decalProjector.material = materialInstance;
            }
        }
        else if (compare == typeof(SpriteRenderer))
        {
            SpriteRenderer[] allSpriteRenderers = (SpriteRenderer[])Convert.ChangeType(allGenerics, typeof(SpriteRenderer[]));
            List<SpriteRenderer> specificSpriteRenderers = new List<SpriteRenderer>();

            foreach (SpriteRenderer spriteRenderer in allSpriteRenderers)
            {
                if (spriteRenderer.sharedMaterial == material)
                {
                    specificSpriteRenderers.Add(spriteRenderer);
                }
            }

            foreach (SpriteRenderer spriteRenderer in specificSpriteRenderers)
            {
                spriteRenderer.material = materialInstance;
            }
        }
    }
    #endregion
}