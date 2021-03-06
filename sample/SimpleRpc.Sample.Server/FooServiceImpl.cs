﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SimpleRpc.Sample.Shared;

namespace SimpleRPC.Sample.Server
{
    public class FooServiceImpl : IFooService
    {
        public void Plus(int a, int b)
        {
            Console.WriteLine(a + b);
        }

        public string Concat(string a, string b)
        {
            return a + b;
        }

        public async Task WriteFooAsync(string a, string b)
        {
            await Task.Delay(10);
            Console.WriteLine(a + b);
        }

        public async Task<string> ConcatAsync(string a, string b)
        {
            await Task.Delay(10);
            return a + b;
        }

        public async Task<string> ReturnGenericTypeAsString<T>()
        {
            await Task.Delay(10);
            return typeof(T).ToString();
        }

        public Task<IEnumerable<string>> ReturnGenericIEnumerable<T>()
        {
            return Task.FromResult((IEnumerable<string>)new Stack<string>(new string[] { typeof(T).Name, "1", "2", "3" }));
        }

        public Task<T> ThrowException<T>()
        {
            throw new ArgumentException("This is an expected error message!!!");
        }
    }
}
