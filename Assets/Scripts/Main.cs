using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using toio;

public class Main : MonoBehaviour
{
    CubeManager cubeManager;
    const int cubeNum = 2;
    int markerX = -1;
    int markerY = -1;
    // Start is called before the first frame update
    async void Start()
    {
        cubeManager = new CubeManager(ConnectType.Real);
        await cubeManager.MultiConnect(cubeNum);
        
        /*
        foreach (var navi in cubeManager.navigators)
        {   
            navi.mode = CubeNavigator.Mode.AVOID;
        }
        */
        
    }

    // Update is called once per frame
    void Update()
    {
        MarkerDetector markerDetector;
        GameObject rawImage;
        string[] markerCorners;

        rawImage = GameObject.Find("RawImage");
        markerDetector = rawImage.GetComponent<MarkerDetector>();
        markerCorners = markerDetector.markerCorners;
        markerX = (int.Parse(markerCorners[4]) + int.Parse(markerCorners[0])) / 2;
        markerY = (int.Parse(markerCorners[5]) + int.Parse(markerCorners[1])) / 2;
        //Debug.Log(string.Join(",", markerCorners));
        //Debug.Log(markerY);

        if (markerX != -1 && markerY != -1)
        {
            markerX = (int)Map(markerX, 0, 1920, 34, 339);
            markerY = (int)Map(markerY, 0, 1080, 35, 250);
            //Debug.Log(markerY);

            /*
            foreach (var navi in cubeManager.syncNavigators)
            {
                //int cubeX = (int)navi.handle.x;
                //int cubeY = (int)navi.handle.y;
                navi.Navi2Target(markerX, markerY).Exec();
            }
            */
            
            /*
            foreach (var handle in cubeManager.syncHandles)
            {
                int cubeX = (int)handle.x;
                int cubeY = (int)handle.y;
                if (cubeX < markerX)
                {
                    if (cubeY < markerY)
                    {
                        handle.Move2Target(markerX-20, markerY-20).Exec();
                    } 
                    else 
                    {
                        handle.Move2Target(markerX-20, markerY+20).Exec();
                    }
                }
                else
                {
                    if (cubeY < markerY)
                    {
                        handle.Move2Target(markerX+20, markerY-20).Exec();
                    } 
                    else 
                    {
                        handle.Move2Target(markerX+20, markerY+20).Exec();
                    }
                }
            }
            */

            foreach (var navi in cubeManager.syncNavigators)
            {
                int cubeX = (int)navi.handle.x;
                int cubeY = (int)navi.handle.y;

                if (cubeX < markerX)
                {
                    if (cubeY < markerY)
                    {
                        navi.Navi2Target(markerX-10, markerY-10).Exec();
                    } 
                    else 
                    {
                        navi.Navi2Target(markerX-10, markerY+10).Exec();
                    }
                }
                else
                {
                    if (cubeY < markerY)
                    {
                        navi.Navi2Target(markerX+10, markerY-10).Exec();
                    } 
                    else 
                    {
                        navi.Navi2Target(markerX+10, markerY+10).Exec();
                    }
                }
            }
        } else 
        {
            foreach (var navi in cubeManager.syncNavigators) 
            {
                int move = Random.Range(0, 30);

                if (move < 1) {
                    navi.Navi2Target(Random.Range(44, 330), Random.Range(45, 241)).Exec();
                }
            }
        }
    }

    float Map(float value, float start1, float stop1, float start2, float stop2)
    {
        return start2 + (stop2 - start2) * ((value - start1) / (stop1 - start1));
    }
}
