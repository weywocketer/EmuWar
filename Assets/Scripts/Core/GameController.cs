using Cinemachine;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using FubarOps.Movement;
using FubarOps.Constants;

namespace FubarOps.Core
{
    public class GameController : MonoBehaviour
    {
        public static GameController Instance { get; private set; }

        public Vector2 mapSize = new(2000, 2000);
        public GameObject playerControlledSoldier;
        public float gridSpacing = 100;
        public float borderMargin = 5;
        [SerializeField] CinemachineVirtualCamera virtualCameraBinoculars;

        [SerializeField] GameObject binocularsOverlay; // Temporary, should be moved to some UI class.
        public List<GameObject> obstacles = new List<GameObject>();

        public List<GameObject>[,] soldiersGrid;
        public List<GameObject>[,] emusGrid;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Debug.LogError("GameController already exists!");
            }

            // Map dimensions should be divisible by gridSpacing, for the grid to cover the whole map.
            int numberOfRows = (int)(mapSize.x / gridSpacing);
            int numberOfColumns = (int)(mapSize.y / gridSpacing);

            soldiersGrid = new List<GameObject>[numberOfRows, numberOfColumns];
            emusGrid = new List<GameObject>[numberOfRows, numberOfColumns];

            for (int j = 0; j < soldiersGrid.GetLength(1); j++)
            {
                for (int i = 0; i < soldiersGrid.GetLength(0); i++)
                {
                    soldiersGrid[i, j] = new List<GameObject>();
                    emusGrid[i, j] = new List<GameObject>();
                }
            }
        }


        void Start()
        {
            obstacles.AddRange(GameObject.FindGameObjectsWithTag("Obstacle"));

            playerControlledSoldier.GetComponent<ManualSteering>().OnPlayerModeSwitched += ToggleBinocularsOverlay;
        }

        // Temporary, should be moved to some UI class.
        void ToggleBinocularsOverlay(PlayerMode playerMode)
        {
            if (playerMode == PlayerMode.usingBinoculars)
            {
                binocularsOverlay.SetActive(true);
            }
            else
            {
                binocularsOverlay.SetActive(false);
            }
        }

        void Update()
        {
            if (Keyboard.current.gKey.wasPressedThisFrame)
            {
                string gridDebugMessage = string.Empty;
                for (int j = emusGrid.GetLength(1) - 1; j >= 0; j--)
                {
                    for (int i = 0; i < emusGrid.GetLength(0); i++)
                    {
                        gridDebugMessage += $"{emusGrid[i, j].Count:D2} ";
                    }

                    gridDebugMessage += "\n";
                }

                print(gridDebugMessage);
            }


            // Temporary time scale changing, for testing purposes.
            if (Keyboard.current.numpadPlusKey.wasPressedThisFrame)
            {
                Time.timeScale += 1;
                print(Time.timeScale);
            }

            if (Keyboard.current.numpadMinusKey.wasPressedThisFrame)
            {
                Time.timeScale -= 1;
                print(Time.timeScale);
            }
        }

    }
}