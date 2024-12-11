using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ShareOnTwitter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    public Texture2D textureToShare;
    public string textToShare = "Check out this cool image!";

    //public void ShareOnAndroid()
    //{
    //    // Save texture as a temporary file
    //    string filePath = Path.Combine(Application.temporaryCachePath, "sharedImage.png");
    //    File.WriteAllBytes(filePath, textureToShare.EncodeToPNG());

    //    // Share text and image
    //    new NativeShare()
    //        .AddFile(filePath)
    //        .SetText(textToShare)
    //        .SetSubject("Subject for the share")
    //        .SetTitle("Title for the share")
    //        .SetCallback((result, shareTarget) =>
    //        {
    //            Debug.Log($"Share result: {result}, Target: {shareTarget}");
    //        })
    //        .Share();

    //    Debug.Log("Sharing initiated...");
    //}
    [System.Obsolete]
    public void Share()
    {
        string base64Image = WebGLImageUtils.TextureToBase64(textureToShare);

        // For demonstration: Log the Base64 string (you need a hosting service)
        Debug.Log(base64Image);

        // Use an external service to upload the image and get its URL
        string imageUrl = "http://kryzer.fitnessinfo.uk/wp-content/uploads/2024/12/1-Picture1.png"; // Replace with real hosted image URL

#if UNITY_WEBGL
        Application.ExternalCall("ShareOnTwitter", textToShare, imageUrl);
#endif
    }
}
