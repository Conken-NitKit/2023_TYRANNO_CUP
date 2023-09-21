
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFadeManager : MonoBehaviour
{
    //フェードアウト処理の開始、完了を管理するフラグ
    private bool isFadeOut = false;

    //フェードイン処理の開始、完了を管理するフラグ
    private bool isFadeIn = true;

    //透明度が変わるスピード
    float fadeSpeed = 0.65f;
    
    public Image fadeImage;
    float red, green, blue, alfa, time;

    //シーン遷移のための型
    string afterScene;

    // Start is called before the first frame update
    void Start()
    {
        //シーンによってfadeSpeedのスピードを変化
        var scene = SceneManager.GetActiveScene ();
        if (scene.name == "Title") fadeSpeed = 2f;

        SetRGBA(0, 0, 0, 1);
        //シーン遷移が完了した際にフェードインを開始するように設定
        SceneManager.sceneLoaded += fadeInStart;
    }
    //シーン遷移が完了した際にフェードインを開始するように設定
    void fadeInStart(Scene scene,LoadSceneMode mode)
    {
        isFadeIn = true;
    }
    
    public void fadeOutStart(int red,int green,int blue,int alfa,string nextScene)
    {
        SetRGBA(red, green, blue, alfa);
        SetColor();
        isFadeOut = true;
        afterScene = nextScene;
    }

    void Update()
    {
        if (isFadeIn == true)
        {
            //不透明度を徐々に下げる
            time += Time.deltaTime;
            alfa -= fadeSpeed * Time.deltaTime * time * time * time;
            //変更した透明度を画像に反映させる関数を呼ぶ
            SetColor();
            if (alfa <= 0)
            {
                isFadeIn = false;
                time = 0;
            }
        }
        if (isFadeOut == true)
        {
            //不透明度を徐々に上げる
            time += Time.deltaTime;
            alfa += fadeSpeed * Time.deltaTime * time * time * time;
            //変更した透明度を画像に反映させる関数を呼ぶ
            SetColor();
            if (alfa >= 1)
            {
                isFadeOut = false;
                SceneManager.LoadScene(afterScene);
            }
        }
    }
    //画像に色を代入する関数
    void SetColor()
    {
        fadeImage.color = new Color(red, green, blue, alfa);
    }
    //色の値を設定するための関数
    public void SetRGBA(int r, int g, int b, int a)
    {
        red = r;
        green = g;
        blue = b;
        alfa = a;
    }
}
