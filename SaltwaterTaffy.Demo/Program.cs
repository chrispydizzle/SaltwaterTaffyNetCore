// This file is part of SaltwaterTaffy, an nmap wrapper library for .NET
// Copyright (C) 2013 Thom Dixon <thom@thomdixon.org>
// Released under the GNU GPLv2 or any later version
using SaltwaterTaffy.Container;
using System;
using System.Diagnostics;
using System.Linq;

namespace SaltwaterTaffy.Demo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.Write("Enter an IP or subnet: ");
            var target = new Target(Console.ReadLine().Trim());
            Console.WriteLine("Initializing scan of {0}", target);
            var context = new NmapContext(ProcessWindowStyle.Maximized);
            context.Options.Add(NmapFlag.TopPorts, "5000");
            context.Options.Add(NmapFlag.TreatHostsAsOnline);
            context.Options.Add(NmapFlag.VersionAll);
            context.Options.Add(NmapFlag.OsDetection);
            context.Target = target.ToString();
            context.Options.Add(NmapFlag.Verbose);
            var r = context.Run();
            ScanResult result = new ScanResult(r);
            Console.WriteLine("Detected {0} host(s), {1} up and {2} down.", result.Total, result.Up, result.Down);
            foreach (Host i in result.Hosts)
            {
                Console.WriteLine("Host: {0}", i.Address);
                foreach (Port j in i.Ports)
                {
                    Console.Write("\tport {0}", j.PortNumber);
                    if (!string.IsNullOrEmpty(j.Service.Name))
                    {
                        Console.Write(" is running {0}", j.Service.Name);
                    }

                    if (j.Filtered)
                    {
                        Console.Write(" is filtered");
                    }

                    Console.WriteLine();
                }

                if (i.OsMatches.Any())
                {
                    Console.WriteLine("and is probably running {0}", i.OsMatches.First().Name);
                }
            }
        }
    }
}