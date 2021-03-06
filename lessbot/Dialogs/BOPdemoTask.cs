﻿using BOPdemo.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BOPdemo
{
    public class BOPdemoTask
    {
        public static async Task<double?> GetStockRateAsync(string StockSymbol)
        {
            try
            {
                string ServiceURL = $"http://finance.yahoo.com/d/quotes.csv?s={StockSymbol}&f=sl1d1nd";
                string ResultInCSV;
                using (WebClient client = new WebClient())
                {
                    ResultInCSV = await client.DownloadStringTaskAsync(ServiceURL).ConfigureAwait(false);
                }
                var FirstLine = ResultInCSV.Split('\n')[0];
                var Price = FirstLine.Split(',')[1];
                if (Price != null && Price.Length >= 0)
                {
                    double result;
                    if (double.TryParse(Price, out result))
                    {
                        return result;
                    }
                }
                return null;
            }
            catch (WebException ex)
            {
                //handle your exception here  
                throw ex;
            }
        }

        public static async Task<WeatherData> GetWeatherAsync(string city)
        {
            try
            {
                string ServiceURL = $"https://free-api.heweather.com/x3/weather?city={city}&key=e8c9a95d0a0f4d2092def1174c44be17";
                string ResultString;
                using (WebClient client = new WebClient())
                {
                    client.Encoding = Encoding.UTF8;
                    ResultString = await client.DownloadStringTaskAsync(ServiceURL).ConfigureAwait(false);
                }
                WeatherData weatherData = (WeatherData)JsonConvert.DeserializeObject(ResultString, typeof(WeatherData));
                return weatherData;
            }
            catch (WebException ex)
            {
                //handle your exception here  
                //throw ex;
                return null;
            }
        }
    }
}