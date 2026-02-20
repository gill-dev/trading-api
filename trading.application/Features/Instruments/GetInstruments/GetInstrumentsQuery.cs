using trading.application.Abstractions.Messaging;
using Trading.Contracts.Requests;

namespace trading.application.Features.Instruments.GetInstruments;

public sealed record GetInstrumentsQuery(string? Instruments = null) : IQuery<InstrumentResponse[]>;
