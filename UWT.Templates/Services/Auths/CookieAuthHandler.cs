using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace UWT.Templates.Services.Auths
{
    class CookieAuthHandler : IAuthenticationHandler, IAuthenticationSignInHandler, IAuthenticationSignOutHandler
    {
        public AuthenticationScheme Scheme { get; private set; }
        protected HttpContext Context { get; private set; }

        public const string CookieName = "uwt";
        public const string MemoryCacheConstHeader = "__uwt_cookie_auth_";
        /// <summary>
        /// 默认过期和续订时长
        /// </summary>
        internal static TimeSpan TicketExpiresTimeSpan = TimeSpan.FromMinutes(30);
        readonly IMemoryCache MemoryCache;
        public CookieAuthHandler(IMemoryCache memoryCache)
        {
            MemoryCache = memoryCache;
        }

        public Task InitializeAsync(AuthenticationScheme scheme, HttpContext context)
        {
            Scheme = scheme;
            Context = context;
            return Task.CompletedTask;
        }

#pragma warning disable CS1998 // 异步方法缺少 "await" 运算符，将以同步方式运行
        public async Task<AuthenticateResult> AuthenticateAsync()
#pragma warning restore CS1998 // 异步方法缺少 "await" 运算符，将以同步方式运行
        {
            var cookie = GetCookieContent();
            if (cookie == null)
            {
                return AuthenticateResult.NoResult();
            }
            var ticket = MemoryCache.Get<AuthenticationTicket>(MemoryCacheConstHeader + cookie);
            Context.User = ticket.Principal;
            return AuthenticateResult.Success(ticket);
        }

        private string GetCookieContent()
        {
            string cookie = null;
            if (Context.Request.Cookies != null && Context.Request.Cookies.ContainsKey(CookieName))
            {
                cookie = Context.Request.Cookies[CookieName];
            }
            else if (Context.Request.Headers.ContainsKey(CookieName))
            {
                cookie = Context.Request.Headers[CookieName];
            }
            if (string.IsNullOrEmpty(cookie))
            {
                return null;
            }
            if (!MemoryCache.TryGetValue(MemoryCacheConstHeader + cookie, out AuthenticationTicket ticket))
            {
                return null;
            }
            return cookie;
        }

        public Task ChallengeAsync(AuthenticationProperties properties)
        {
            return Task.CompletedTask;
        }

        public Task ForbidAsync(AuthenticationProperties properties)
        {
            Context.Response.StatusCode = 403;
            return Task.CompletedTask;
        }

        public Task SignInAsync(ClaimsPrincipal user, AuthenticationProperties properties)
        {
            var ticket = new AuthenticationTicket(user, properties, Scheme.Name);
            var guid = Guid.NewGuid().ToString("N");
            MemoryCache.Set(MemoryCacheConstHeader + guid, ticket, TicketExpiresTimeSpan);
            Context.Response.Cookies.Append(CookieName, guid);
            Context.Items[CookieName] = guid;
            return Task.CompletedTask;
        }

        public Task SignOutAsync(AuthenticationProperties properties)
        {
            var cookie = GetCookieContent();
            MemoryCache.Remove(MemoryCacheConstHeader + cookie);
            Context.Response.Cookies.Delete(CookieName);
            return Task.CompletedTask;
        }
    }
}
