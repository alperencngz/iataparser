using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace IataParser.Services
{
    public class AbstractParser
    {
        private const int StandardMessageIdentifierStartPosition = 0;
        private const int StandardMessageIdentifierLength = 3;
        private const int StandardNumericQualifierStartPosition = 11;
        private const int StandardNumericQualifierLength = 2;

        private JObject parsingInstructions;

        public AbstractParser(string jsonInstructionsPath)
        {
            string jsonContent = File.ReadAllText(jsonInstructionsPath);
            parsingInstructions = JObject.Parse(jsonContent);
        }

        public void ParseLine(List<string> ticketLines, string outputJsonPath)
        {
            var ticket = new JObject();
            var segments = new JArray();

            foreach (var line in ticketLines)
            {
                string standardMessageIdentifier = line.Substring(StandardMessageIdentifierStartPosition, StandardMessageIdentifierLength);
                string standardNumericQualifier = line.Substring(StandardNumericQualifierStartPosition, StandardNumericQualifierLength);
                string recordTypeIdentifier = standardMessageIdentifier + standardNumericQualifier;

                if (parsingInstructions.ContainsKey(recordTypeIdentifier))
                {
                    var instructions = parsingInstructions[recordTypeIdentifier];
                    var segmentData = new JObject();

                    foreach (var instruction in instructions)
                    {
                        string attribute = instruction["attribute"].ToString();
                        int startPosition = int.Parse(instruction["start_position"].ToString());
                        int length = int.Parse(instruction["length"].ToString());

                        string value = line.Substring(startPosition, length).Trim();

                        if (recordTypeIdentifier == "BKI62" || recordTypeIdentifier == "BKI63")
                        {
                            segmentData[attribute] = value;
                        }
                        else
                        {
                            ticket[attribute] = value;
                        }
                    }

                    if (segmentData.Count > 0)
                    {
                        segments.Add(segmentData);
                    }
                }
            }

            if (segments.Count > 0)
            {
                ticket["Segments"] = segments;
            }

            File.WriteAllText(outputJsonPath, ticket.ToString(Formatting.Indented));
        }
    }
}