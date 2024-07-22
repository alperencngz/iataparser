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

            var lines = File.ReadLines(inputFilePath);
            int lineNumber = 0;
            foreach (var line in lines)
            {
                lineNumber++;
                if (lineNumber <= 3) continue; // Skip the first three lines

                if (line.StartsWith("BKT") && line.Substring(11, 2) == "06" && currentTicketLines.Count > 0)
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

            var ticketJson = new
            {
                TransactionNumber = ticket.TransactionNumber,
                TransactionRecordCounter = ticket.TransactionRecordCounter,
                TicketingAirlineCodeNumber = ticket.TicketingAirlineCodeNumber,
                ReportingSystemIdentifier = ticket.ReportingSystemIdentifier,
                DateOfIssue = ticket.DateOfIssue,
                TicketDocumentNumber = ticket.TicketDocumentNumber,
                CheckDigit = ticket.CheckDigit,
                CouponUseIndicator = ticket.CouponUseIndicator,
                ConjunctionTicketIndicator = ticket.ConjunctionTicketIndicator,
                AgentNumericCode = ticket.AgentNumericCode,
                ReasonForIssuanceCode = ticket.ReasonForIssuanceCode,
                TourCode = ticket.TourCode,
                TransactionCode = ticket.TransactionCode,
                TrueOriginDestinationCityCodes = ticket.TrueOriginDestinationCityCodes,
                PnrReferenceAndOrAirlineData = ticket.PnrReferenceAndOrAirlineData,
                TimeOfIssue = ticket.TimeOfIssue,
                JourneyTurnaroundAirportCityCode = ticket.JourneyTurnaroundAirportCityCode,
                OriginalIssueTicketDocumentNumber = ticket.OriginalIssueTicketDocumentNumber,
                OriginalIssueLocationCityCode = ticket.OriginalIssueLocationCityCode,
                OriginalIssueDate = ticket.OriginalIssueDate,
                OriginalIssueAgentNumericCode = ticket.OriginalIssueAgentNumericCode,
                EndorsementsRestrictions = ticket.EndorsementsRestrictions,
                Segments = GetSegments(ticket),
                Fare = ticket.Fare,
                TicketingModeIndicator = ticket.TicketingModeIndicator,
                EquivalentFarePaid = ticket.EquivalentFarePaid,
                Total = ticket.Total,
                ServicingAirlineSystemProviderIdentifier = ticket.ServicingAirlineSystemProviderIdentifier,
                FareCalculationModeIndicator = ticket.FareCalculationModeIndicator,
                BookingAgentIdentification = ticket.BookingAgentIdentification,
                BookingEntityOutletType = ticket.BookingEntityOutletType,
                FareCalculationPricingIndicator = ticket.FareCalculationPricingIndicator,
                AirlineIssuingAgent = ticket.AirlineIssuingAgent,
                PassengerName = ticket.PassengerName,
                PassengerSpecificData = ticket.PassengerSpecificData,
                DateOfBirth = ticket.DateOfBirth,
                PassengerTypeCode = ticket.PassengerTypeCode,
                FormOfPaymentSequenceNumber = ticket.FormOfPaymentSequenceNumber,
                FormOfPaymentInformation = ticket.FormOfPaymentInformation,
                FareCalculationSequenceNumber = ticket.FareCalculationSequenceNumber,
                FareCalculationArea = ticket.FareCalculationArea,
                FormOfPaymentType = ticket.FormOfPaymentType,
                FormOfPaymentTransactionIdentifier = ticket.FormOfPaymentTransactionIdentifier,
                FormOfPaymentAmount = ticket.FormOfPaymentAmount,
                FormOfPaymentAccountNumber = ticket.FormOfPaymentAccountNumber,
                ExpiryDate = ticket.ExpiryDate,
                ExtendedPaymentCode = ticket.ExtendedPaymentCode,
                ApprovalCode = ticket.ApprovalCode,
                InvoiceNumber = ticket.InvoiceNumber,
                InvoiceDate = ticket.InvoiceDate,
                RemittanceAmount = ticket.RemittanceAmount,
                CardVerificationValueResult = ticket.CardVerificationValueResult,
                CommissionType = ticket.CommissionType,
                CommissionRate = ticket.CommissionRate,
                CommissionAmount = ticket.CommissionAmount,
                EffectiveCommissionRate = ticket.EffectiveCommissionRate,
                EffectiveCommissionAmount = ticket.EffectiveCommissionAmount,
                CurrencyType = ticket.CurrencyType
            };

            string json = JsonConvert.SerializeObject(ticketJson, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        static List<object> GetSegments(Ticket ticket)
        {
            var segments = new List<object>();
            for (int i = 0; i < ticket.SegmentIdentifier.Count; i++)
            {
                segments.Add(new
                {
                    SegmentIdentifier = ticket.SegmentIdentifier[i],
                    OriginAirportCityCode = ticket.OriginAirportCityCode[i],
                    FlightDepartureDate = ticket.FlightDepartureDate[i],
                    FlightDepartureTime = ticket.FlightDepartureTime[i],
                    FlightDepartureTerminal = ticket.FlightDepartureTerminal[i],
                    DestinationAirportCityCode = ticket.DestinationAirportCityCode[i],
                    FlightArrivalDate = ticket.FlightArrivalDate[i],
                    FlightArrivalTime = ticket.FlightArrivalTime[i],
                    FlightArrivalTerminal = ticket.FlightArrivalTerminal[i],
                    StopoverCode = ticket.StopoverCode[i],
                    NotValidBeforeDate = ticket.NotValidBeforeDate[i],
                    NotValidAfterDate = ticket.NotValidAfterDate[i],
                    Carrier = ticket.Carrier[i],
                    SoldPassengerCabin = ticket.SoldPassengerCabin[i],
                    FlightNumber = ticket.FlightNumber[i],
                    ReservationBookingDesignator = ticket.ReservationBookingDesignator[i],
                    FlightBookingStatus = ticket.FlightBookingStatus[i],
                    BaggageAllowance = ticket.BaggageAllowance[i],
                    FareBasisTicketDesignator = ticket.FareBasisTicketDesignator[i],
                    FrequentFlyerReference = ticket.FrequentFlyerReference[i],
                    FareComponentPricedPassengerTypeCode = ticket.FareComponentPricedPassengerTypeCode[i],
                    ThroughChangeOfGaugeIndicator = ticket.ThroughChangeOfGaugeIndicator[i],
                    EquipmentCode = ticket.EquipmentCode[i]
                });
            }
            return segments;
        }
    }
}