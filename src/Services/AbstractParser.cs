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
        private HashSet<string> segmentIdentifiers;

        public AbstractParser(string jsonInstructionsPath)
        {
            try
            {
                string jsonContent = File.ReadAllText(jsonInstructionsPath);
                parsingInstructions = JObject.Parse(jsonContent);
                segmentIdentifiers = new HashSet<string> { "BKI62", "BKI63" }; // Consider loading this from config
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading parsing instructions: {ex.Message}");
                throw;
            }
        }

        public void ParseLine(List<string> ticketLines, string outputJsonPath)
        {
            var ticket = new JObject();
            var segments = new JArray();

            foreach (var line in ticketLines)
            {
                try
                {
                    string recordTypeIdentifier = GetRecordTypeIdentifier(line);

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

                            if (segmentIdentifiers.Contains(recordTypeIdentifier))
                            {
                                if (value != ""){
                                    segmentData[attribute] = value;
                                }
                            }
                            else
                            {
                                if (value != ""){
                                    ticket[attribute] = value;
                                }
                            }
                        }

                        if (segmentData.Count > 0 && recordTypeIdentifier == "BKI62")
                        {
                            segments.Add(segmentData);
                        } 
                        else if (segmentData.Count > 0 && recordTypeIdentifier == "BKI63")
                        {
                            // Here, I want to iterate over segmets, see which segment has the
                            // same SegmentIdentifier with the current segment created in BKI63,
                            // and when I find the corresponding segment (there should be one
                            // corresponging SegmentIdentifier for every BKI62, BKI63 couple
                            // corresponding to document) I iterate over BKI63 segment's attributes,
                            // and if current segment we are looking at doesn't contains that attribute,
                            // we just add the attribute to the segment (without creating a new segment)

                            string currentSegmentIdentifier = segmentData["SegmentIdentifier"]?.ToString();
                            string currentOriginAirportCityCode = segmentData["OriginAirportCityCode"]?.ToString();
                            string currentDestinationAirportCityCode = segmentData["DestinationAirportCityCode"]?.ToString();

                            if (!string.IsNullOrEmpty(currentSegmentIdentifier) &&
                                !string.IsNullOrEmpty(currentOriginAirportCityCode) &&
                                !string.IsNullOrEmpty(currentDestinationAirportCityCode))
                            {
                                for (int i = 0; i < segments.Count; i++)
                                {
                                    var segment = (JObject)segments[i];
                                    if (segment["SegmentIdentifier"]?.ToString() == currentSegmentIdentifier &&
                                        segment["OriginAirportCityCode"]?.ToString() == currentOriginAirportCityCode &&
                                        segment["DestinationAirportCityCode"]?.ToString() == currentDestinationAirportCityCode)
                                    {
                                        foreach (var property in segmentData.Properties())
                                        {
                                            if (!segment.ContainsKey(property.Name))
                                            {
                                                segment[property.Name] = property.Value;
                                            }
                                        }
                                        break; // Exit the loop once we've found and updated the matching segment
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error parsing line: {ex.Message}");
                    // Consider how you want to handle parsing errors (skip line, abort, etc.)
                }
            }

            if (segments.Count > 0)
            {
                ticket["Segments"] = segments;
            }

            try
            {
                File.WriteAllText(outputJsonPath, ticket.ToString(Formatting.Indented));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing output file: {ex.Message}");
            }
        }

        private string GetRecordTypeIdentifier(string line)
        {
            string standardMessageIdentifier = line.Substring(StandardMessageIdentifierStartPosition, StandardMessageIdentifierLength);
            string standardNumericQualifier = line.Substring(StandardNumericQualifierStartPosition, StandardNumericQualifierLength);
            return standardMessageIdentifier + standardNumericQualifier;
        }
    }
}