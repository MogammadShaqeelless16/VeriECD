using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using VeriECD.Models;

namespace VeriECD.Services
{
    public class RestService
    {
        private HttpClient client;
        string grant_type = "password";

        public RestService()
        {


            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
        }

        public async Task<Token> Login(User u)
        {


            var postData = new List<KeyValuePair<string, string>>();

            postData.Add(new KeyValuePair<string, string>("grant_type", grant_type));
            postData.Add(new KeyValuePair<string, string>("username", u.Username));
            postData.Add(new KeyValuePair<string, string>("grant_type", u.Password));

            var content = new FormUrlEncodedContent(postData);
            //var weburl = "www.test.com";

            //var response = await PostResponseLogin<Token>(Constants.LoginUrl, content); 

            //DateTime dt = new DateTime();
            //dt = DateTime.Today;
            //response.expire_date = dt.AddSeconds(response.expire_in);

            //return response;

            Token token = new Token() { access_token = "test", expire_in = 30 };
            token.expire_date = DateTime.Now.AddMinutes(token.expire_in);

            return token;
        }

        //public async Task<Token> PostResponse<Token>(string weburl, FormUrlEncodedContent content)
        //{
        //    var response = await client.PostAsync(weburl, content);
        //    var jsonResult = response.Content.ReadAsStringAsync().Result;
        //    var token = JsonConvert.DeserializeObject<Token>(jsonResult);
        //    return token;
        //}

        public async Task<T> PostResponseLogin<T>(string weburl, FormUrlEncodedContent content) where T : class
        {

            var response = await client.PostAsync(weburl, content);
            var jsonResult = response.Content.ReadAsStringAsync().Result;
            var responseObject = JsonConvert.DeserializeObject<T>(jsonResult);
            return responseObject;
        }

        public async Task<T> PostResponse<T>(string weburl, string jsonstring) where T : class 
        {
            /*

            Metoda za generiranje tokena 

           */

            Token Token = App.TokenDatabase.GetToken();
            string ContentType = "application/json";
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.access_token);

            try
            {
                var Result = await client.PostAsync(weburl, new StringContent(jsonstring, Encoding.UTF8, ContentType));
                if (Result.StatusCode == HttpStatusCode.OK)
                {
                    var JsonResult = Result.Content.ReadAsStringAsync().Result;
                    try
                    {
                        var ContentResponse = JsonConvert.DeserializeObject<T>(JsonResult);
                        return ContentResponse;
                    }

                    catch { return null; }
                }
                
            }

            catch { return null; }

            return null;
            
        }

        public async Task<T> GetResponse<T>(string weburl) where T : class
        {
            var Token = App.TokenDatabase.GetToken();
                //TokenDatabase.GetToken();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.access_token);
            try
            {
                var Response = await client.GetAsync(weburl);
                if (Response.StatusCode == HttpStatusCode.OK)
                {
                    var JsonResult = Response.Content.ReadAsStringAsync().Result;

                    try
                    {
                        var ContentResponse = JsonConvert.DeserializeObject<T>(JsonResult);
                        return ContentResponse;
                    }

                    catch { return null; }
                }
            }

            catch { return null; }
            return null;                                
        }
    }
}
