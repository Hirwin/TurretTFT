using CodeMonkey.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using Unity.Mathematics;
using UnityEngine;

public class GridBuildingSystem : MonoBehaviour
{
    //test
    private GridXZ<GridObject> grid;
    [SerializeField] private int gridWidth, gridHeight;
    [SerializeField] private float cellSize;
    [SerializeField] private Transform testTransform;
    [SerializeField] private NavMeshSurface navMesh;

    private void Awake() {
        grid = new GridXZ<GridObject>(gridWidth, gridHeight, cellSize, Vector3.zero, (GridXZ<GridObject> g, int x, int z) => new GridObject(g, x, z));
    }

    public class GridObject {
        private GridXZ<GridObject> grid;
        private Transform transform;
        private int x;
        private int z;
        public GridObject(GridXZ<GridObject> grid, int x, int z) {
            this.grid = grid;
            this.x = x;
            this.z = z;
        }

        public void SetTransform(Transform transform) {
            this.transform = transform;
            grid.TriggerGridObjectChanged(x, z);
        }

        public bool CanBuild() {
            return transform == null;
        }

        public void ClearTransform() {
            transform = null;
        }

        public override string ToString() {
            return x + ", " + z + "\n" + transform;
        }
    }

    public void ChangeTurretType(Transform turret) {
        testTransform = turret;
    }

    private bool InGridCheck(int x, int z) {
        if (x == -1 || z == -1) {
            return false;  
        }
        return true;
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            grid.GetXZ(Mouse3D.GetMouseWorldPosition(), out int x, out int z);
            if (InGridCheck(x, z)) {
                GridObject gridObject = grid.GetGridObject(x, z);
                if (gridObject.CanBuild()) {
                    Transform builtTransform = Instantiate(testTransform, grid.GetWorldPosition(x, z), Quaternion.identity);
                    gridObject.SetTransform(builtTransform);
                    //navMesh.BuildNavMesh();
                } else {
                    UtilsClass.CreateWorldTextPopup("Cannot Build here!", Mouse3D.GetMouseWorldPosition());
                }
            }
        }
    }



}
