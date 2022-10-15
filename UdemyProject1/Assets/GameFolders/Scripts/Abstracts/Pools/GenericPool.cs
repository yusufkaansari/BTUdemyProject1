using System.Collections;
using System.Collections.Generic;
using UdemyProject1.Combats;
using UnityEngine;

namespace UdemyProject1.Abstracts.Pools
{
    public abstract class GenericPool<T> : MonoBehaviour where T : Component
    {
        // Havuzda saklanacak GameObject
        [SerializeField] T[] prefabs;
        // ilk olu�turmada ka� tane obje olu�turulacak
        [SerializeField] int countLoop = 5;

        // Havuz olu�turulur
        Queue<T> _poolPrefabs = new Queue<T>();

        private void Awake()
        {
            SingletonObject();
        }
        private void OnEnable()
        {
            GameManager.Instance.OnSceneChanged += ResetAllObjects;
        }
        private void OnDisable()
        {
            GameManager.Instance.OnSceneChanged -= ResetAllObjects;
        }
        private void Start()
        {
            FillPoolPrefab();
        }
        protected abstract void SingletonObject();

        public T GetOutPool()
        {
            if (_poolPrefabs.Count == 0)
            {
                // havuz bo� ise, doldurulur
                FillPoolPrefab();
            }
            // e�er havuzda eleman var ise, eleman� ortaya ��kar
            return _poolPrefabs.Dequeue();
        }
        private void FillPoolPrefab()
        {
            for (int i = 0; i < countLoop; i++)
            {
                // e�er objelerin birden fazla Variant'lar� varsa onlardan random bir tanesini alacak
                T newPrefab = Instantiate(prefabs[Random.Range(0, prefabs.Length)]);
                newPrefab.transform.parent = this.transform;
                // Obje havuza girerken inaktif edilir
                newPrefab.gameObject.SetActive(false);
                _poolPrefabs.Enqueue(newPrefab);
            }
        }

        public void PutInPool(T poolObject)
        {
            // havuz tekrar koyulurken obje inaktif edilir.
            poolObject.gameObject.SetActive(false);
            // havuza obje tekrar koyulur
            _poolPrefabs.Enqueue(poolObject);
        }

        public abstract void ResetAllObjects();
    }
}

