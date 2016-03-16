using UnityEngine;
using System.Collections;

public class LoadSceneScript : MonoBehaviour
{
    public void LoadScene (int level)
    {
        Application.LoadLevel(level);
    }
}