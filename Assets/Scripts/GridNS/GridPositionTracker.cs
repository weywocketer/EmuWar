using System.Collections.Generic;
using UnityEngine;
using FubarOps.Core;
using FubarOps.Constants;

namespace FubarOps.GridNS
{
    /// <summary>
    /// Keeps track of the unit's position in the grid used for spatial partitioning.
    /// </summary>
    public class GridPositionTracker : MonoBehaviour
    {
        public List<GameObject>[,] gridUsedByThisBoid;
        public Side side;
        int gridPositionX;
        int gridPositionY;

        void Start()
        {
            // Get a reference to an apropriate array from GameController.
            switch (side)
            {
                case Side.soldiers:
                    gridUsedByThisBoid = GameController.Instance.soldiersGrid;
                    break;
                case Side.emus:
                    gridUsedByThisBoid = GameController.Instance.emusGrid;
                    break;
            }


            gridPositionX = (int)(transform.position.x / GameController.Instance.gridSpacing);
            gridPositionY = (int)(transform.position.y / GameController.Instance.gridSpacing);


            gridUsedByThisBoid[gridPositionX, gridPositionY].Add(gameObject);

        }

        public void UpdateGridPosition()
        {
            int newGridPositionX = (int)(transform.position.x / GameController.Instance.gridSpacing);
            int newGridPositionY = (int)(transform.position.y / GameController.Instance.gridSpacing);

            if (newGridPositionX != gridPositionX || newGridPositionY != gridPositionY)
            {
                gridUsedByThisBoid[gridPositionX, gridPositionY].Remove(gameObject);

                gridPositionX = newGridPositionX;
                gridPositionY = newGridPositionY;

                gridUsedByThisBoid[gridPositionX, gridPositionY].Add(gameObject);
            }
        }

        public void RemoveBoidFromGrid()
        {
            gridUsedByThisBoid[gridPositionX, gridPositionY].Remove(gameObject);
        }


        public List<GameObject> GetListOfNearbyBoids(List<GameObject>[,] grid)
        {
            List<GameObject> boids = new();


            int firstXindex = Mathf.Max(gridPositionX - 1, 0);
            int lastXindex = Mathf.Min(gridPositionX + 1, grid.GetLength(0) - 1);

            int firstYindex = Mathf.Max(gridPositionY - 1, 0);
            int lastYindex = Mathf.Min(gridPositionY + 1, grid.GetLength(0) - 1);


            for (int j = firstYindex; j <= lastYindex; j++)
            {
                for (int i = firstXindex; i <= lastXindex; i++)
                {
                    boids.AddRange(grid[i, j]);
                }
            }

            return boids;
        }
    }
}