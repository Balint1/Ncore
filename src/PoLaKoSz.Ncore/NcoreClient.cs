﻿using System;
using System.Net;
using System.Net.Http;
using PoLaKoSz.Ncore.EndPoints;
using PoLaKoSz.Ncore.Models;
using PoLaKoSz.Ncore.Parsers;

namespace PoLaKoSz.Ncore
{
    /// <summary>
    /// Provides access to nCore.
    /// </summary>
    public class NcoreClient
    {
        /// <summary>
        /// Initializes a new instance with a default <see cref="HttpClient" />.
        /// </summary>
        public NcoreClient()
            : this(new HttpClientHandler())
        {
        }

        /// <summary>
        /// Initializes a new instance with a custom <see cref="HttpClientHandler" />.
        /// </summary>
        /// <param name="messageHandler">Custom messagehandler.</param>
        public NcoreClient(HttpClientHandler messageHandler)
        {
            CookieContainer cookies = new CookieContainer();
            messageHandler.CookieContainer = cookies;
            messageHandler.UseCookies = true;

            HttpClient client = new HttpClient(messageHandler)
            {
                BaseAddress = new Uri("https://ncore.cc/"),
            };
            client.DefaultRequestHeaders.Add("User-Agent", " Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3982.0 Safari/537.36");

            UserConfig userConfig = new UserConfig();
            LoginParser authChecker = new LoginParser();
            Login = new LoginEndPoint(client, cookies, authChecker, userConfig);
        }

        /// <summary>
        /// Gets an access point to all login related action.
        /// </summary>
        public ILoginEndPoint Login { get; }
    }
}
