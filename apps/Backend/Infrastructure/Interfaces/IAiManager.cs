    using Application.Models.AiModel;

    namespace Infrastructure.Interfaces;

    public interface IAiManager
    {
        protected AiModelDescriptor Model { get; set; }
        Task<string> SendRequestAsync(string prompt);
    }