
using System;
using System.Web.Http;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace KCollector
{
    public class KeyWordController : ApiController
    {
     
        [HttpGet]
        public int Get(string word)
        {
            Random random = new Random();
            return  random.Next(1, 1000);

        }

    }
}

namespace KCollector
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net.Http.Formatting;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using System.Web;

    namespace API
    {
        public class JsonpMediaTypeFormatter : JsonMediaTypeFormatter
        {

            private string _callbackQueryParamter;

            public JsonpMediaTypeFormatter()
            {
                SupportedMediaTypes.Add(DefaultMediaType);
                SupportedMediaTypes.Add(new MediaTypeWithQualityHeaderValue("text/javascript"));

                MediaTypeMappings.Add(new UriPathExtensionMapping("jsonp", DefaultMediaType));
            }

            public string CallbackQueryParameter
            {
                get { return _callbackQueryParamter ?? "callback"; }
                set { _callbackQueryParamter = value; }
            }

            public override System.Threading.Tasks.Task WriteToStreamAsync(Type type, object value, System.IO.Stream writeStream, System.Net.Http.HttpContent content, System.Net.TransportContext transportContext)
            {
                string callback;
                if (IsJsonpRequest(out callback))
                {
                    return Task.Factory.StartNew(() =>
                    {
                        var writer = new StreamWriter(writeStream);
                        writer.Write(callback + "(");
                        writer.Flush();
                        base.WriteToStreamAsync(type, value, writeStream, content, transportContext).Wait();
                        writer.Write(")");
                        writer.Flush();
                    });
                }
                return base.WriteToStreamAsync(type, value, writeStream, content, transportContext);
            }

            private bool IsJsonpRequest(out string callback)
            {
                callback = null;
                switch (HttpContext.Current.Request.HttpMethod)
                {
                    case "POST":
                        callback = HttpContext.Current.Request.Form[CallbackQueryParameter];
                        break;
                    default:
                        callback = HttpContext.Current.Request.QueryString[CallbackQueryParameter];
                        break;
                }
                return !string.IsNullOrEmpty(callback);
            }
        }
    }
}