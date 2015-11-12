﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[ExecuteInEditMode]
class DetourStarter : MonoBehaviour
{
    public enum RenderMode { POLYS, DETAIL_POLYS, TILE_POLYS }

    public Material material;
    public PolyMeshAsset polymesh;
    public TileCacheAsset navmeshData;
    public RenderMode Mode;

    private DbgRenderMesh mesh = new DbgRenderMesh();

    public void OnEnable()
    {
        Detour.get.Initialize(navmeshData);

        if (Application.isPlaying)
        {
            Destroy(this);
        }
        else
        {
            switch (Mode)
            {
                case RenderMode.POLYS:
                    RecastDebug.ShowRecastNavmesh(mesh, polymesh.PolyMesh, polymesh.config);
                    break;

                case RenderMode.DETAIL_POLYS:
                    RecastDebug.ShowRecastDetailMesh(mesh, polymesh.PolyDetailMesh);
                    break;

                case RenderMode.TILE_POLYS:
                    for (int i = 0; i < navmeshData.header.numTiles; ++i)
                        RecastDebug.ShowTilePolyDetails(mesh, Detour.get.NavMesh, i);
                    break;
            }

            mesh.CreateGameObjects("RecastRenderer", material);
            mesh.Rebuild();
        }
    }

    public void OnDestroy()
    {
        mesh.DestroyGameObjects();
    }

    public void OnDisable()
    {
        mesh.DestroyGameObjects();
    }
}
