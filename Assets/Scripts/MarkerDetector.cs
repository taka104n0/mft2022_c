using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.ArucoModule;
using OpenCVForUnity.UnityUtils;

public class MarkerDetector : MonoBehaviour
{
    WebCamTexture webCamTexture;
    WebCamDevice[] webCamDevice;
    int selectCamera = 0;
    Mat srcMat;
    Texture2D dstTexture;
    float alpha = 0.5f;

    Dictionary dictionary;
    int dictionaryId = Aruco.DICT_4X4_50;
    Mat ids;
    List<Mat> corners;
    public string[] markerCorners;
    public string rawMarkerCorners;
    
    // Start is called before the first frame update
    void Start()
    {   
        RawImage rawImage;
        // カメラ設定
        webCamTexture = new WebCamTexture();
        webCamDevice = WebCamTexture.devices;
        webCamTexture = new WebCamTexture(webCamDevice[selectCamera].name, 1920, 1080);
        rawImage = GetComponent<RawImage>();
        rawImage.texture = webCamTexture;
        rawImage.color = new Color(rawImage.color.r, rawImage.color.g, rawImage.color.b, alpha);;
        webCamTexture.Play();

        dictionary = Aruco.getPredefinedDictionary((int)dictionaryId);  // ArUcoの辞書を設定
        ids = new Mat();
        corners = new List<Mat>();

        markerCorners = new string[]{"-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1"};
    }

    // Update is called once per frame
    void Update()
    {   
        // Webカメラ接続完了前は処理をしない
        if (this.webCamTexture.width <= 16 || this.webCamTexture.height <= 16) return;
        
        // 初期化
        if (this.srcMat == null)
        {
            this.srcMat = new Mat(this.webCamTexture.height, this.webCamTexture.width, CvType.CV_8UC3);
            this.dstTexture = new Texture2D(this.srcMat.cols(), this.srcMat.rows(), TextureFormat.RGBA32, false);
        }

        Utils.webCamTextureToMat(this.webCamTexture, this.srcMat);

        Aruco.detectMarkers(this.srcMat, this.dictionary, this.corners, this.ids);  // マーカー検出
        if (this.ids.total() > 0)
        {
            //Aruco.drawDetectedMarkers(this.srcMat, this.corners, this.ids);  // マーカーの描画
            rawMarkerCorners = this.corners[0].dump();  // マーカーの4隅の座標
            rawMarkerCorners = rawMarkerCorners.Substring(1, rawMarkerCorners.Length-2);
            markerCorners = rawMarkerCorners.Split(',');
            for (int i=0; i<markerCorners.Length; i++)
            {
                string crd = markerCorners[i];
                if (crd.IndexOf(" ") == 0)
                {
                    crd = crd.Substring(1, crd.Length-1);
                    markerCorners[i] = crd;
                }
            }
            //Debug.Log(markerCorners);
        } 
        else
        {
            markerCorners = new string[]{"-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1"};
        }
        
        //Utils.matToTexture2D(this.srcMat, this.dstTexture);
        //GetComponent<RawImage>().texture = this.dstTexture;
    }

    
    private void OnDestroy()
    {
        this.webCamTexture.Stop();
        WebCamTexture.Destroy(this.webCamTexture);
        this.srcMat.Dispose();
        Texture2D.Destroy(this.dstTexture);
    }
    
}
