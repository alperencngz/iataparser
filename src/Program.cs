using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using IataParser.Models;
using IataParser.Services;

namespace IataParser
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputFilePath = "path_to_your_input_file.txt";
            string outputFolderPath = "path_to_your_output_folder";

            IataParserService parser = new IataParserService();
            List<string> currentTicketLines = new List<string>();
            bool isFirstTicketLine = true;

            var lines = File.ReadLines(inputFilePath);
            int lineNumber = 0;
            foreach (var line in lines)
            {
                lineNumber++;
                if (lineNumber <= 3) continue; // Skip the first three lines for now

                if (line.StartsWith("BKT") && line.Substring(12, 2) == "06" && currentTicketLines.Count > 0)
                {
                    // Process the previous ticket
                    Ticket ticket = parser.ParseTicket(currentTicketLines);
                    SaveTicketAsJson(ticket, outputFolderPath);
                    currentTicketLines.Clear();
                }

                currentTicketLines.Add(line);
            }

            // Process the last ticket
            if (currentTicketLines.Count > 0)
            {
                Ticket ticket = parser.ParseTicket(currentTicketLines);
                SaveTicketAsJson(ticket, outputFolderPath);
            }

            Console.WriteLine("Parsing completed.");
        }

        static void SaveTicketAsJson(Ticket ticket, string folderPath)
        {
            string fileName = $"Ticket_{ticket.TransactionNumber}.json";
            string filePath = Path.Combine(folderPath, fileName);
            string json = JsonConvert.SerializeObject(ticket, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }
    }
}