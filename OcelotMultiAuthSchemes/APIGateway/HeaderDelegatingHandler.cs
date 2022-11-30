using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using FluentValidation.Resources;
using System;

namespace APIGateway
{
    public class HeaderDelegatingHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HeaderDelegatingHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            IEnumerable<string> headerValues;
            if (request.Headers.TryGetValues("Authorization", out headerValues))
            {
                try
                {
                    string accessToken = headerValues.First();
              
                    //request.Headers.TryAddWithoutValidation("Authorization", "user:S@ptco4321,p@assw@ord:123");
                    //request.Headers.TryAddWithoutValidation("Accept-Language", "ar-sa");
                    request.Headers.Authorization = new AuthenticationHeaderValue("SAPTCO", "user:S@ptco4321,p@assw@ord:123");
                    request.Headers.AcceptLanguage.Add(new StringWithQualityHeaderValue("ar-sa"));
                    request.Headers.Remove("AccessToken");
                }
                catch(Exception ex)
                {
                    
                }
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
