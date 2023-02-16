using System;
using TestApp2;
using BenchmarkDotNet.Running;
using System.Diagnostics;


#if DEBUG

string uri = "https://vk.com/doc398556357_656497725?hash=7LeZsIaSbak2z2o6U2wEn0Uj3OgEfjXU558UwXz9Sfg&dl=EPwDPLbYhn73Sz3blh23nbd1NQUGkbNNY7nC0evDKn4";
string savePath = "D:\\";


Stopwatch time = Stopwatch.StartNew();
var unarchiver = new Unarchiver();
await unarchiver.DownloadFileAsync(uri, savePath);
time.Stop();
Console.WriteLine($"Time - {time.ElapsedMilliseconds} ms");

#else

BenchmarkRunner.Run<UnarchiverBenchmark>();

#endif