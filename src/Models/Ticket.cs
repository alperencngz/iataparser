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
    public string SegmentIdentifier { get; set; }
    public string OriginAirportCityCode { get; set; }
    public string FlightDepartureDate { get; set; }
    public string FlightDepartureTime { get; set; }
    public string FlightDepartureTerminal { get; set; }
    public string DestinationAirportCityCode { get; set; }
    public string FlightArrivalDate { get; set; }
    public string FlightArrivalTime { get; set; }
    public string FlightArrivalTerminal { get; set; }
    public string StopoverCode { get; set; }
    public string NotValidBeforeDate { get; set; }
    public string NotValidAfterDate { get; set; }
    public string Carrier { get; set; }
    public string SoldPassengerCabin { get; set; }
    public string FlightNumber { get; set; }
    public string ReservationBookingDesignator { get; set; }
    public string FlightBookingStatus { get; set; }
    public string BaggageAllowance { get; set; }
    public string FareBasisTicketDesignator { get; set; }
    public string FrequentFlyerReference { get; set; }
    public string FareComponentPricedPassengerTypeCode { get; set; }
    public string ThroughChangeOfGaugeIndicator { get; set; }
    public string EquipmentCode { get; set; }
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