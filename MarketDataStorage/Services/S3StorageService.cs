using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Moex.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketDataStorage.Services
{
    public class S3StorageService : IStorageService
    {
        private static readonly RegionEndpoint BUCKET_REGION = RegionEndpoint.USEast2;

        public async Task SaveAsync(Futures future, Option option, IEnumerable<OptionTrade> trades)
        {
            var data = trades.Select(t =>
                $"{t.TradeNo}, {t.BoardName}, {t.SecId}, {t.TradeDate}, {t.TradeTime}, {t.Price}, {t.Quantity}, {t.SysTime}");

            var client = new AmazonS3Client(
                "",
                "",
                BUCKET_REGION);

            var putRequest = new PutObjectRequest
            {
                BucketName = "elasticbeanstalk-us-east-2-375346982414",
                Key = GetKeyPath(future.SecId, option.SecId),
                ContentBody = string.Join("\r\n", data.ToArray()),
                ContentType = "text/plain"
            };

            await client.PutObjectAsync(putRequest);
        }

        private string GetKeyPath(string futureSecId, string optionSecId)
        {
            return $"MarketData/{DateTime.Now.ToString("yyyy-MM-dd")}/{futureSecId}/{optionSecId}.csv";
        }
    }
}
