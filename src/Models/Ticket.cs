using System.Collections.Generic;

namespace IataParser.Models
{
    public class Ticket
{
    public string TransactionNumber { get; set; }
    public string TransactionRecordCounter { get; set; }
    public string TicketingAirlineCodeNumber { get; set; }
    public string ReportingSystemIdentifier { get; set; }
    public string DateOfIssue { get; set; }
    public string TicketDocumentNumber { get; set; }
    public string CheckDigit { get; set; }
    public string CouponUseIndicator { get; set; }
    public string ConjunctionTicketIndicator { get; set; }
    public string AgentNumericCode { get; set; }
    public string ReasonForIssuanceCode { get; set; }
    public string TourCode { get; set; }
    public string TransactionCode { get; set; }
    public string TrueOriginDestinationCityCodes { get; set; }
    public string PnrReferenceAndOrAirlineData { get; set; }
    public string TimeOfIssue { get; set; }
    public string JourneyTurnaroundAirportCityCode { get; set; }
    public string OriginalIssueTicketDocumentNumber { get; set; }
    public string OriginalIssueLocationCityCode { get; set; }
    public string OriginalIssueDate { get; set; }
    public string OriginalIssueAgentNumericCode { get; set; }
    public string EndorsementsRestrictions { get; set; }

    // FOLLOWING PROPERTIES ARE STRING FOR PROPER CNJ TICKET PROCESSING 
    public bool IsConjunctionTicket { get; set; } = false;
    public List<string> SegmentIdentifier { get; set; } = new List<string>();
    public List<string> OriginAirportCityCode { get; set; } = new List<string>();
    public List<string> FlightDepartureDate { get; set; } = new List<string>();
    public List<string> FlightDepartureTime { get; set; } = new List<string>();
    public List<string> FlightDepartureTerminal { get; set; } = new List<string>();
    public List<string> DestinationAirportCityCode { get; set; } = new List<string>();
    public List<string> FlightArrivalDate { get; set; } = new List<string>();
    public List<string> FlightArrivalTime { get; set; } = new List<string>();
    public List<string> FlightArrivalTerminal { get; set; } = new List<string>();
    public List<string> StopoverCode { get; set; } = new List<string>();
    public List<string> NotValidBeforeDate { get; set; } = new List<string>();
    public List<string> NotValidAfterDate { get; set; } = new List<string>();
    public List<string> Carrier { get; set; } = new List<string>();
    public List<string> SoldPassengerCabin { get; set; } = new List<string>();
    public List<string> FlightNumber { get; set; } = new List<string>();
    public List<string> ReservationBookingDesignator { get; set; } = new List<string>();
    public List<string> FlightBookingStatus { get; set; } = new List<string>();
    public List<string> BaggageAllowance { get; set; } = new List<string>();
    public List<string> FareBasisTicketDesignator { get; set; } = new List<string>();
    public List<string> FrequentFlyerReference { get; set; } = new List<string>();
    public List<string> FareComponentPricedPassengerTypeCode { get; set; } = new List<string>();
    public List<string> ThroughChangeOfGaugeIndicator { get; set; } = new List<string>();
    public List<string> EquipmentCode { get; set; } = new List<string>();

    public string Fare { get; set; }
    public string TicketingModeIndicator { get; set; }
    public string EquivalentFarePaid { get; set; }
    public string Total { get; set; }
    public string ServicingAirlineSystemProviderIdentifier { get; set; }
    public string FareCalculationModeIndicator { get; set; }
    public string BookingAgentIdentification { get; set; }
    public string BookingEntityOutletType { get; set; }
    public string FareCalculationPricingIndicator { get; set; }
    public string AirlineIssuingAgent { get; set; }
    public string PassengerName { get; set; }
    public string PassengerSpecificData { get; set; }
    public string DateOfBirth { get; set; }
    public string PassengerTypeCode { get; set; }
    public string FormOfPaymentSequenceNumber { get; set; }
    public string FormOfPaymentInformation { get; set; }
    public string FareCalculationSequenceNumber { get; set; }
    public string FareCalculationArea { get; set; }
    public string FormOfPaymentType { get; set; }
    public string FormOfPaymentTransactionIdentifier { get; set; }
    public string FormOfPaymentAmount { get; set; }
    public string FormOfPaymentAccountNumber { get; set; }
    public string ExpiryDate { get; set; }
    public string ExtendedPaymentCode { get; set; }
    public string ApprovalCode { get; set; }
    public string InvoiceNumber { get; set; }
    public string InvoiceDate { get; set; }
    public string RemittanceAmount { get; set; }
    public string CardVerificationValueResult { get; set; }
    public string CommissionType { get; set; }
    public string CommissionRate { get; set; }
    public string CommissionAmount { get; set; }
    public string EffectiveCommissionRate { get; set; }
    public string EffectiveCommissionAmount { get; set; }
    public string CurrencyType { get; set; }
}
}