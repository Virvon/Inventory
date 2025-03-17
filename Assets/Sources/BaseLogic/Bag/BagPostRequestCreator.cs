using Assets.Sources.BaseLogic.Item;
using Assets.Sources.Services.CoroutineRunner;
using Assets.Sources.Utils;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Sources.BaseLogic.Bag
{
    public class BagPostRequestCreator
    {
        private const string ServerURL = "https://wadahub.manerai.com/api/inventory/status";
        private const string AuthorizationToken = "kPERnYcWAY46xaSy8CEzanosAgsWM84Nx7SKM4QBSqPq6c7StWfGxzhxPfDh8MaP";

        private readonly Model.Bag _bag;
        private readonly ICoroutineRunner _coroutineRunner;

        public BagPostRequestCreator(Model.Bag bag, ICoroutineRunner coroutineRunner)
        {
            _bag = bag;

            _bag.ItemAdded.AddListener(OnItemAdded);
            _bag.ItemRemoved.AddListener(OnItemRemoved);
            _coroutineRunner = coroutineRunner;
        }

        private void OnItemRemoved(ItemObject item) =>
            _coroutineRunner.StartCoroutine(Send(item.Identifire, "Removed"));

        private void OnItemAdded(ItemObject item) =>
            _coroutineRunner.StartCoroutine(Send(item.Identifire, "Added"));

        public IEnumerator Send(Guid itemIdentifier, string eventType)
        {
            string jsonData = new EventData(itemIdentifier.ToString(), eventType).ToJson();

            WWWForm formData = new();
            UnityWebRequest request = UnityWebRequest.Post(ServerURL, formData);
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Authorization", $"Bearer {AuthorizationToken}");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
                throw new Exception(request.error);

            Debug.Log("Server response: " + request.downloadHandler.text);
        }

        [Serializable]
        private struct EventData
        {
            public string ItemIdentifier;
            public string EventType;

            public EventData(string itemIdentifier, string eventType)
            {
                ItemIdentifier = itemIdentifier;
                EventType = eventType;
            }
        }
    }
}