using UnityEngine;
using System.Collections;

public class LoadScene : MonoBehaviour
{

    public void Load_Scene(int scene)
    {
        Application.LoadLevel(scene);
    }
}