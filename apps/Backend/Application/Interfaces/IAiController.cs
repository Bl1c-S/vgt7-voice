    using Application.Models.AiModel;

    namespace Application;

    public interface IAiController
    {
        protected AiModelDescriptor Model { get; set; }
        Task<string> SendRequestAsync(string prompt);
    }