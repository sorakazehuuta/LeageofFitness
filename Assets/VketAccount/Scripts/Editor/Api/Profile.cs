using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace VketAccount.Api
{
    public class Profile: AbstractApiAccessor
    {
        [Serializable]
        public class PictureUrl
        {
            [SerializeField] private string url = default;
            public string Url => url;
        }
        [Serializable]
        public class CarrierWaveImage
        {
            [SerializeField] private string url = default;
            public string Url => url;
            [SerializeField] private PictureUrl large = default;
            public PictureUrl Large => large;
            [SerializeField] private PictureUrl medium = default;
            public PictureUrl Medium => medium;
            [SerializeField] private PictureUrl small = default;
            public PictureUrl Small => small;
            [SerializeField] private PictureUrl xsmall = default;
            public PictureUrl XSmall => xsmall;
            [SerializeField] private PictureUrl xxsmall = default;
            public PictureUrl XXSmall => xxsmall;
        }
        [Serializable]
        public class Result
        {
            [SerializeField] private string sub = default;
            public string Sub => sub;
            [SerializeField] private string vket_id = default;
            public string VketId => vket_id;
            [SerializeField] private string vket_beta_id = default;
            public string VketBetaId => vket_beta_id;
            [SerializeField] private string name_ja = default;
            public string Name => name_ja;
            [SerializeField] private string name_en = default;
            public string NameEn => name_en;
            [SerializeField] private CarrierWaveImage picture = default;
            public CarrierWaveImage Picture => picture;
        }
        private readonly string _oauthServer;
        private readonly string _accessToken;

        public Profile(string oauthServer, string accessToken)
        {
            _oauthServer = oauthServer;
            _accessToken = accessToken;
        }

        public async Task<ProvideHandle<Result>> GetAsync()
        {
            var taskCompletionSource = new TaskCompletionSource<AsyncOperation>();
            var request = UnityWebRequest.Get($"{_oauthServer}/api/v1/me.json");
            request.SetRequestHeader("Authorization", $"Bearer {_accessToken}");
            request.SetRequestHeader("Content-Type", "application/json");
            request.SendWebRequest().completed += x => taskCompletionSource.SetResult(x);

            await taskCompletionSource.Task;
            var handle = new ProvideHandle<Result>();
            if (request.isHttpError || request.isNetworkError)
            {
                handle.Fail(new NetworkException(request.error, request.responseCode));
                return handle;
            }

            var response = JsonUtility.FromJson<Result>(request.downloadHandler.text);
            handle.Success(response);

            return handle;
        }
    }
}