using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime;
using Newtonsoft.Json;

namespace TradingInfo.Services
{
    public class DatabaseProvider : IDatabaseProvider
    {

        //public AmazonS3Client s3Client;
        public AmazonDynamoDBClient _dynamoDbClient;

        public DatabaseProvider()
        {
            var credentials = new BasicAWSCredentials(Constants.key, Constants.secret);
            //this.s3Client = new AmazonS3Client(credentials, RegionEndpoint.USEast1);
            this._dynamoDbClient = new AmazonDynamoDBClient(credentials, RegionEndpoint.USEast1);
        }

        public async Task<List<StockInfo>> Get()
        {
            return legitS3BucketInfo().Result;
            //return DummyDBRetriever();
        }

        private List<StockInfo> DummyDBRetriever()
        {
            string[] stockTickers = new string[] { "Walmart", "RocketLab NZ", "Amazon", "Meta", "Target", "Citrix", "Bank of America", "Harris Teeter" };

            var rng = new Random();
            List<StockInfo> dailyStockInfo = new List<StockInfo>();

            foreach (string stock in stockTickers)
            {
                dailyStockInfo.Add(
                    new StockInfo
                    {
                        StockTicker = stock,
                        CompanyInfo = "company info",
                        UnitPrice = rng.Next(100, 500),
                        Date = DateTime.Now
                    }
                    );
            }

            return dailyStockInfo;
        }

        private async Task<List<StockInfo>> legitS3BucketInfo()
        {
            List<StockInfo> test = new List<StockInfo>();

            //S3 call
            //var buckets = await s3Client.ListBucketsAsync();

            //DynamoDb call
            /*Dictionary<string, AttributeValue> key = new Dictionary<string, AttributeValue>
            {
                {"Date", new AttributeValue { S = DateTime.Now.ToString("yyyy-MM-dd") } },
                {"Ticker", new AttributeValue {S = "AMZN"} }
            };

            GetItemRequest request = new GetItemRequest
            {
                TableName = "StockInfo",
                Key = key,
            };*/

            //var result = await _dynamoDbClient.GetItemAsync(request);

            var table = Table.LoadTable(_dynamoDbClient, "StockInfo");
            var item = table.GetItemAsync(DateTime.Now.ToString("yyyy-MM-dd"), "AMZN");
            var jsonString = item.Result.ToJson();
            var jsonObject = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(jsonString);

            foreach (var high in jsonObject["Data"].high)
            {
                test.Add(
                    new StockInfo
                    {
                        StockTicker = jsonObject["Ticker"],
                        CompanyInfo = "Company info",
                        UnitPrice = high,
                        Date = DateTime.UtcNow
                    }
                );
            }

            

            //Dictionary<string, AttributeValue> item = result.Item;

            /*foreach (var keyValuePair in item)
            {
                test.Add(
                    new StockInfo
                    {
                        StockTicker = result.ToString(),
                        CompanyInfo = "",
                        UnitPrice = 0.00F,
                        Date = DateTime.UtcNow
                    }
                );
            }*/


            /*test.Add(
                new StockInfo
                {
                    StockTicker = "Success",
                    CompanyInfo = "Success",
                    UnitPrice = buckets.Buckets.Count,
                    Date = DateTime.Now
                }
            );*/

            //Try to get access to buckets
            return test;
        }
    }
}
