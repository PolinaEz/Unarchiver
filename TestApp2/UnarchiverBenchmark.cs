using Aspose.Zip.Gzip;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet;
using BenchmarkDotNet.Attributes;

namespace TestApp2
{
    [MemoryDiagnoser]
    [Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn]
    public class UnarchiverBenchmark
    {
        [Benchmark]
        public async Task Test()
        {
            string uri = "https://vk.com/doc398556357_656497725?hash=7LeZsIaSbak2z2o6U2wEn0Uj3OgEfjXU558UwXz9Sfg&dl=EPwDPLbYhn73Sz3blh23nbd1NQUGkbNNY7nC0evDKn4";
            string savePath = "D:\\";

            var unarchiver = new Unarchiver();
            await unarchiver.DownloadFileAsync(uri, savePath);
        }
    }
}