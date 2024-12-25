using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeFirstScene : MonoBehaviour
{
    private void Awake()
    {
        SceneSwapper.GoToBoatScene();
    }
}
