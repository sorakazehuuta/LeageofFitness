
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace CombatGimmick
{
    //---------------------------------------     
    [UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
    public class TutorialBoard : UdonSharpBehaviour
    {
        int pageNum;
        [Tooltip("表示するテクスチャ")] public Texture[] tutorialTex;
        [Tooltip("表示用のマテリアル")] public Material mat;
        //---------------------------------------     
        void Start()
        {
            if(tutorialTex.Length <= 0 || !tutorialTex[0]) { return; }

            ShowFirstPage();
        }
        //--------------------------------------- 
        public void NextPage()
        {
            if (tutorialTex.Length <= 0) { return; }

            ++pageNum;
            if (pageNum >= tutorialTex.Length)
            {
                pageNum = 0;
            }

            ShowPage();
        }
        //---------------------------------------     
        public void PrevPage()
        {
            if (tutorialTex.Length <= 0) { return; }

            --pageNum;
            if (pageNum < 0)
            {
                pageNum = tutorialTex.Length - 1;
            }

            ShowPage();
        }
        //---------------------------------------     
        public void ShowFirstPage()
        {
            mat.mainTexture = tutorialTex[0];
        }
        //---------------------------------------     
        public void ShowPage()
        {
            mat.mainTexture = tutorialTex[pageNum];
        }
        //---------------------------------------     
    }
}



