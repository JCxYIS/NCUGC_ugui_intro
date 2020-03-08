using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * 抱歉這個code是禮拜天晚上在火車上打的
 * 所以超級破爛
 * 不要嘗試去讀他
 * 
 * 各位對不起~~~~
 * by Google哥 20200308 2100
 */

public class SlideManager : MonoBehaviour
{
    [SerializeField] Transform inSlideTemplate;
    [SerializeField] Transform outSlideTemplate;
    [SerializeField] Animator controlAnim;
    [SerializeField] Transform slidesContainer;

    Transform inSlide, outSlide;
    Vector3 originalPos;
    List<Transform> slides = new List<Transform>();
    int currentSlide = -1;


    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < slidesContainer.childCount; i++)
        {
            slides.Add(slidesContainer.GetChild(i));
        }
        NextSlide();
    }

    // Update is called once per frame
    void Update()
    {       
        // ani
        if(outSlide && controlAnim.GetCurrentAnimatorStateInfo(0).IsName("end"))
        {
            outSlide.SetParent(slidesContainer);
            outSlide.localScale = Vector3.one;
            outSlide.transform.position = originalPos;
            outSlide = null;
        }
        else
        {
            inSlide.position = inSlideTemplate.position;
        }

        // kb
        if (Input.GetKeyDown(KeyCode.RightArrow))
            NextSlide(true);
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            NextSlide(false);

    }

    public void NextSlide(bool isNext = true)
    {
        if (controlAnim.GetCurrentAnimatorStateInfo(0).IsName("in"))
        {
            Debug.Log("Anim not ended");
            return;
        }

        if (isNext)
            currentSlide++;
        else
            currentSlide--;

        if (currentSlide < 0)
            currentSlide = 0;
        else if (currentSlide >= slidesContainer.childCount)
            currentSlide = slidesContainer.childCount - 1;
        else
        {
            Debug.Log($"next slide: {currentSlide}");

            outSlide = inSlide;
            inSlide = slides[currentSlide];
            
            outSlide?.SetParent(outSlideTemplate);
            inSlide.SetParent(inSlideTemplate);

            originalPos = inSlide.transform.position;
            controlAnim.Play("in");
        }

    }
}
