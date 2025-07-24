namespace TrackPro.Domain.Entities

// Entidade para o registro de histórico
{
    public class Movement
    {
        public Guid Id { get; private set; }
        public string CodePart { get; private set; }

        public int? OriginStationId { get; private set; }
        public int DestinationStationId { get; private set; }
        public DateTime Date { get; private set; }
        public string Responsible { get; private set; }

        public Movement(string codePart, int? originStationId, int destinationStationId, string responsible)
        {
            if (string.IsNullOrWhiteSpace(codePart))
                throw new ArgumentException("Código da peça é obrigatório.");

            if (string.IsNullOrWhiteSpace(responsible))
                throw new ArgumentException("Responsável é obrigatório.");

            Id = Guid.NewGuid();
            CodePart = codePart;
            OriginStationId = originStationId;
            DestinationStationId = destinationStationId;
            Responsible = responsible;
            Date = DateTime.UtcNow;
        }
    }
}