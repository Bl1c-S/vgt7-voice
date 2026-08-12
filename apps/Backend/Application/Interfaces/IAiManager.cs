    using Application.Models.AiModel;

    namespace Application;

    public interface IAiManager
    {
        protected AiModelDescriptor Model { get; set; }
        Task<string> SendRequestAsync(string prompt);
    }