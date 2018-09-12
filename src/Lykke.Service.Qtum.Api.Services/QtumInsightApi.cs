﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lykke.Service.Qtum.Api.Core.Domain.InsightApi;
using Lykke.Service.Qtum.Api.Core.Domain.InsightApi.AddrTxs;
using Lykke.Service.Qtum.Api.Core.Domain.InsightApi.Status;
using Lykke.Service.Qtum.Api.Core.Services;
using Lykke.Service.Qtum.Api.Services.InsightApi;
using Lykke.Service.Qtum.Api.Services.InsightApi.AddrTxs;
using Lykke.Service.Qtum.Api.Services.InsightApi.Status;
using NBitcoin;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Lykke.Service.Qtum.Api.Services
{
    public class QtumInsightApi : IInsightApiService
    {
        private readonly string _url;

        public QtumInsightApi(string url)
        {
            _url = url;
        }

        public async Task<IAddrTxs> GetAddrTxsAsync(BitcoinAddress address, int from = 0, int to = 50)
        {
            var client = new RestClient($"{_url}/addrs/{address}/txs?from={from}&to={to}");
            var request = new RestRequest(Method.GET);
            var response = await client.ExecuteTaskAsync(request);

            if (response.IsSuccessful)
            {
                return JObject.Parse(response.Content).ToObject<AddrTxs>();
            }
            else
            {
                throw response.ErrorException;
            }
        }   

        /// <inheritdoc/>
        public async Task<IStatus> GetStatusAsync()
        {
            var client = new RestClient($"{_url}/status");
            var request = new RestRequest(Method.GET);
            var response = await client.ExecuteTaskAsync(request);

            if (response.IsSuccessful)
            {
                return JObject.Parse(response.Content).ToObject<Status>();
            }
            else
            {
                throw response.ErrorException;
            }       
        }

        /// <inheritdoc/>
        public async Task<List<IUtxo>> GetUtxoAsync(BitcoinAddress address)
        {
            var client = new RestClient($"{_url}/addr/{address}/utxo");
            var request = new RestRequest(Method.GET);
            var response = await client.ExecuteTaskAsync<List<Utxo>>(request);

            if (response.IsSuccessful)
            {
                return response.Data.Select(x => (IUtxo) x).ToList();
            }
            else
            {
                throw response.ErrorException;
            }   
        }
    }
}
