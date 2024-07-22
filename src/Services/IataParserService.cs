using System;
using System.Collections.Generic;
using IataParser.Models;

namespace IataParser.Services
{

    int standardMessageIdentifierStartPosition = 0;
    int standardMessageIdentifierLength = 3;
    int standardNumericQualifierStartPosition = 11;
    int standardNumericQualifierLength = 2;



    string transactionHeaderRecord = "BKT06";
    string documentIdentificationRecord = "BKS24";
    string qualifyingIssueInformationForSalesTransactionsRecord = "BKS46";
    string additionalItineraryDataRecord = "BKI62";
    string itineraryDataSegmentRecord = "BKI63";
    string documentAmountsRecord = "BAR64";
    string additionalInformationPassengerRecord = "BAR65";
    string additionalInformationFormOfPaymentRecord = "BAR66";
    string fareCalculationRecord = "BKF81";
    string additionalCardInformationRecord = "BCC82";
    string formOfPaymentRecord = "BKP84";

    string commissionRecord = "BKS39"

    public class IataParserService
    {
        public Ticket ParseTicket(List<string> ticketLines)
        {
            // Implement the logic to parse the ticket from the lines
            // I will put the data to ticket object's corersponding fields
            Ticket ticket = new Ticket();
            int BKS24counter = 0;
            int BKS46counter = 0;

            // Example parsing logic (need to customize it according to your data format)
            foreach (var line in ticketLines)
            {
                // Parse the line and populate the ticket object

                string standardMessageIdentifier;
                string standardNumericQualifier;
                string recordTypeIdentifier;

                standardMessageIdentifier = line.Substring(standardMessageIdentifierStartPosition, standardMessageIdentifierLength);
                standardNumericQualifier = line.Substring(standardNumericQualifierStartPosition, standardNumericQualifierLength); 

                recordTypeIdentifier = standardMessageIdentifier + standardNumericQualifier; 

                switch(recordTypeIdentifier)
                {
                    case transactionHeaderRecord: // BKT06
                        string transactionNumber;
                        string transactionRecordCounter;
                        string ticketingAirlineCodeNumber;
                        string reportingSystemIdentifier;

                        // following substrings will be paramterized too like line 34-35 BUT IGNORE FOR NOW
                        transactionNumber = line.Substring(13, 6); 
                        transactionRecordCounter = line.Substring(21, 3);
                        ticketingAirlineCodeNumber = line.Substring(24, 3);
                        reportingSystemIdentifier = line.Substring(64, 4);

                        ticket.TransactionNumber = transactionNumber;
                        ticket.TransactionRecordCounter = transactionRecordCounter;
                        ticket.TicketingAirlineCodeNumber = ticketingAirlineCodeNumber;
                        ticket.ReportingSystemIdentifier = reportingSystemIdentifier;
                    break;

                    case documentIdentificationRecord: //BKS24

                        if (BKS24counter == 0) {

                            // VARIABLES FOR BKS24 C=0
                            string dateOfIssue;
                            string transactionNumber;
                            string ticketDocumentNumber;
                            string checkDigit;
                            string couponUseIndicator;
                            string conjunctionTicketIndicator;
                            string agentNumericCode;
                            string reasonForIssuanceCode;
                            string tourCode;
                            string transactionCode;
                            string trueOriginDestinationCityCodes;
                            string pnrReferenceOrAirlineData;
                            string timeOfIssue;
                            string journeyTurnaroundAirportCityCode;

                            // ASSIGNING PROPER PLACES TO VARIABLES IN LINE
                            dateOfIssue = line.Substring(13, 6); 
                            transactionNumber = line.Substring(19, 6); 
                            ticketDocumentNumber = line.Substring(25, 14); 
                            checkDigit = line.Substring(39, 1); 
                            couponUseIndicator = line.Substring(40, 4); 
                            conjunctionTicketIndicator = line.Substring(44, 3); 
                            agentNumericCode = line.Substring(47, 8); 
                            reasonForIssuanceCode = line.Substring(55, 1); 
                            tourCode = line.Substring(56, 15); 
                            transactionCode = line.Substring(71, 4); 
                            trueOriginDestinationCityCodes = line.Substring(75, 10); 
                            pnrReferenceOrAirlineData = line.Substring(85, 13); 
                            timeOfIssue = line.Substring(98, 4); 
                            journeyTurnaroundAirportCityCode = line.Substring(102, 5);
                        }

                        // the assigning logic of the following part will be adjusted
                        // maybe make a logic that if a ticket has CNJ, create a subticket for it ?
                        // or maybe just take the variables that can take 2 value when ticket is CNJ 
                        // as a list of strings, and when converting to the json just give them as ordered 
                        // ticket information. Since they would be ordered, it would be easy to extract?
                        if (BKS24counter == 1 && line.Substring(44, 3) == CNJ){ // change substring part to isConjuctor variable?

                            string ticketDocumentNumber;
                            string checkDigit;
                            string couponUseIndicator;
                            string conjunctionTicketIndicator;

                            ticketDocumentNumber = line.Substring(25, 14); 
                            checkDigit = line.Substring(39, 1); 
                            couponUseIndicator = line.Substring(40, 4); 
                            conjunctionTicketIndicator = line.Substring(44, 3); 
                        }
                        BKS24counter++;
                    break;

                    case (qualifyingIssueInformationForSalesTransactionsRecord):
                        if(BKS46counter == 0){
                            string originalIssueTicketDocumentNumber;
                            string originalIssueLocationCityCode;
                            string originalIssueDate;
                            string originalIssueAgentNumericCode;
                            string endorsementsRestrictions;

                            originalIssueTicketDocumentNumber = line.Substring(40, 14); 
                            originalIssueLocationCityCode = line.Substring(54, 3); 
                            originalIssueDate = line.Substring(57, 7); 
                            originalIssueAgentNumericCode = line.Substring(64, 8); 
                            endorsementsRestrictions = line.Substring(72, 49); 
                        }

                        BKS46counter++;
                    break;

                    case (additionalItineraryDataRecord):

                        // THE LOGIC OF FOLLOWING PART (FOR CNJ FLIGHT TICKETS) WILL BE IMPLEMENTED
                        if (BKS24counter == 1){
                            string segmentIdentifier;
                            string originAirportCityCode;
                            string flightDepartureDate;
                            string flightDepartureTime;
                            string flightDepartureTerminal;
                            string destinationAirportCityCode;
                            string flightArrivalDate;
                            string flightArrivalTime;
                            string flightArrivalTerminal;

                            segmentIdentifier = line.Substring(40, 1);
                            originAirportCityCode = line.Substring(41, 5);
                            flightDepartureDate = line.Substring(46, 7);
                            flightDepartureTime = line.Substring(53, 5);
                            flightDepartureTerminal = line.Substring(58, 5);
                            destinationAirportCityCode = line.Substring(63, 5);
                            flightArrivalDate = line.Substring(68, 7);
                            flightArrivalTime = line.Substring(75, 5);
                            flightArrivalTerminal = line.Substring(80, 5);
                        }

                        if (BKS24counter == 2){
                            string segmentIdentifier;
                            string originAirportCityCode;
                            string flightDepartureDate;
                            string flightDepartureTime;
                            string flightDepartureTerminal;
                            string destinationAirportCityCode;
                            string flightArrivalDate;
                            string flightArrivalTime;
                            string flightArrivalTerminal;

                            segmentIdentifier = line.Substring(40, 1);
                            originAirportCityCode = line.Substring(41, 5);
                            flightDepartureDate = line.Substring(46, 7);
                            flightDepartureTime = line.Substring(53, 5);
                            flightDepartureTerminal = line.Substring(58, 5);
                            destinationAirportCityCode = line.Substring(63, 5);
                            flightArrivalDate = line.Substring(68, 7);
                            flightArrivalTime = line.Substring(75, 5);
                            flightArrivalTerminal = line.Substring(80, 5);
                        }
                    break;

                    case (itineraryDataSegmentRecord):

                        if (BKS24counter == 1){
                            string segmentIdentifier;
                            string stopoverCode;
                            string notValidBeforeDate;
                            string notValidAfterDate;
                            string originAirportCityCode;
                            string destinationAirportCityCode;
                            string carrier;
                            string soldPassengerCabin;
                            string flightNumber;
                            string reservationBookingDesignator;
                            string flightDepartureDate;
                            string flightDepartureTime;
                            string flightBookingStatus;
                            string baggageAllowance;
                            string fareBasisTicketDesignator;
                            string frequentFlyerReference;
                            string fareComponentPricedPassengerTypeCode;
                            string throughChangeOfGaugeIndicator;
                            string equipmentCode;

                            segmentIdentifier = line.Substring(40, 1);
                            stopoverCode = line.Substring(41, 1);
                            notValidBeforeDate = line.Substring(42, 5);
                            notValidAfterDate = line.Substring(47, 5);
                            originAirportCityCode = line.Substring(52, 5);
                            destinationAirportCityCode = line.Substring(57, 5);
                            carrier = line.Substring(62, 3);
                            soldPassengerCabin = line.Substring(65, 1);
                            flightNumber = line.Substring(66, 5);
                            reservationBookingDesignator = line.Substring(71, 2);
                            flightDepartureDate = line.Substring(73, 7);
                            flightDepartureTime = line.Substring(80, 5);
                            flightBookingStatus = line.Substring(85, 2);
                            baggageAllowance = line.Substring(87, 3);
                            fareBasisTicketDesignator = line.Substring(90, 15);
                            frequentFlyerReference = line.Substring(105, 20);
                            fareComponentPricedPassengerTypeCode = line.Substring(125, 3);
                            throughChangeOfGaugeIndicator = line.Substring(128, 1);
                            equipmentCode = line.Substring(129, 3);
                        }

                        if (BKS24counter == 2){
                            string segmentIdentifier;
                            string stopoverCode;
                            string notValidBeforeDate;
                            string notValidAfterDate;
                            string originAirportCityCode;
                            string destinationAirportCityCode;
                            string carrier;
                            string soldPassengerCabin;
                            string flightNumber;
                            string reservationBookingDesignator;
                            string flightDepartureDate;
                            string flightDepartureTime;
                            string flightBookingStatus;
                            string baggageAllowance;
                            string fareBasisTicketDesignator;
                            string frequentFlyerReference;
                            string fareComponentPricedPassengerTypeCode;
                            string throughChangeOfGaugeIndicator;
                            string equipmentCode;

                            segmentIdentifier = line.Substring(40, 1);
                            stopoverCode = line.Substring(41, 1);
                            notValidBeforeDate = line.Substring(42, 5);
                            notValidAfterDate = line.Substring(47, 5);
                            originAirportCityCode = line.Substring(52, 5);
                            destinationAirportCityCode = line.Substring(57, 5);
                            carrier = line.Substring(62, 3);
                            soldPassengerCabin = line.Substring(65, 1);
                            flightNumber = line.Substring(66, 5);
                            reservationBookingDesignator = line.Substring(71, 2);
                            flightDepartureDate = line.Substring(73, 7);
                            flightDepartureTime = line.Substring(80, 5);
                            flightBookingStatus = line.Substring(85, 2);
                            baggageAllowance = line.Substring(87, 3);
                            fareBasisTicketDesignator = line.Substring(90, 15);
                            frequentFlyerReference = line.Substring(105, 20);
                            fareComponentPricedPassengerTypeCode = line.Substring(125, 3);
                            throughChangeOfGaugeIndicator = line.Substring(128, 1);
                            equipmentCode = line.Substring(129, 3);
                        }
                    break;

                    case (documentAmountsRecord):
                        string fare;
                        string ticketingModeIndicator;
                        string equivalentFarePaid;
                        string total;
                        string servicingAirlineSystemProviderIdentifier;
                        string fareCalculationModeIndicator;
                        string bookingAgentIdentification;
                        string bookingEntityOutletType;
                        string fareCalculationPricingIndicator;
                        string airlineIssuingAgent;

                        fare = line.Substring(40, 12);
                        ticketingModeIndicator = line.Substring(52, 1);
                        equivalentFarePaid = line.Substring(53, 12);
                        total = line.Substring(65, 12);
                        servicingAirlineSystemProviderIdentifier = line.Substring(77, 4);
                        fareCalculationModeIndicator = line.Substring(81, 1);
                        bookingAgentIdentification = line.Substring(82, 6);
                        bookingEntityOutletType = line.Substring(88, 1);
                        fareCalculationPricingIndicator = line.Substring(89, 1);
                        airlineIssuingAgent = line.Substring(90, 8);
                    break;

                    case (additionalInformationPassengerRecord):
                        string passengerName;
                        string passengerSpecificData;
                        string dateOfBirth;
                        string passengerTypeCode;

                        passengerName = line.Substring(40, 49);
                        passengerSpecificData = line.Substring(89, 29);
                        dateOfBirth = line.Substring(118, 7);
                        passengerTypeCode = line.Substring(125, 3);
                    break;

                    case (additionalInformationFormOfPaymentRecord):
                        string formOfPaymentSequenceNumber;
                        string formOfPaymentInformation;

                        formOfPaymentSequenceNumber = line.Substring(40, 1);
                        formOfPaymentInformation = line.Substring(41, 50);
                    break;

                    case (fareCalculationRecord):
                        string fareCalculationSequenceNumber;
                        string fareCalculationArea;

                        fareCalculationSequenceNumber = line.Substring(40, 1);
                        fareCalculationArea = line.Substring(41, 87);
                    break;

                    case (additionalCardInformationRecord):
                        string formOfPaymentType;
                        string formOfPaymentTransactionIdentifier;

                        formOfPaymentType = line.Substring(25, 10);
                        formOfPaymentTransactionIdentifier = line.Substring(35, 25);
                    break;

                    case (formOfPaymentRecord):
                        string formOfPaymentType;
                        string formOfPaymentAmount;
                        string formOfPaymentAccountNumber;
                        string expiryDate;
                        string extendedPaymentCode;
                        string approvalCode;
                        string invoiceNumber;
                        string invoiceDate;
                        string remittanceAmount;
                        string cardVerificationValueResult;
                        string currencyType;

                        formOfPaymentType = line.Substring(25, 10);
                        formOfPaymentAmount = line.Substring(35, 11);
                        formOfPaymentAccountNumber = line.Substring(46, 19);
                        expiryDate = line.Substring(65, 4);
                        extendedPaymentCode = line.Substring(69, 2);
                        approvalCode = line.Substring(71, 6);
                        invoiceNumber = line.Substring(77, 14);
                        invoiceDate = line.Substring(91, 6);
                        remittanceAmount = line.Substring(97, 11);
                        cardVerificationValueResult = line.Substring(108, 1);
                        currencyType = line.Substring(132, 4);
                    break;

                    case (commissionRecord):
                        string commissionType;
                        string commissionRate;
                        string commissionAmount;
                        string effectiveCommissionRate;
                        string effectiveCommissionAmount;
                        string currencyType;

                        commissionType = line.Substring(43, 6);
                        commissionRate = line.Substring(49, 5);
                        commissionAmount = line.Substring(54, 11);
                        effectiveCommissionRate = line.Substring(87, 5);
                        effectiveCommissionAmount = line.Substring(92, 11);
                        currencyType = line.Substring(132, 4);
                    break;
                }
            }

            return ticket;
        }
    }
}