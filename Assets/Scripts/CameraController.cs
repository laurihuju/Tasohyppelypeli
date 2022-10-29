using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float cameraSmoothness;

    [SerializeField] private Transform playerTransform;

    private CameraBoundaries boundaries;

    private Transform myTransform;
    private Camera myCamera;

    void Start()
    {
        myTransform = GetComponent<Transform>();
        myCamera = GetComponent<Camera>();
        if(boundaries == null)
        {
            SetCameraBoundaries(LevelLoader.instance.GetActiveLevel().GetCameraBoundaries());
        }
    }

    void FixedUpdate()
    {
        //Otetaan talteen kameran kohdesijainti
        Vector3 targetPosition = playerTransform.position;
        targetPosition.z = -10;

        //Korjataan kameran kohdesijaintia, jos sijainti on kameran rajojen ulkopuolella
        targetPosition = CorrectPositionIfOutsideBoundaries(targetPosition);

        //Lasketaan kameran uusi sijainti niin, että kamera liikkuu tasaisesti
        Vector3 smoothPosition = Vector3.Lerp(new Vector3(myTransform.position.x, myTransform.position.y, -10), targetPosition, cameraSmoothness);

        //Korjataan kameran uutta sijaintia, jos sijainti on kameran rajojen ulkopuolella
        smoothPosition = CorrectPositionIfOutsideBoundaries(smoothPosition);

        //Lasketaan kameran uusi sijainti ja asetetaan se kameran sijainniksi
        myTransform.position = smoothPosition;
    }

    public void SetCameraBoundaries(CameraBoundaries boundaries)
    {
        this.boundaries = boundaries;
    }

    private Vector3 CorrectPositionIfOutsideBoundaries(Vector3 position)
    {
        //Lasketaan kameran leveys ja korkeus
        float cameraOrthographicHeight = myCamera.orthographicSize;
        float cameraOrthographicWidth = cameraOrthographicHeight * myCamera.aspect;

        //Jos kameran kohdesijainti menee vasemman rajan yli, estetään rajan ylittyminen
        if (position.x - cameraOrthographicWidth < boundaries.GetLeftBoundary().position.x)
        {
            position.x = boundaries.GetLeftBoundary().position.x + cameraOrthographicWidth;
        }

        //Jos kameran kohdesijainti menee oikean rajan yli, estetään rajan ylittyminen
        if (position.x + cameraOrthographicWidth > boundaries.GetRightBoundary().position.x)
        {
            position.x = boundaries.GetRightBoundary().position.x - cameraOrthographicWidth;
        }

        //Jos kameran kohdesijainti menee ylärajan yli, estetään rajan ylittyminen
        if (position.y + cameraOrthographicHeight > boundaries.GetTopBoundary().position.y)
        {
            position.y = boundaries.GetTopBoundary().position.y - cameraOrthographicHeight;
        }

        //Jos kameran kohdesijainti menee alarajan yli, estetään rajan ylittyminen
        if (position.y - cameraOrthographicHeight < boundaries.GetBottomBoundary().position.y)
        {
            position.y = boundaries.GetBottomBoundary().position.y + cameraOrthographicHeight;
        }

        return position;
    }
}
