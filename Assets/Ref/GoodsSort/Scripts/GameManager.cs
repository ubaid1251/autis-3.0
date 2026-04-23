using Main;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GoodsSort
{
    public class GameManager : MonoBehaviour
    {
        public static bool GameTouch = true;
        public static int InHandSortingIndex = 1000,frontSortingIndex = 980;
        Item currentItem;

        public static GameManager instance;

        public List<Block> blocks;

        public AudioSource dropAudio, tapAudio, matchAudio;

        private void Awake()
        {
            instance = this;
        }
        public LevelGenerator generator;

        //void Start()
        //{
        //    string json = generator.GenerateLevelJSON(2);
        //    Debug.Log(json);
        //}
        private void Start()
        {
            Application.targetFrameRate = 120;

            foreach (Transform t in transform)
            {

                Block b = t.GetComponent<Block>();

                if (b != null)
                {

                    blocks.Add(b);
                }

            }

        }

        public List<Block> GetBlocks() {
            blocks = new List<Block>();
            foreach (Transform t in transform)
            {

                Block b = t.GetComponent<Block>();

                if (b != null)
                {

                    blocks.Add(b);
                }

            }
            return blocks;
        }

        public void IsCompleted() {

            foreach (Block b in blocks) {

                if (!b.frontTiles.IsEmpty()) {
                    return;
                }
            }
            CompletionPanel.instance.OpenCompletionScreen();
            Debug.Log("Tadaaaa Completed");
        
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                DetectObject();
                if (currentItem != null)
                {
                    currentItem.MouseDown();
                }
            }
            if (Input.GetMouseButton(0))
            {

                if (currentItem != null)
                {
                    currentItem.MouseDrag();
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                if (currentItem != null)
                {
                    currentItem.MouseUp();
                }
                currentItem = null;
            }
        }

        void DetectObject()
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D[] colliders = Physics2D.OverlapPointAll(mousePosition);

            if (colliders.Length > 0)
            {
                var rendererObjects = colliders
                    .Select(collider =>
                    {
                        Transform colliderTransform = collider.transform;
                        if (colliderTransform.childCount >= 2)
                        {
                            Transform secondChild = colliderTransform.GetChild(1); // 0 is first, 1 is second
                            SpriteRenderer renderer = secondChild.GetComponent<SpriteRenderer>();
                            return new { Collider = collider, Renderer = renderer };
                        }
                        return null;
                    })
                    .Where(entry => entry != null && entry.Renderer != null)
                    .OrderByDescending(entry => entry.Renderer.sortingOrder);

                var topEntry = rendererObjects.FirstOrDefault();

                if (topEntry != null)
                {
                    Debug.Log($"Clicked on 2nd child: {topEntry.Renderer.gameObject.name}");

                    if (topEntry.Renderer.gameObject.CompareTag("Item"))
                    {
                        currentItem = topEntry.Renderer.transform.parent.GetComponent<Item>();
                    }
                }
            }
        }


        public void Home()
        {
            SceneManager.LoadScene("Main");
        }
    }
}