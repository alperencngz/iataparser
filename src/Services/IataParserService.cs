using System;
using System.Collections.Generic;
using IataParser.Models;

namespace IataParser.Services
{
    public class IataParserService
    {
        private const int StandardMessageIdentifierStartPosition = 0;
        private const int StandardMessageIdentifierLength = 3;
        private const int StandardNumericQualifierStartPosition = 11;
        private const int StandardNumericQualifierLength = 2;

        private const string TransactionHeaderRecord = "BKT06";
        private const string DocumentIdentificationRecord = "BKS24";
        private const string QualifyingIssueInformationForSalesTransactionsRecord = "BKS46";
        private const string AdditionalItineraryDataRecord = "BKI62";
        private const string ItineraryDataSegmentRecord = "BKI63";
        private const string DocumentAmountsRecord = "BAR64";
        private const string AdditionalInformationPassengerRecord = "BAR65";
        private const string AdditionalInformationFormOfPaymentRecord = "BAR66";
        private const string FareCalculationRecord = "BKF81";
        private const string AdditionalCardInformationRecord = "BCC82";
        private const string FormOfPaymentRecord = "BKP84";
        private const string CommissionRecord = "BKS39";

        public Ticket ParseTicket(List<string> ticketLines)
        {
            Ticket ticket = new Ticket();
            int bks24Counter = 0;
            int bks46Counter = 0;
            int segmentCounter = 0;

            foreach (var line in ticketLines)
            {
                string standardMessageIdentifier = line.Substring(StandardMessageIdentifierStartPosition, StandardMessageIdentifierLength);
                string standardNumericQualifier = line.Substring(StandardNumericQualifierStartPosition, StandardNumericQualifierLength);
                string recordTypeIdentifier = standardMessageIdentifier + standardNumericQualifier;

                switch (recordTypeIdentifier)
                {
                    case TransactionHeaderRecord:
                        ParseBKT06(line, ticket);
                        break;
                    case DocumentIdentificationRecord:
                        ParseBKS24(line, ticket, ref bks24Counter);
                        break;
                    case QualifyingIssueInformationForSalesTransactionsRecord:
                        ParseBKS46(line, ticket, ref bks46Counter);
                        break;
                    case AdditionalItineraryDataRecord:
                        ParseBKI62(line, ticket, ref segmentCounter);
                        break;
                    case ItineraryDataSegmentRecord:
                        ParseBKI63(line, ticket, segmentCounter);
                        break;
                    case DocumentAmountsRecord:
                        ParseBAR64(line, ticket);
                        break;
                    case AdditionalInformationPassengerRecord:
                        ParseBAR65(line, ticket);
                        break;
                    case AdditionalInformationFormOfPaymentRecord:
                        ParseBAR66(line, ticket);
                        break;
                    case FareCalculationRecord:
                        ParseBKF81(line, ticket);
                        break;
                    case AdditionalCardInformationRecord:
                        ParseBCC82(line, ticket);
                        break;
                    case FormOfPaymentRecord:
                        ParseBKP84(line, ticket);
                        break;
                    case CommissionRecord:
                        ParseBKS39(line, ticket);
                        break;
                }
            }

            return ticket;
        }

        private void ParseBKT06(string line, Ticket ticket)
        {
            ticket.TransactionNumber = line.Substring(13, 6);
            ticket.TransactionRecordCounter = line.Substring(21, 3);
            ticket.TicketingAirlineCodeNumber = line.Substring(24, 3);
            ticket.ReportingSystemIdentifier = line.Substring(64, 4);
        }

        private void ParseBKS24(string line, Ticket ticket, ref int bks24Counter)
        {
            if (bks24Counter == 0)
            {
                ticket.DateOfIssue = line.Substring(13, 6);
                ticket.TicketDocumentNumber = line.Substring(25, 14);
                ticket.CheckDigit = line.Substring(39, 1);
                ticket.CouponUseIndicator = line.Substring(40, 4);
                ticket.ConjunctionTicketIndicator = line.Substring(44, 3);
                ticket.AgentNumericCode = line.Substring(47, 8);
                ticket.ReasonForIssuanceCode = line.Substring(55, 1);
                ticket.TourCode = line.Substring(56, 15);
                ticket.TransactionCode = line.Substring(71, 4);
                ticket.TrueOriginDestinationCityCodes = line.Substring(75, 10);
                ticket.PnrReferenceAndOrAirlineData = line.Substring(85, 13);
                ticket.TimeOfIssue = line.Substring(98, 4);
                ticket.JourneyTurnaroundAirportCityCode = line.Substring(102, 5);
            }
            else if (bks24Counter == 1 && line.Substring(44, 3) == "CNJ")
            {
                ticket.IsConjunctionTicket = true;
                ticket.ConjunctionTicketDocumentNumber = line.Substring(25, 14);
                ticket.ConjunctionCheckDigit = line.Substring(39, 1);
                ticket.ConjunctionCouponUseIndicator = line.Substring(40, 4);
                ticket.ConjunctionTicketIndicator = line.Substring(44, 3);
            }
            bks24Counter++;
        }

        private void ParseBKS46(string line, Ticket ticket, ref int bks46Counter)
        {
            if (bks46Counter == 0)
            {
                ticket.OriginalIssueTicketDocumentNumber = line.Substring(40, 14);
                ticket.OriginalIssueLocationCityCode = line.Substring(54, 3);
                ticket.OriginalIssueDate = line.Substring(57, 7);
                ticket.OriginalIssueAgentNumericCode = line.Substring(64, 8);
                ticket.EndorsementsRestrictions = line.Substring(72, 49);
            }
            bks46Counter++;
        }

        private void ParseBKI62(string line, Ticket ticket, ref int segmentCounter)
        {
            ticket.SegmentIdentifier.Add(line.Substring(40, 1));
            ticket.OriginAirportCityCode.Add(line.Substring(41, 5));
            ticket.FlightDepartureDate.Add(line.Substring(46, 7));
            ticket.FlightDepartureTime.Add(line.Substring(53, 5));
            ticket.FlightDepartureTerminal.Add(line.Substring(58, 5));
            ticket.DestinationAirportCityCode.Add(line.Substring(63, 5));
            ticket.FlightArrivalDate.Add(line.Substring(68, 7));
            ticket.FlightArrivalTime.Add(line.Substring(75, 5));
            ticket.FlightArrivalTerminal.Add(line.Substring(80, 5));
            segmentCounter++;
        }

        private void ParseBKI63(string line, Ticket ticket, int segmentCounter)
        {
            ticket.StopoverCode.Add(line.Substring(41, 1));
            ticket.NotValidBeforeDate.Add(line.Substring(42, 5));
            ticket.NotValidAfterDate.Add(line.Substring(47, 5));
            ticket.Carrier.Add(line.Substring(62, 3));
            ticket.SoldPassengerCabin.Add(line.Substring(65, 1));
            ticket.FlightNumber.Add(line.Substring(66, 5));
            ticket.ReservationBookingDesignator.Add(line.Substring(71, 2));
            ticket.FlightBookingStatus.Add(line.Substring(85, 2));
            ticket.BaggageAllowance.Add(line.Substring(87, 3));
            ticket.FareBasisTicketDesignator.Add(line.Substring(90, 15));
            ticket.FrequentFlyerReference.Add(line.Substring(105, 20));
            ticket.FareComponentPricedPassengerTypeCode.Add(line.Substring(125, 3));
            ticket.ThroughChangeOfGaugeIndicator.Add(line.Substring(128, 1));
            ticket.EquipmentCode.Add(line.Substring(129, 3));
        }

        private void ParseBAR64(string line, Ticket ticket)
        {
            ticket.Fare = line.Substring(40, 12);
            ticket.TicketingModeIndicator = line.Substring(52, 1);
            ticket.EquivalentFarePaid = line.Substring(53, 12);
            ticket.Total = line.Substring(65, 12);
            ticket.ServicingAirlineSystemProviderIdentifier = line.Substring(77, 4);
            ticket.FareCalculationModeIndicator = line.Substring(81, 1);
            ticket.BookingAgentIdentification = line.Substring(82, 6);
            ticket.BookingEntityOutletType = line.Substring(88, 1);
            ticket.FareCalculationPricingIndicator = line.Substring(89, 1);
            ticket.AirlineIssuingAgent = line.Substring(90, 8);
        }

        private void ParseBAR65(string line, Ticket ticket)
        {
            ticket.PassengerName = line.Substring(40, 49);
            ticket.PassengerSpecificData = line.Substring(89, 29);
            ticket.DateOfBirth = line.Substring(118, 7);
            ticket.PassengerTypeCode = line.Substring(125, 3);
        }

        private void ParseBAR66(string line, Ticket ticket)
        {
            ticket.FormOfPaymentSequenceNumber = line.Substring(40, 1);
            ticket.FormOfPaymentInformation = line.Substring(41, 50);
        }

        private void ParseBKF81(string line, Ticket ticket)
        {
            ticket.FareCalculationSequenceNumber = line.Substring(40, 1);
            ticket.FareCalculationArea = line.Substring(41, 87);
        }

        private void ParseBCC82(string line, Ticket ticket)
        {
            ticket.FormOfPaymentType = line.Substring(25, 10);
            ticket.FormOfPaymentTransactionIdentifier = line.Substring(35, 25);
        }

        private void ParseBKP84(string line, Ticket ticket)
        {
            ticket.FormOfPaymentType = line.Substring(25, 10);
            ticket.FormOfPaymentAmount = line.Substring(35, 11);
            ticket.FormOfPaymentAccountNumber = line.Substring(46, 19);
            ticket.ExpiryDate = line.Substring(65, 4);
            ticket.ExtendedPaymentCode = line.Substring(69, 2);
            ticket.ApprovalCode = line.Substring(71, 6);
            ticket.InvoiceNumber = line.Substring(77, 14);
            ticket.InvoiceDate = line.Substring(91, 6);
            ticket.RemittanceAmount = line.Substring(97, 11);
            ticket.CardVerificationValueResult = line.Substring(108, 1);
            ticket.CurrencyType = line.Substring(132, 4);
        }

        private void ParseBKS39(string line, Ticket ticket)
        {
            ticket.CommissionType = line.Substring(43, 6);
            ticket.CommissionRate = line.Substring(49, 5);
            ticket.CommissionAmount = line.Substring(54, 11);
            ticket.EffectiveCommissionRate = line.Substring(87, 5);
            ticket.EffectiveCommissionAmount = line.Substring(92, 11);
            ticket.CurrencyType = line.Substring(132, 4);
        }
    }
}