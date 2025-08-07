﻿// Case Study 01 - Multithreading 
// Updated: 2025-06-25
using System;
using System.IO;
using CalculatingFunctions;
using System.Threading;
using System;
using System.Text.Json;
using System.Diagnostics;

class Program
{
    static decimal[] data = new decimal[11000001];
    static decimal result = 0;
    static int index = 0;

    //Algorithm of CalClass.Calculate1()
    //{        

    //    index = 0;
    //    while (index < 110000000) 
    //    {
    //        decimal sum = 0;


    //        if ((int)data[index] % 2 == 0)
    //        {
    //            sum += (decimal)((double)data[index] * 0.002);
    //        }
    //        else if ((int)data[index] % 3 == 0)
    //        {
    //            sum += (decimal)((double)data[index] * 0.003);
    //        }
    //        else if ((int)data[index] % 5 == 0)
    //        {
    //            sum += (decimal)((double)data[index] * 0.005);
    //        }
    //        else if ((int)data[index] % 7 == 0)
    //        {
    //            sum += (decimal)((double)data[index] * 0.007);
    //        }
    //        else
    //        {
    //            sum += (decimal)((double)data[index] * 0.001);
    //        }


    //        if ((long)sum % 2 == 0)
    //        {
    //            result += sum * (decimal)0.00001;
    //        }
    //        else
    //        {
    //            result += (sum * (-1)) * (decimal)0.00001;
    //        }

    //        data[index] *= (decimal)0.1;
    //        index++;

    //    }
    //}

    private static void ThreadWork()
    {
        CalClass CF = new CalClass();
        int i = 0;

        while (i < 30)
        {
            index = 0;
            while (index < 10000000)
            {
                result += CF.Calculate1(ref data, ref index);
            }
            i++;
        }
    }

    private static void LoadData()
    {
        Console.WriteLine("Loading data...");
        FileStream fs = new FileStream("data.bin", FileMode.Open);
        BinaryReader br = new BinaryReader(fs);
        for (int i = 0; i < data.Length; i++)
        {
            Single f = br.ReadSingle();
            data[i] = (decimal)(f * 36);
        }
        Console.WriteLine("Data loaded successfully.\n\n");
    }

    private static void Main(string[] args)
    {
        LoadData();
        Console.WriteLine("Calculation start ...");

        Thread Th1 = new Thread(ThreadWork);
        //Thread Th2 = new Thread(ThreadWork);

        Stopwatch _st = new Stopwatch();
        _st.Start();

        Th1.Start();
        //Th2.Start();
        Th1.Join(); // Wait for the thread to finish
        //Th2.Join(); // Wait for the thread to finish

        _st.Stop();
        Console.WriteLine($"Calculation finished in {_st.ElapsedMilliseconds} ms. Result: {result.ToString("F25")}");
    }
}