using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    private GameObject player;
    private GameObject mainCamera;
    private GameObject buildingsCamera;
    private GameObject cloudsCamera;

    private Transform mainCameraTransform;
    private Transform buildingsCameraTransform;
    private Transform cloudsCameraTransform;

    private float buildingsParallaxScale;
    private float cloudsParallaxScale;

    private void Start()
    {
        player = PlayerChooser.ChoosePlayer();
        mainCamera = player.transform.GetChild(0).gameObject;
        buildingsCamera = player.transform.GetChild(1).gameObject;
        cloudsCamera = player.transform.GetChild(2).gameObject;

        mainCameraTransform = mainCamera.transform;
        buildingsCameraTransform = buildingsCamera.transform;
        cloudsCameraTransform = cloudsCamera.transform;

        buildingsParallaxScale = 0.7f;
        cloudsParallaxScale = 0.4f;
    }

    private void FixedUpdate()
    {
        buildingsCameraTransform.position = mainCameraTransform.position * buildingsParallaxScale;
        cloudsCameraTransform.position = mainCameraTransform.position * cloudsParallaxScale;
    }
}