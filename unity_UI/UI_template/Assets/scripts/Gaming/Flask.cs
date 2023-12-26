using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Text;

public class Flask 
{
    private static string urlSet(string func)
    {
        //string url = "http://140.122.185.169:5050/api/";
        //string url = "http://172.23.1.9:5000/api/";
        string url = "http://192.168.1.108:5000/api/";
        //string url = "http://127.0.0.1:5000/api/";
        return url + func;
    }
    public static IEnumerator SendRequest(string json, string func)
    {   

        string url = urlSet(func);
        Debug.Log(url);
        UnityWebRequest request = UnityWebRequest.PostWwwForm(url, "POST");
        //request.timeout = 10;
        
        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        
        yield return request.SendWebRequest();
        
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("ConErr - " + request.result + ":" + request.error);
            yield return request.result;
        }
        else
        {
            Debug.Log("Response: " + request.result);
            yield return request.downloadHandler.text;
        }
        
    }
}
