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
    string qualifyingIssueInformationForSalesTransactionsRecord == "BKS46";

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

                        // following substrings will be paramterized too like line 34-35
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
                }
            }

            return ticket;
        }
    }
}