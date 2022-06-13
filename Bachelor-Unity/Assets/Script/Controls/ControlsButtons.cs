using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlsButtons : MonoBehaviour
{
    DataBase db = DataBase.getInstance();

    public void BackToOptionsButton()
    /* Changing scene from point cloud to options menu.
    */
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        //fromPoints boolean, so we can check if we came from the point cloud
        db.setFromPoints(true);
    }

}
