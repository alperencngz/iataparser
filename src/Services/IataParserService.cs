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
            }

            return ticket;
        }
    }
}