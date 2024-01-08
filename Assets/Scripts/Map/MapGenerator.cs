using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;
using UnityEngine.InputSystem;
using FubarOps.Core;

namespace FubarOps.Map
{
    public class MapGenerator : MonoBehaviour
    {
        [SerializeField] SpriteShapeController a;
        [SerializeField] Camera mapGenerationCamera;
        [SerializeField] RenderTexture mapRenderTexture;
        [SerializeField] GameObject terrainParentObject;

        void Start()
        {
            mapGenerationCamera.orthographicSize = (Mathf.Max(GameController.Instance.mapSize.x, GameController.Instance.mapSize.y)) / 2;
            mapGenerationCamera.transform.position = new Vector3(GameController.Instance.mapSize.x / 2, GameController.Instance.mapSize.y / 2, -10);
        }

    #if UNITY_EDITOR

        void Update()
        {


            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {

                Texture2D mapTexture = new Texture2D(mapRenderTexture.width, mapRenderTexture.width);

                RenderTexture.active = mapRenderTexture;
                mapTexture.ReadPixels(new Rect(0, 0, mapRenderTexture.width, mapRenderTexture.height), 0, 0);

                mapTexture.Apply();

                System.IO.File.WriteAllBytes($"{Application.dataPath}/MapGeneration/{SceneManager.GetActiveScene().name}.png", mapTexture.EncodeToPNG());


                UnityEditor.AssetDatabase.Refresh();

            }

            if (Keyboard.current.gKey.wasPressedThisFrame)
            {
                foreach (MapMarker mapMarker in terrainParentObject.GetComponentsInChildren<MapMarker>())
                {
                    GameObject createdMarker = Instantiate(mapMarker.gameObject, transform.GetChild(0));
                    createdMarker.GetComponent<SpriteShapeController>().spriteShape = mapMarker.spriteShape;
                    createdMarker.layer = 7;
                }
            }
        }

    #endif
    }
}
