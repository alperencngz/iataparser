using System;
using System.Collections.Generic;
using System.IO;
using IataParser.Services;

namespace IataParser
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputFilePath = "/Users/alperencngzz/Desktop/internwork/IataParser/data/input/input.txt";
            string outputFolderPath = "/Users/alperencngzz/Desktop/internwork/IataParser/data/output";
            string jsonInstructionsPath = "/Users/alperencngzz/Desktop/internwork/IataParser/src/Helpers/config.json";

            AbstractParser parser = new AbstractParser(jsonInstructionsPath);
            List<string> currentTicketLines = new List<string>();

            var lines = File.ReadLines(inputFilePath);
            int lineNumber = 0;
            int ticketCounter = 0;
            foreach (var line in lines)
            {
                lineNumber++;
                if (lineNumber <= 3) continue; // Skip the first three lines

                if (line.StartsWith("BKT") && line.Substring(11, 2) == "06" && currentTicketLines.Count > 0)
                {
                    // Process the previous ticket
                    ProcessTicket(parser, currentTicketLines, outputFolderPath, ticketCounter);
                    currentTicketLines.Clear();
                    ticketCounter++;
                }

                currentTicketLines.Add(line);
            }

            // Process the last ticket
            if (currentTicketLines.Count > 0)
            {
                ProcessTicket(parser, currentTicketLines, outputFolderPath, ticketCounter);
            }

            Console.WriteLine("Parsing completed.");
        }

        static void ProcessTicket(AbstractParser parser, List<string> ticketLines, string outputFolderPath, int ticketCounter)
        {
            string outputJsonPath = Path.Combine(outputFolderPath, $"Ticket_{ticketCounter}.json");
            parser.ParseLine(ticketLines, outputJsonPath);
        }
    }
}