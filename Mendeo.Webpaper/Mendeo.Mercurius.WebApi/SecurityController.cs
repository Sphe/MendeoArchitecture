using Mendeo.Common.WebApi;
using Mendeo.Mercurius.Dto;
using Mendeo.Mercurius.Model.FullDomain;
using Mendeo.Mercurius.Service.Contract;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace Mendeo.Mercurius.WebApi
{
    public class SecurityController : ApiController
    {
        private const string secretKey = @"6Les9f4SAAAAAFSrdTHG7oVmfKq70l3qXmfbZEBW";

        public SecurityController()
        {
        }

        [HttpGet]
        /* 
         * http://stackoverflow.com/questions/27764692/validating-recaptcha-2-no-captcha-recaptcha-in-asp-nets-server-side 
         * http://www.codeproject.com/Tips/851004/How-to-Validate-Recaptcha-V-Server-side
         * https://developers.google.com/recaptcha/docs/verify
         */
        public async Task<bool> VerifyCaptcha(string responseCaptcha)
        {
            bool Valid = false;
            
            using(var client = new HttpClient())
            {
                // This is the postdata
                var postData = new List<KeyValuePair<string, string>>();
                postData.Add(new KeyValuePair<string, string>("secret", secretKey));
                postData.Add(new KeyValuePair<string, string>("response", responseCaptcha));

                HttpContent content = new FormUrlEncodedContent(postData);

                var res = await client.PostAsync("https://www.google.com/recaptcha/api/siteverify", content);

                var res2 = await res.Content.ReadAsStringAsync();

                var res3 = JsonConvert.DeserializeObject<ReCaptchaClass>(res2);

                return res3.Success;
            }
        }

    }

    public class ReCaptchaClass
    {
        private bool m_Success;
        private List<string> m_ErrorCodes;

        [JsonProperty("success")]
        public bool Success
        {
            get { return m_Success; }
            set { m_Success = value; }
        }

        [JsonProperty("error-codes")]
        public List<string> ErrorCodes
        {
            get { return m_ErrorCodes; }
            set { m_ErrorCodes = value; }
        }
    }
}
