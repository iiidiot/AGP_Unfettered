using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneManagerUnfettered
{
    public static string nextSceneName;
    public static string[] sceneList =
    {
            "Map2_1",
            "Map1_1",

    };
    public static int nextSceneID = 0;
}

public class LoadSceneController : MonoBehaviour
{

    public Slider loadingSlider;

    public Text loadingText;

    private float loadingSpeed = 1;

    private float targetValue;

    private AsyncOperation operation;

    void Start()
    {
        SceneManagerUnfettered.nextSceneID++;

        loadingSlider.value = 0.0f;

        if (SceneManager.GetActiveScene().name == "Loading")
        {
            //启动协程    
            StartCoroutine(AsyncLoading());
        }
    }



    IEnumerator AsyncLoading()
    {
        operation = SceneManager.LoadSceneAsync(SceneManagerUnfettered.sceneList[SceneManagerUnfettered.nextSceneID - 1]);


        //阻止当加载完成自动切换    
        operation.allowSceneActivation = false;

        yield return operation;
    }


    // Update is called once per frame  
    void Update()
    {
        targetValue = operation.progress;

        if (operation.progress >= 0.9f)
        {
            //operation.progress的值最大为0.9  
            targetValue = 1.0f;
        }

        if (targetValue != loadingSlider.value)
        {
            //插值运算  
            loadingSlider.value = Mathf.Lerp(loadingSlider.value, targetValue, Time.deltaTime * loadingSpeed);
            if (Mathf.Abs(loadingSlider.value - targetValue) < 0.01f)
            {
                loadingSlider.value = targetValue;
            }
        }

        loadingText.text = ((int)(loadingSlider.value * 100)).ToString() + "%";

        if ((int)(loadingSlider.value * 100) == 100)
        {

            //允许异步加载完毕后自动切换场景  
            operation.allowSceneActivation = true;
        }
    }
}
