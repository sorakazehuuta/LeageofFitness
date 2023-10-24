using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace VketAccount.Authorization
{
    public class RefreshedAccessTokenProvider
    {
        [Serializable]
        public class Result
        {
            [SerializeField]
            private string access_token = default;
            public string AccessToken => access_token;
            [SerializeField]
            private string expires_in = default;
            public string ExpiresIn => expires_in;
            [SerializeField]
            private string scope = default;
            public string Scope => scope;
            [SerializeField]
            private string token_type = default;
            public string TokenType => token_type;
        }
        
        private readonly string _oauthServer;
        private readonly string _clientId;
        private readonly string _clientSecret;
        
        public RefreshedAccessTokenProvider(string oauthServer, string clientId, string clientSecret)
        {
            _oauthServer = oauthServer;
            _clientId = clientId;
            _clientSecret = clientSecret;
        }

        public async Task<ProvideHandle<Result>> ProvideAsync(string refreshToken)
        {
            var form = new WWWForm();
            form.AddField("client_id", _clientId);
            form.AddField("client_secret", _clientSecret);
            form.AddField("grant_type", "refresh_token");
            form.AddField("refresh_token", refreshToken);

            var taskCompletionSource = new TaskCompletionSource<AsyncOperation>();
            var request = UnityWebRequest.Post($"{_oauthServer}/oauth/token", form);
            request.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");
            request.SendWebRequest().completed += x => taskCompletionSource.SetResult(x);
            
            await taskCompletionSource.Task;
            var handle = new ProvideHandle<Result>();
            if(request.isHttpError || request.isNetworkError) {
                handle.Fail(new NetworkException(request.error ,request.responseCode));
                return handle;
            }
            
            var response = JsonUtility.FromJson<Result>(request.downloadHandler.text);
            handle.Success(response);
            return handle;
        }
    }
}