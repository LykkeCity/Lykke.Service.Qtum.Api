﻿using System;
using System.Numerics;
using System.Threading.Tasks;
using Lykke.Service.Qtum.Api.Core.Domain.InsightApi;
using NBitcoin;

namespace Lykke.Service.Qtum.Api.Core.Services
{
    /// <summary>
    /// Blockchain service interface
    /// </summary>
    public interface IBlockchainService
    {
        /// <summary>
        /// Get network
        /// </summary>
        /// <returns>Blockchain network <see cref="Network"/></returns>
        Network GetNetwork();
        
        /// <summary>
        /// Address validate
        /// </summary>
        /// <param name="address">Blockchain address</param>
        /// <returns>Is assress valid</returns>
        bool IsAddressValid(string address);
        
        /// <summary>
        /// Bitcoin address parse
        /// </summary>
        /// <param name="address">Address</param>
        /// <returns>Parsed address</returns>
        BitcoinAddress ParseAddress(string address);

        /// <summary>
        /// Address validate
        /// </summary>
        /// <param name="address">Blockchain address</param>
        /// <param name="exception">Validate exception</param>
        /// <returns>Is assress valid</returns>
        bool IsAddressValid(string address, out Exception exception);
        
        /// <summary>
        /// Get address chain height
        /// </summary>
        /// <returns>Block count</returns>
        Task<Int64> GetBlockCountAsync();
        
        /// <summary>
        /// Get balance for address
        /// </summary>
        /// <param name="address">Address</param>
        /// <returns>Balance for address</returns>
        Task<BigInteger> GetAddressBalanceAsync(BitcoinAddress address);
        
        /// <summary>
        /// Broadcast transaction to network
        /// </summary>
        /// <param name="signedTransaction">Signed transaction</param>
        /// <returns>Broadcast result (txId or error)</returns>
        Task<(string txId, string error)> BroadcastSignedTransactionAsync(string signedTransaction);

        /// <summary>
        /// Get transaction info by id
        /// </summary>
        /// <param name="id">Transaction id</param>
        /// <returns><see cref="ITxInfo"/></returns>
        Task<ITxInfo> GetTransactionInfoByIdAsync(string id);
    }
    
    public enum TransactionType
    {
        open,
        receive,
        send,
        change
    }
}
