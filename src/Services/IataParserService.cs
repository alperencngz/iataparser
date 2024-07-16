using System;
using System.Collections.Generic;
using IataParser.Models;

namespace IataParser.Services
{
    public class IataParserService
    {
        public Ticket ParseTicket(List<string> ticketLines)
        {
            // Implement the logic to parse the ticket from the lines
            // I will put the data to ticket object's corersponding fields
            Ticket ticket = new Ticket();

            // Example parsing logic (need to customize it according to your data format)
            foreach (var line in ticketLines)
            {
                // Parse the line and populate the ticket object

                string standardMessageIdentifier;
                string standardNumericQualifier;
                string recordTypeIdentifier;

                standardMessageIdentifier = line.Substring(0,3); // 0-> standardMessageIdentifierStart , 3 -> standardMessageIdentifierStartLength
                standardNumericQualifier = line.Substring(11, 2); // the numbers will be paramterized accordingly

                recordTypeIdentifier = standardMessageIdentifier + standardNumericQualifier; 

                switch(recordTypeIdentifier)
                {
                    case "BKT06":
                        string transactionNumber;
                        string transactionRecordCounter;
                        string ticketingAirlineCodeNumber;
                        string reportingSystemIdentifier;

                        transactionNumber = line.Substring(13, 6);
                        transactionRecordCounter = line.Substring(21, 3);
                        ticketingAirlineCodeNumber = line.Substring(24, 3);
                        reportingSystemIdentifier = line.Substring(64, 4);

                        ticket.TransactionNumber = transactionNumber;
                        ticket.TransactionRecordCounter = transactionRecordCounter;
                        ticket.TicketingAirlineCodeNumber = ticketingAirlineCodeNumber;
                        ticket.ReportingSystemIdentifier = reportingSystemIdentifier;
                    break;

                    case "BKS24":

                    break;

                }
            }

            return ticket;
        }
    }
}