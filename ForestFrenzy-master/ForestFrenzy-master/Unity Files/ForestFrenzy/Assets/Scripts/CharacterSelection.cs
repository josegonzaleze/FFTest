using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour {

    public int ImageIndex;

    //public void SelectImage(int index)
    //{
    //    ImageIndex = index;
    //}




    //public int currentIndex;
    //private int maxIndex = 2;
    //public int newIndex;
    //private int minIndex = 1;

    //Image myImage;
    //Sprite mySprite;

    //public void IncrementImageList()
    //{
    //    if(currentIndex == maxIndex)
    //    {
    //        newIndex = minIndex;
    //    }
    //    else
    //    {
    //        newIndex = ++currentIndex;
    //    }
    //    setCharacter();
    //}

    //public void DecrementImageList()
    //{
    //    if (currentIndex == minIndex)
    //    {
    //        newIndex = maxIndex;
    //    }
    //    else
    //    {
    //        newIndex = --currentIndex;
    //    }
    //    setCharacter();
    //}

    //private void setCharacter()
    //{
       
    //    switch (newIndex)
    //    {
    //        case 0:
    //            // myImage = Resources.Load("Icon1") as Image;
    //            var objWithImage = GameObject.Find("Character001");
    //            var canvasGroup =  objWithImage.GetComponent<CanvasGroup>();
    //            canvasGroup.alpha = 0;
    //            break;

    //        default:
    //            var objWithImage1 = GameObject.Find("Character001");
    //            var canvasGroup1 = objWithImage1.GetComponent<CanvasGroup>();
    //            var image =  objWithImage1.GetComponent<Image>();
    //            image.sprite = mySprite;        
    //            //canvasGroup1.alpha = 0;
    //            //var imageSource = objWithImage1.GetComponent<Image>();
    //            //imageSource.sprite = myImage;
    //            break;
    //    }
    //}


	// Use this for initialization
	void Start ()
    {
        //currentIndex = 0;
        //myImage = Resources.Load("Icon2") as Image;
        //mySprite = Resources.Load("Icon2") as Sprite;

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
