namespace TrackPro.Domain.Entities

// Uma das entidades principais do projeto, ela contém as regras de negócio do sistema
{
    public class Part
    {
        public string Code { get; private set; }
        public string Description { get; private set; }
        public string Status { get; private set; }
        public int CurrentStationId { get; private set; }

        // Construtor usado para criar uma nova peça. Garante que toda peça já nasça no estado inicial correto
        public Part(string code, string description)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentException("O código da peça não pode ser vazio.");

            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("A descrição da peça não pode ser vazia.");

            Code = code;
            Description = description;

            CurrentStationId = 1;
            Status = "Em processo - Recebimento";
        }

        // Método que contém a lógica para movimentar a peça
        public void MoveToNextStation(Station nextStation)
        {
            CurrentStationId = nextStation.Id;
            Status = $"Em processo - {nextStation.Name}";
        }

        // Método para finalizar o processo da peça
        public void FinishProcess()
        {
            Status = "Finalizada";
            CurrentStationId = 0; 
        }
    }
}