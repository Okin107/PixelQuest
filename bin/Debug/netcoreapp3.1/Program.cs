﻿using System;

namespace Idle_Heroes
{
    class Program
    {
        static void Main(string[] args)
        {
            Bot bot = new Bot();
            bot.RunAsync().GetAwaiter().GetResult();

            Console.WriteLine("Bot connected!");
        }
    }
}
